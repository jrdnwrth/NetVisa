using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace NetVisa
{
    public class Visa_Session : Visa
    {
        private readonly char _termChar;
        private int _opcTimeout;

        /// <summary>
        /// First read length
        /// </summary>
        private int FirsReadLen => Math.Min(this.IoSegmentSize, 1024);

        /// <summary>
        /// Next read length 
        /// </summary>
        private int NextReadChunkLen => Math.Min(this.IoSegmentSize, 65536);

        public bool VxiCapable { get; }

        public int ReadStbVisaTimeout { get; set; }

        public int WriteDelay { get; set; }

        public int ReadDelay { get; set; }

        public int IoSegmentSize { get; set; }

        public Settings Settings { get; }

        /// <summary>Timeout for all the OPC-sync operations</summary>
        public int OpcTimeout
        {
            get => this._opcTimeout;
            set => this._opcTimeout = value >= 1 ? value : throw new ArgumentException(nameof(OpcTimeout));
        }

        /// <summary>Type of the VISA session</summary>
        public SessionKind SessionType { get; }

        /// <summary>
        /// If set to true (default value), each VISA read must end with TermChar. If not, the reading continues
        /// </summary>
        public bool AssureResponseEndWithTc { get; }

        /// <summary>Constructor for the VisaSession</summary>
        /// <param name="resourceName"></param>
        /// <param name="visaPlugin">If non-null, you can use a custom VISA API implementation</param>
        public Visa_Session(
          string resourceName,
          Resource_Manager resource_manager
          )
          : base(resourceName, resource_manager)
        {
            this.SessionType = this._GetSessionKind();
            this.Settings = new Settings();
            this.VxiCapable = this.Settings.VxiCapable;
            this._termChar = this.Settings.TermChar;

            // Override settings depending on the session type.
            switch (this.SessionType)
            {
                case SessionKind.Serial:
                    this.ReadTermChar = _termChar;
                    this.ReadTermCharEnabled = true;
                    this.SerialSendEndIn = 0;
                    this.SerialSendEndOut = 0;
                    this.VxiCapable = false;
                    break;
                case SessionKind.Socket:
                    this.ReadTermChar = _termChar;
                    this.ReadTermCharEnabled = true;
                    this.VxiCapable = false;
                    break;
            }
            this.WriteDelay = this.Settings.WriteDelay;
            this.ReadDelay = this.Settings.ReadDelay;
            this.IoSegmentSize = this.Settings.IoSegmentSize;
            this.AssureResponseEndWithTc = this.Settings.AssureResponseEndWithTc;
            this.Timeout = this.Settings.VisaTimeout;
            this.OpcTimeout = this.Settings.OpcTimeout;
            this.ReadStbVisaTimeout = this.Settings.ReadStbVisaTimeout;

            this.Clear();
            this.Write("*CLS");
        }

        /// <summary>
        /// Reads string from instrument with unlimited length trimmed for trailing TermChars
        /// The read is performed in two steps to optimize memory use:
        /// The First read is performed with the fixed size of 1024 bytes.
        /// The Second read is then performed with 64kB segments, until all the data are read out.
        /// </summary>
        public string ReadStringUnknownLength()
        {
            DateTime now = DateTime.Now;
            bool moreDataAvailable;
            int readCount;
            byte[] numArray = this.Read(this.FirsReadLen, out moreDataAvailable, this.AssureResponseEndWithTc, out readCount);
            if (!moreDataAvailable)
                return Encoding.ASCII.GetString(numArray, 0, readCount).TrimEnd(this._termChar);
            string str;
            using (MemoryStream memoryStream = new MemoryStream(readCount))
            {
                memoryStream.Write(numArray, 0, readCount);
                this._ReadDataUnknownLengthToStream(memoryStream, this.NextReadChunkLen, false, new DateTime?(now));
                str = Encoding.ASCII.GetString(memoryStream.ToArray());
            }
            return str.TrimEnd(this._termChar);
        }

        /// <summary>
        /// Segmented reading of unknown-length data into a Stream.
        /// For Socket and Serial interfaces this method only works if the ReadTermCharacterEnabled is True
        /// Enter the startTime if you already have read some data into the stream, otherwise the method creates its own start time
        /// </summary>
        private void _ReadDataUnknownLengthToStream(
          Stream stream,
          int segmentSize,
          bool binTransfer,
          DateTime? startTime = null)
        {
            if (!startTime.HasValue)
                startTime = new DateTime?(DateTime.Now);
            int segmentIx = 0;
            long position1 = stream.Position;
            if (position1 > 0L)
                ++segmentIx;
            if (!this.VxiCapable & binTransfer && this.ReadTermCharEnabled)
                throw new Instrument_Exception(this.Session.ResourceName + ": " + string.Format("{0} interface does not support reading binary data of unknown length.", SessionType));
            bool finished;
            do
            {
                long position2 = stream.Position;
                bool moreDataAvailable;
                int stream1 = this.ReadToStream(stream, segmentSize, false, out moreDataAvailable);
                finished = !moreDataAvailable || stream1 < segmentSize;
                ++segmentIx;
                position1 += stream1;
            }
            while (!finished);
        }

        /// <summary>Returns session type</summary>
        private SessionKind _GetSessionKind()
        {
            switch (this.InterfaceType)
            {
                case 1:
                case 3:
                    return SessionKind.Gpib;
                case 4:
                    return SessionKind.Serial;
                case 6:
                    if (this.ResourceClass == "SOCKET")
                        return SessionKind.Socket;
                    return !this.IsHislip ? SessionKind.Vxi11 : SessionKind.Hislip;
                case 7:
                    return SessionKind.Usb;
                default:
                    return SessionKind.Unsupported;
            }
        }


        /// <summary>
        /// Read string from instrument with maximum defined length.
        /// The maxLength value cannot exceed the VISA's _buffer.Length (100kB)
        /// </summary>
        private string _ReadString(int maxLength, out bool moreDataAvailable)
        {
            return this.ReadString(maxLength, out moreDataAvailable, this.AssureResponseEndWithTc, out int _);
        }


        /// <summary>Sends *STB? query and reads the result</summary>
        private Status_Byte _QueryStb() => this.Settings.DisableStbQuery ? Status_Byte.None : (Status_Byte)Convert.ToInt32(this.QueryShort("*STB?"));

        /// <summary>
        /// Reads viReadSTB and casts it to the StatusByte type
        /// 
        /// </summary>
        /// <param name="blockTmoSettings">If true, ignores the ReadStbVisaTimeout setting.</param>
        public Status_Byte _ReadStb(bool blockTmoSettings)
        {
            if (blockTmoSettings || this.ReadStbVisaTimeout <= 0)
                return (Status_Byte)this.ReadStb();
            int num = 0;
            if (this.ReadStbVisaTimeout > 0)
            {
                num = this.Timeout;
                this.Timeout = this.ReadStbVisaTimeout;
            }
            try
            {
                return (Status_Byte)this.ReadStb();
            }
            finally
            {
                this.Timeout = num;
            }
        }

        public string QueryShort(string query)
        {
            this.Write(query);
            var str = this._ReadString(64, out bool moreDataAvailable);
            if (moreDataAvailable)
                throw new Exception("More than 64 bytes of data was returned for 'QueryShort'");
            return str;
        }

        /// <summary>Combines Write + ReadStringUnknownLength()</summary>
        public string QueryStringUnknownLength(string query)
        {
            this.Write(query);
            return this.ReadStringUnknownLength();
        }


        /// <summary>
        /// Returns one response to the SYSTEM:ERROR? query
        /// If 0,"No error is returned, the return string is null
        /// </summary>
        public string? QuerySystemError()
        {
            this.Write("SYST:ERR?");
            string str = this.ReadStringUnknownLength();
            if (str.StartsWith("0,") || str.StartsWith("+0,"))
                return null;
            return str;
        }

        /// <summary>
        /// Returns all errors in the instrument's error queue
        /// Used query: "SYSTEM:ERROR?"
        /// </summary>
        public List<string> QuerySystemErrorAll()
        {
            var string_list = new List<string>();
            for (int index = 0; index < 50; ++index)
            {
                string str = this.QuerySystemError();
                if (!string.IsNullOrEmpty(str))
                    string_list.Add(str);
                else
                    break;
            }
            return string_list;
        }

        /// <summary>
        /// Returns true, if error queue contains at least one error
        /// </summary>
        public bool ErrorQueueIsNotEmpty() => (this._QueryStb() & Status_Byte.ErrorQueueNotEmpty) > Status_Byte.None;

        /// <summary>
        /// Session type constructed from the VISA interfaceType and ResourceClass parameters
        /// </summary>
        public enum SessionKind
        {
            Unsupported = 0,
            Gpib = 1,
            Serial = 2,
            Vxi11 = 3,
            Hislip = 4,
            Socket = 5,
            Usb = 6,
        }
    }
}
