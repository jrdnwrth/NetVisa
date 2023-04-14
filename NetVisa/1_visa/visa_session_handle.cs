using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NetVisa;

/// <summary>Handle for VISA communication</summary>
public class Visa_Session_Handle
{
    /// <summary>Full Resource name including plugins</summary>
    public string ResourceName { get; private set; }

    /// <summary>Resource name stripped of the round brackets part</summary>
    public string VisaResourceName { get; private set; }

    public int Handle { get; set; }

    /// <summary>Private constructor with all the parameters</summary>
    private Visa_Session_Handle(
      string resourceName,
      int rmHandle,
      int handle,
      Visa_Plugin plugin
      )
    {
        this.ResourceName = resourceName;
        this.VisaResourceName = resourceName;
        this.Handle = handle;
    }

    /// <summary>Standard Session Handle</summary>
    public Visa_Session_Handle(string resourceName, Visa_Plugin plugin)
      : this(resourceName, 0, 0, plugin)
    {
    }

    public override string ToString()
    {
        return string.Format("VisaSessionHandle {0}", Handle);
    }
}
