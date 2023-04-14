using System;

namespace NetVisa;

public class Instrument_Exception : Exception
{
    public Instrument_Exception(string message)
      : base(message)
    {
    }
}

public class Socket_Instrument_Exception : Exception
{
    public Socket_Instrument_Exception(string message)
      : base(message)
    {

    }
}

