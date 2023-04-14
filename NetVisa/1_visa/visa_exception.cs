namespace NetVisa;

/// <summary>Exception thrown by the underlying VISA-C component</summary>
public class Visa_Exception : Instrument_Exception
{
    /// <summary>
    /// The only available constructor with the message parameters
    /// </summary>
    public Visa_Exception(string resourceString, string message)
      : base(message)
    {
    }
}
