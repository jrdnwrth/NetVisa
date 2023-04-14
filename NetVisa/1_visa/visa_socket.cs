using System.Net;
using System.Net.Sockets;

namespace NetVisa;

/// <summary>
/// VISA Socket class as an extension of the base Socket
/// Provides:
/// - Connect()
/// - Disconnect()
/// - Write()
/// - Read()
/// </summary>
public class Visa_Socket : Socket
{
    /// <summary>Server IP Address</summary>
    public string IpAddress { get; set; }

    /// <summary>Server Port number</summary>
    public int Port { get; set; }

    /// <summary>Read Termination character</summary>
    public char TermChar { get; set; }

    /// <summary>Read Termination character enable</summary>
    public bool TermCharEnable { get; set; }

    /// <summary>Empty Constructor</summary>
    public Visa_Socket(char termChar)
      : base(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
    {
        this.TermChar = termChar;
        this.TermCharEnable = true;
    }

    /// <summary>Constructor with IP Address and port</summary>
    public Visa_Socket(string ipAddress, int port, char termChar)
      : this(termChar)
    {
        this.IpAddress = ipAddress;
        this.Port = port;
    }

    public override string ToString() => string.Format("VisaSocket {0}:{1} ({2})", IpAddress, Port, this.Connected ? "Connected" : (object)"NotConnected");

    /// <summary>Initialize connection</summary>
    public void Connect()
    {
        IPAddress address = IPAddress.Parse(this.IpAddress);
        try
        {
            this.Connect(address, this.Port);
        }
        catch (SocketException ex)
        {
            this._Throw("Establishing the connection to the instrument failed");
        }
        if (this.Connected)
            return;
        this._Throw("Establishing the connection to the instrument failed");
    }

    /// <summary>public handling of exceptions</summary>
    /// <param name="message"></param>
    private void _Throw(string message)
    {
        message = string.Format("SocketIO IP {0} port {1}: ", IpAddress, Port) + message;
        throw new Socket_Instrument_Exception(message.Trim());
    }

    /// <summary>Write bytes</summary>
    public int Write(byte[] buffer, int count)
    {
        if (!this.Connected)
            this._Throw("Connection is not valid");
        try
        {
            return this.Send(buffer, 0, count, SocketFlags.None);
        }
        catch (SocketException ex)
        {
            this._Throw("Error during writing. Details: " + ex.Message);
        }
        return 0;
    }

    /// <summary>Read bytes</summary>
    public int Read(byte[] buffer, int maxLen, out bool moreDataAvailable)
    {
        if (!this.Connected)
            this._Throw("Connection is not valid");
        bool flag = false;
        int offset = 0;
        int num1 = 0;
        int num2;
        do
        {
            do
            {
                int size = maxLen - offset;
                if (size > 0)
                {
                    try
                    {
                        int num3 = this.Receive(buffer, offset, size, SocketFlags.None);
                        num1 = offset;
                        offset += num3;
                    }
                    catch (SocketException ex) when (ex.SocketErrorCode == SocketError.TimedOut)
                    {
                        this._Throw(string.Format("Read timeout, timeout is set to {0} ms", ReceiveTimeout));
                    }
                }
                else
                    goto label_15;
            }
            while (!this.TermCharEnable);
            num2 = -1;
            for (int index = num1; index < offset; ++index)
            {
                if (buffer[index] == TermChar)
                {
                    num2 = index;
                    break;
                }
            }
        }
        while (num2 < 0);
        offset = num2 + 1;
        flag = true;
    label_15:
        moreDataAvailable = offset >= maxLen && (!this.TermCharEnable || !flag);
        return offset;
    }

    /// <summary>Set timeout - write and receive</summary>
    public int Timeout
    {
        get => this.ReceiveTimeout;
        set => this.ReceiveTimeout = this.SendTimeout = value;
    }

    /// <summary>Disconnects the session</summary>
    public void Disconnect() => this.Disconnect(false);

    /// <summary>
    /// When cought by the garbage collector, dispose automatically
    /// </summary>
    ~Visa_Socket() => this.Dispose(false);
}
