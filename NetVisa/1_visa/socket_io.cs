using System;
using System.Text;
using System.Text.RegularExpressions;

namespace NetVisa;

/// <summary>
/// Class implementing the exact VISA APIs for the message based communication
/// </summary>
public static class Socket_Io
{
    public static int viOpenDefaultRM(out int rmSession)
    {
        rmSession = Socket_Resource_Manager.GetNewSessionHandle();
        return 0;
    }

    public static int viOpen(
      int rmSession,
      string resource,
      uint accessMode,
      uint timeOut,
      out int session)
    {
        session = 0;
        Match match = new Regex("TCPIP::([^:]+)::(\\d+)::SOCKET", RegexOptions.IgnoreCase).Match(resource);
        if (!match.Success)
        {
            string lastError = "ResourceName '" + resource + "' is invalid for the direct SocketIO session. Supported format: 'TCPIP::192.168.1.1::5025::SOCKET'";
            SetLastError(rmSession, lastError);
            return -5001;
        }
        Visa_Socket visaSocket = new Visa_Socket(match.Groups[1].Value, Convert.ToInt32(match.Groups[2].Value), '\n');
        visaSocket.Connect();
        session = rmSession;
        Socket_Resource_Manager.SessionsList[session] = visaSocket;
        return 0;
    }

    /// <summary>Sets last error for the session</summary>
    public static void SetLastError(int session, string lastError) => Socket_Resource_Manager.LastError[session] = lastError;

    /// <summary>Gets and clears last error for the session</summary>
    public static string GetLastError(int session)
    {
        string lastError = Socket_Resource_Manager.LastError[session];
        Socket_Resource_Manager.LastError[session] = null;
        return lastError;
    }

    /// <summary>Closes and disposes of the session</summary>
    public static int viClose(int session)
    {
        if (Socket_Resource_Manager.SessionsList.ContainsKey(session))
        {
            Visa_Socket sessions = Socket_Resource_Manager.SessionsList[session];
            Socket_Resource_Manager.SessionsList.Remove(session);
            Socket_Resource_Manager.LastError.Remove(session);
            if (sessions != null)
            {
                sessions.Disconnect();
                sessions.Dispose();
            }
        }
        return 0;
    }

    public static int viClear(int session) => 0;

    public static int viWrite(int session, byte[] buffer, uint length, out uint written)
    {
        written = 0U;
        try
        {
            written = (uint)Socket_Resource_Manager.SessionsList[session].Write(buffer, (int)length);
        }
        catch (Socket_Instrument_Exception ex)
        {
            SetLastError(session, ex.Message);
            return -5001;
        }
        return 0;
    }

    public static int viRead(int session, byte[] buffer, uint length, out uint read)
    {
        read = 0U;
        bool moreDataAvailable;
        try
        {
            read = (uint)Socket_Resource_Manager.SessionsList[session].Read(buffer, (int)length, out moreDataAvailable);
        }
        catch (Socket_Instrument_Exception ex)
        {
            SetLastError(session, ex.Message);
            return ex.Message.Contains("Read timeout") ? -1073807339 : -5001;
        }
        return !moreDataAvailable ? 0 : 1073676294;
    }

    public static int viSetAttribute(int session, uint attrName, UIntPtr attrValue)
    {
        switch (attrName)
        {
            case 1073676310:
                int uint32_1 = (int)attrValue.ToUInt32();
                return 0;
            case 1073676312:
                uint uint32_2 = attrValue.ToUInt32();
                Socket_Resource_Manager.SessionsList[session].TermChar = (char)uint32_2;
                return 0;
            case 1073676314:
                uint uint32_3 = attrValue.ToUInt32();
                Socket_Resource_Manager.SessionsList[session].Timeout = (int)uint32_3;
                return 0;
            case 1073676344:
                uint uint32_4 = attrValue.ToUInt32();
                Socket_Resource_Manager.SessionsList[session].TermCharEnable = uint32_4 > 0U;
                return 0;
            default:
                string lastError = string.Format("Attribute {0} is not supported or has an non-integer type", attrName);
                SetLastError(session, lastError);
                return -5001;
        }
    }

    public static int viGetAttribute(int session, uint attrName, out UIntPtr attrValue)
    {
        switch (attrName)
        {
            case 1073676310:
                attrValue = new UIntPtr(1U);
                return 0;
            case 1073676312:
                char termChar = Socket_Resource_Manager.SessionsList[session].TermChar;
                attrValue = new UIntPtr(termChar);
                return 0;
            case 1073676314:
                int timeout = Socket_Resource_Manager.SessionsList[session].Timeout;
                attrValue = new UIntPtr((uint)timeout);
                return 0;
            case 1073676344:
                bool termCharEnable = Socket_Resource_Manager.SessionsList[session].TermCharEnable;
                attrValue = new UIntPtr(termCharEnable ? 1U : 0U);
                return 0;
            case 1073676657:
                attrValue = new UIntPtr(6U);
                return 0;
            default:
                attrValue = new UIntPtr();
                string lastError = string.Format("Attribute {0} is not supported or has an non-integer type", attrName);
                SetLastError(session, lastError);
                return -5001;
        }
    }

    /// <summary>String type attributes reading</summary>
    public static int viGetAttribute(int session, uint attrName, StringBuilder attrValue)
    {
        if (attrName == 3221159937U)
        {
            attrValue.Append("SOCKET");
            return 0;
        }
        if (attrName == 3221160308U)
        {
            attrValue.Append("Rohde & Schwarz GmbH (Socket IO)");
            return 0;
        }
        string lastError = string.Format("Attribute {0} is not supported or has an non-string type", attrName);
        SetLastError(session, lastError);
        return -5001;
    }

    public static int viStatusDesc(int session, int status, byte[] buffer)
    {
        string s = string.Format("Unknown error {0:X}", status);
        if (status == -5001)
            s = GetLastError(session);
        Buffer.BlockCopy(Encoding.ASCII.GetBytes(s), 0, buffer, 0, s.Length);
        return 0;
    }

    public static int viFindRsrc(
      int rmSession,
      string expr,
      out int vi,
      out int retCount,
      StringBuilder desc)
    {
        vi = 0;
        retCount = 0;
        return 0;
    }

    public static int viFindNext(int rmSession, StringBuilder desc) => 0;
}
