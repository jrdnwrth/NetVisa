using System;
using System.IO;
using System.Text;

namespace NetVisa;

/// <summary>
/// VISA base class. Uses delegates from visa_wrapper.
/// </summary>
public class Visa : IDisposable
{
    /// <summary>VisaC object</summary>
    //private Driver _driver;
    private object _readLocker;
    public Resource_Manager resource_manager;
    private Driver _driver;
    /// <summary>Buffer for reading from instrument</summary>
    private readonly byte[] _buffer = new byte[1000000];
    private int? _cachedTimeout;
    /// <summary>Keeps the SRQ installed handler reference</summary>
    private Driver.EventHandler _installedVisaCsrqHandler;

    protected int InterfaceType { get; }

    protected string ResourceClass { get; }

    /// <summary>VISA timeout in milliseconds</summary>
    public int Timeout
    {
        get
        {
            if (!this._cachedTimeout.HasValue)
                this._cachedTimeout = new int?(this._GetAttributeInt(1073676314U));
            return this._cachedTimeout.Value;
        }
        set
        {
            if (value < 1)
                throw new ArgumentException("TimeoutMs");
            if (this._cachedTimeout.HasValue)
            {
                if (value != this._cachedTimeout.Value)
                    this._SetAttributeInt(1073676314U, value);
            }
            else
                this._SetAttributeInt(1073676314U, value);
            this._cachedTimeout = new int?(value);
        }
    }

    /// <summary>Enable termination character when Reading</summary>
    public bool ReadTermCharEnabled
    {
        get => this._GetAttributeInt(1073676344U) == 1;
        set => this._SetAttributeInt(1073676344U, value ? 1 : 0);
    }

    /// <summary>Define termination character when Reading</summary>
    public int ReadTermChar
    {
        get => this._GetAttributeInt(1073676312U);
        set => this._SetAttributeInt(1073676312U, value);
    }

    /// <summary>Send End Enable when Writing</summary>
    public bool SendEndEnable
    {
        get => this._GetAttributeInt(1073676310U) == 1;
        set => this._SetAttributeInt(1073676310U, value ? 1 : 0);
    }

    /// <summary>Serial Port Send End In</summary>
    public int SerialSendEndIn
    {
        get => this._GetAttributeInt(1073676467U);
        set => this._SetAttributeInt(1073676467U, value);
    }

    /// <summary>Serial Port Send End Out</summary>
    public int SerialSendEndOut
    {
        get => this._GetAttributeInt(1073676468U);
        set => this._SetAttributeInt(1073676468U, value);
    }

    /// <summary>Checks whether the TCPIP session is HiSLIP</summary>
    public bool IsHislip => this._GetAttributeInt(1073677059U) == 1;

    /// <summary>
    /// Size of the public buffer used for all read operations
    /// </summary>
    public int ReadBufferSize => this._buffer.Length;

    /// <summary>VISA Handle object</summary>
    public Visa_Session_Handle Session { get; }

    /// <summary>Constructor for the VISA object</summary>
    /// <param name="resource_name">Standard VISA Resource name or an alias name</param>
    /// <param name="visa_plugin">If non-null, you can use a custom VISA API implementation. Has priority over resourceName plugin</param>
    public Visa(string resource_name, Resource_Manager resource_manager)
    {
        this._readLocker = new object();
        this._installedVisaCsrqHandler = null;
        this.Session = new Visa_Session_Handle(resource_name, resource_manager.driver.visa_plugin);
        this._driver = resource_manager.driver;
        this.resource_manager = resource_manager;
        try
        {
            int session;
            this._ThrowOnError(this._driver.Open(this.resource_manager.handle, this.Session.VisaResourceName, 0U, 0U, out session), "Error when opening new VISA Session. ResourceName: '" + resource_name + "'");
            this.Session.Handle = session;
            this.InterfaceType = this._GetAttributeInt(1073676657U);
            this.ResourceClass = this._GetAttributeString(3221159937U);
        }
        catch (Visa_Exception _)
        {
            this.Close();
            throw;
        }
    }

    /// <summary>Converts the status code into human-readable message</summary>
    /// <param name="status">Status code from VISA functions</param>
    private string _GetVISAStatusDesc(int status)
    {
        byte[] numArray = new byte[256];
        int num1 = this._driver.StatusDesc(this.Session.Handle, status, numArray);
        return Encoding.ASCII.GetString(numArray).TrimEnd(char.MinValue);
    }

