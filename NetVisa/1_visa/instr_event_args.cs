namespace NetVisa;

public class Instr_Event_Args : Base_Event_Args
{
    /// <summary>
    /// Response data. The actual format depends on the type of the query
    /// </summary>
    public object Data { get; set; }

    /// <summary>Status Byte value at the time of invoking</summary>
    public Status_Byte StatusByte { get; set; }

    /// <summary>
    /// Initializes a new instance of the InstrEventArgs class.
    /// </summary>
    public Instr_Event_Args(string resourceName)
      : base(resourceName)
    {
        this.Data = null;
    }
}
