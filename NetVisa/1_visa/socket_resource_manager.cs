using System;
using System.Collections.Generic;
using System.Linq;

namespace NetVisa;

public static class Socket_Resource_Manager
{
    /// <summary>List of RmSessions=VisaSessions and Visa_Socket Objects</summary>
    public static Dictionary<int, Visa_Socket> SessionsList = new Dictionary<int, Visa_Socket>();
    /// <summary>Last error assigned to the session</summary>
    public static Dictionary<int, string> LastError = new Dictionary<int, string>();

    /// <summary>
    /// Finds new Resource Manager and VISA session handle (starting from 1) and sets it up
    /// </summary>
    /// <returns></returns>
    public static int GetNewSessionHandle()
    {
        int key = 1;
        if (SessionsList.Count > 0)
            key = SessionsList.Max(x => x.Key) + 1;
        SessionsList[key] = null;
        LastError[key] = null;
        return key;
    }
}