    /// <summary>Error handler for all the VISA IOException()</summary>
    /// <param name="status">Return value from VISA functions</param>
    /// <param name="context">Additional optional text</param>
    private int _ThrowOnError(int status, string context = "")
    {
        if (status >= 0)
            return status;
        string str = !string.IsNullOrEmpty(context) ? (!context.StartsWith("$", StringComparison.Ordinal) ? string.Format("{0}: {1} ", this, context) : context.TrimStart('$') + " ") : string.Format("{0}: ", this);
        if (status == -1073807339)
            throw new Exception(this.Session.ResourceName + str + $"Timeout occurred. VISA timeout is set to {this.Timeout} ms");
        string message;
        if (status == -1073807343)
            message = str + "Given Resource Name is invalid or does not exist.";
        else
            message = str + string.Format("VISA Error 0x{0:X}: {1}", status, this._GetVISAStatusDesc(status));
        throw new Visa_Exception(this.Session.ResourceName, message);
    }

    /// <summary>
    /// Returns true, if entered status code indicates that more data might be available
    /// </summary>
    /// <param name="status"></param>
    private bool _MoreDataIsAvailable(int status) => status == 1073676294;

    /// <summary>Read bytes to Stream</summary>
    /// <param name="stream">Stream to read to</param>
    /// <param name="count">Number of bytes to read</param>
    /// <param name="moreDataAvailable">Returns true, if more data for reading is available</param>
    /// <param name="assureResponseEndWithTc">If true, each VISA read must end with TermChar. If not, the reading continues</param>
    /// <returns>Number of bytes actually read</returns>
    public int ReadToStream(
      Stream stream,
      int count,
      bool assureResponseEndWithTc,
      out bool moreDataAvailable)
    {
        byte[] buffer = new byte[count];
        uint read1;
        int status1 = this._ThrowOnError(this._driver.Read(this.Session.Handle, buffer, (uint)count, out read1), "Read To Stream -");
        stream.Write(buffer, 0, (int)read1);
        moreDataAvailable = this._MoreDataIsAvailable(status1);
        if (assureResponseEndWithTc && read1 < count && read1 > 0U)
        {
            byte num = Convert.ToByte(this.ReadTermChar);
            if (buffer[(int)read1 - 1] != num)
            {
                uint read2;
                int status2 = this._ThrowOnError(this._driver.Read(this.Session.Handle, buffer, (uint)count - read1, out read2), "Read To Stream2 -");
                stream.Write(buffer, 0, (int)read2);
                moreDataAvailable = this._MoreDataIsAvailable(status2);
                read1 += read2;
            }
        }
        return (int)read1;
    }

    /// <summary>Writes bytes from Stream</summary>
    /// <param name="stream">Stream to write from</param>
    /// <param name="count">Bytes count to write</param>
    /// <returns>Bytes count actually written</returns>
    public int WriteFromStream(Stream stream, int count)
    {
        byte[] buffer = new byte[count];
        int length = stream.Read(buffer, 0, count);
        uint written;
        this._ThrowOnError(this._driver.Write(this.Session.Handle, buffer, (uint)length, out written), "Write To Stream -");
        return (int)written;
    }

    /// <summary>Get Attribute of Int32 type</summary>
    /// <param name="attributeId"></param>
    /// <returns>value of the attribute</returns>
    private int _GetAttributeInt(uint attributeId)
    {
        UIntPtr attrValue;
        this._ThrowOnError(this._driver.GetAttributeInt(this.Session.Handle, attributeId, out attrValue), "Get Attribute Int32 -");
        return (int)attrValue.ToUInt32();
    }

    /// <summary>Set Attribute of Int32 type</summary>
    /// <param name="attributeId"></param>
    /// <param name="value"></param>
    private void _SetAttributeInt(uint attributeId, int value)
    {
        this._ThrowOnError(this._driver.SetAttribute(this.Session.Handle, attributeId, (UIntPtr)(ulong)value), "Set Attribute Int32 -");
    }

    /// <summary>Get Attribute of string type</summary>
    /// <param name="attributeId"></param>
    /// <returns>value of the attribute</returns>
    private string _GetAttributeString(uint attributeId)
    {
        StringBuilder attrValue = new StringBuilder(256);
        this._ThrowOnError(this._driver.GetAttributeString(this.Session.Handle, attributeId, attrValue), "Get Attribute String -");
        return attrValue.ToString();
    }

    public void Dispose()
    {
        this.Close();
    }

    /// <summary>Close the resource manager and the visa session</summary>
    public void Close()
    {
        int num = this._driver.Close(this.Session.Handle);
    }

