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

    public int Handle { get; set; }

    /// <summary>Private constructor with all the parameters</summary>
    public Visa_Session_Handle(
      string resourceName
      )
    {
        this.ResourceName = resourceName;
    }

    public override string ToString()
    {
        return string.Format("VisaSessionHandle {0}", Handle);
    }
}
