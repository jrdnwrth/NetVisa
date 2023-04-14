using System;
using System.Globalization;

namespace NetVisa;

public class Base_Event_Args : EventArgs
{
    /// <summary>Resource name of the session that generated the event</summary>
    public string ResourceName { get; }

    /// <summary>Context of the event. Usually the sent SCPI command.</summary>
    public string Context { get; set; }

    /// <summary>Time of beginning of the operation</summary>
    public DateTime StartTimestamp { get; set; }

    /// <summary>Duration of the operation</summary>
    public TimeSpan Duration { get; private set; }

    /// <summary>
    /// Initializes a new instance of the InstrEventArgs class.
    /// </summary>
    public Base_Event_Args(string resourceName)
    {
        this.ResourceName = resourceName;
        this.Duration = TimeSpan.Zero;
    }

    /// <summary>String representation of the class</summary>
    public override string ToString()
    {
        string str = "EventArgs '" + this.ResourceName + "'";
        if (this.Context != null)
            str = str + ", context: " + this.Context;
        if (this.Duration != TimeSpan.Zero)
            str += string.Format(CultureInfo.InvariantCulture, ", duration: {0:F4} secs", Duration.TotalSeconds);
        return str;
    }

    /// <summary>Sets duration of the event operation as TimeSpan</summary>
    public void SetDuration(TimeSpan duration) => this.Duration = duration;

    /// <summary>
    /// Sets duration of the event operation
    /// by calculating it from the entered time and the StartTimestamp
    /// </summary>
    public void SetDuration(DateTime time) => this.Duration = time - this.StartTimestamp;

    /// <summary>
    /// Sets duration of the event operation
    /// by calculating it from the time now and the StartTimestamp
    /// </summary>
    public void SetDuration() => this.Duration = DateTime.Now - this.StartTimestamp;
}