    /// <summary>Calling viClear() method</summary>
    public void Clear() => this._ThrowOnError(this._driver.Clear(this.Session.Handle), "Calling viClear -");

    /// <summary>Write text to instrument</summary>
    /// <param name="text">text to write</param>
    public void Write(string text)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(text);
        uint written;
        int status = this._driver.Write(this.Session.Handle, bytes, (uint)bytes.Length, out written);
        if (status >= 0)
            return;
        this._ThrowOnError(status, "VISA Write -");
    }

    /// <summary>Write binary buffer to instrument</summary>
    /// <param name="buffer">binary buffer to write</param>
    public void Write(byte[] buffer)
    {
        uint written;
        int status = this._driver.Write(this.Session.Handle, buffer, (uint)buffer.Length, out written);
        if (status >= 0)
            return;
        this._ThrowOnError(status, "Write Binary Data -");
    }

    /// <summary>
    /// Reads data from instrument with defined maximum length.
    /// The maxLength value cannot exceed the _buffer.Length
    /// </summary>
    /// <param name="maxLength">Maximum data length to read</param>
    /// <param name="moreDataAvailable">Returns true, if more data for reading is available</param>
    /// <param name="assureResponseEndWithTc">If true, each VISA read must end with TermChar. If not, the reading continues</param>
    /// <param name="readCount">Number of bytes actually read</param>
    /// <returns>Data as Byte array</returns>
    public byte[] Read(
      int maxLength,
      out bool moreDataAvailable,
      bool assureResponseEndWithTc,
      out int readCount)
    {
        lock (this._readLocker)
        {
            if (maxLength > this._buffer.Length)
                throw new Visa_Exception(this.Session.ResourceName, string.Format("Attempting to read data from instrument with maximum count bigger than public buffer size: {0} > {1}", maxLength, _buffer.Length));
            uint read1;
            int status1 = this._driver.Read(this.Session.Handle, this._buffer, (uint)maxLength, out read1);
            if (status1 < 0)
                this._ThrowOnError(status1, "VISA Read -");
            moreDataAvailable = this._MoreDataIsAvailable(status1);
            if (assureResponseEndWithTc && read1 < maxLength && read1 > 0U)
            {
                byte num = Convert.ToByte(this.ReadTermChar);
                if (this._buffer[(int)read1 - 1] != num)
                {
                    byte[] numArray = new byte[maxLength - read1];
                    uint read2;
                    int status2 = this._ThrowOnError(this._driver.Read(this.Session.Handle, numArray, (uint)maxLength - read1, out read2), "VISA Read2 -");
                    Buffer.BlockCopy(numArray, 0, _buffer, (int)read1, (int)read2);
                    moreDataAvailable = this._MoreDataIsAvailable(status2);
                    if (!moreDataAvailable && read1 == maxLength && this._buffer[(int)read1 - 1] != num)
                        moreDataAvailable = true;
                    read1 += read2;
                }
            }
            readCount = (int)read1;
            return this._buffer;
        }
    }

    /// <summary>
    /// Reads data as string from instrument with defined maximum length.
    /// The maxLength value cannot exceed the _buffer.Length
    /// </summary>
    /// <param name="maxLength">Maximum string length to read</param>
    /// <param name="moreDataAvailable">Returns true, if more data for reading is available</param>
    /// <param name="assureResponseEndWithTc">If true, each VISA read must end with TermChar. If not, the reading continues</param>
    /// <param name="readCount">Number of characters actually read</param>
    /// <returns>Read data as string</returns>
    public string ReadString(
      int maxLength,
      out bool moreDataAvailable,
      bool assureResponseEndWithTc,
      out int readCount)
    {
        return Encoding.ASCII.GetString(this.Read(maxLength, out moreDataAvailable, assureResponseEndWithTc, out readCount), 0, readCount);
    }

    /// <summary>Reads single character</summary>
    /// <returns>read character</returns>
    public char ReadChar()
    {
        byte[] buffer = new byte[1];
        uint read;
        this._ThrowOnError(this._driver.Read(this.Session.Handle, buffer, 1U, out read), "VISA Read Byte -");
        return Convert.ToChar(buffer[0]);
    }


    /// <summary>Reads Status Byte using viReadSTB()</summary>
    /// <returns>STatus Byte value</returns>
    public uint ReadStb()
    {
        uint status1;
        int status2 = this._driver.ReadStb(this.Session.Handle, out status1);
        if (status2 < 0)
            this._ThrowOnError(status2, "viReadSTB() -");
        return status1;
    }

    /// <summary>Enables Service Request Event</summary>
    protected void EnableSrqEvent(Constants.EventMechanism mechanism)
    {
        int status = this._driver.EnableEvent(this.Session.Handle, 1073684491U, (short)mechanism, 0);
        if (status >= 0)
            return;
        this._ThrowOnError(status, "Enable SRQ event -");
    }

    /// <summary>Disables Service Request Event</summary>
    protected void DisableSrqEvent(Constants.EventMechanism mechanism)
    {
        int status = this._driver.DisableEvent(this.Session.Handle, 1073684491U, (short)mechanism);
        if (status >= 0)
            return;
        this._ThrowOnError(status, "Disable SRQ event -");
    }

    /// <summary>Flushes all the existing events</summary>
    protected void DiscardAllEnabledEvents(Constants.EventMechanism mechanism)
    {
        int status = this._driver.DiscardEvents(this.Session.Handle, 1073709055U, (short)mechanism);
        if (status >= 0)
            return;
        this._ThrowOnError(status, "Discard all events -");
    }

    /// <summary>Flushes all the existing SRQ events</summary>
    protected void DiscardAllSrqEvents(Constants.EventMechanism mechanism)
    {
        int status = this._driver.DiscardEvents(this.Session.Handle, 1073684491U, (short)mechanism);
        if (status >= 0)
            return;
        this._ThrowOnError(status, "Discard all SRQ events -");
    }

    /// <summary>Installs SRQ handler</summary>
    private void _InstallSrqHandler(Driver.EventHandler handler)
    {
        int status = this._driver.InstallHandler(this.Session.Handle, 1073684491U, handler, 0);
        if (status >= 0)
            return;
        this._ThrowOnError(status, "Adding srq event handler for service request (ViInstallHandler) -");
    }

    /// <summary>Uninstalls SRQ handler</summary>
    private void _UninstallSrqHandler(Driver.EventHandler handler)
    {
        int status = this._driver.UninstallHandler(this.Session.Handle, 1073684491U, handler, 0);
        if (status >= 0)
            return;
        this._ThrowOnError(status, "Removing event handler for service request (ViUninstallHandler) -");
    }

    /// <summary>
    /// Waits on Service Request Event.
    /// If a timeout occurs, the method returns true
    /// </summary>
    /// <param name="timeoutMs">timeout for waiting on the SRQ event</param>
    /// <param name="disableAfterwards">If true, the DisableSrqEvent() is called afterwards</param>
    /// <returns>True, if timeout occurred</returns>
    public bool WaitOnSrqEvent(int timeoutMs, bool disableAfterwards)
    {
        int outEventType;
        int status = this._driver.WaitOnEvent(this.Session.Handle, 1073684491U, timeoutMs, out outEventType, IntPtr.Zero);
        if (disableAfterwards)
            this.DisableSrqEvent(Constants.EventMechanism.Queue);
        if (status == -1073807339)
            return true;
        if (status < 0)
            this._ThrowOnError(status, "Waiting on SRQ Event -");
        return false;
    }

    /// <summary>
    /// Public method to install a new srq handler.
    /// Visa-C SRQ handler is always only one method (SrqHandler), which serves as a router for other C#-like event 'handler'
    /// </summary>
    public void InstallSrqVisaChandler(EventHandler<Instr_Event_Args> handler)
    {
        if (this._installedVisaCsrqHandler != null)
            this._UninstallSrqHandler(this._installedVisaCsrqHandler);
        this._installedVisaCsrqHandler = new Driver.EventHandler(SrqHandler);
        this._InstallSrqHandler(this._installedVisaCsrqHandler);

        int SrqHandler(int vi, uint inEventType, int inContext, int inUserHandle)
        {
            Instr_Event_Args e = new Instr_Event_Args(this.Session.ResourceName)
            {
                StatusByte = (Status_Byte)this.ReadStb()
            };
            EventHandler<Instr_Event_Args> eventHandler = handler;
            if (eventHandler != null)
                eventHandler(this, e);
            return 0;
        }
    }

    /// <summary>Public method to uninstall VISA-C srq handler.</summary>
    public void UninstallSrqVisaChandler()
    {
        if (this._installedVisaCsrqHandler != null)
            this._UninstallSrqHandler(this._installedVisaCsrqHandler);
        this._installedVisaCsrqHandler = null;
        this.DisableSrqEvent(Constants.EventMechanism.Handler);
    }
}
