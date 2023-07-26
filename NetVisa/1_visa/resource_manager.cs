using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetVisa;

/// <summary>
/// Visa Resource Manager
/// # WHEN TO CLOSE THE RESOURCE
/// Once you close() (aka Dispose()) this object, you can no longer talk to instruments.  
/// This is the same for pyvisa.  (It must be inherent to the visa64.dll.)
/// This means that you are likely to leave it open for the durration of your application.
/// This is hard to facilitate with the normal using/dispose mechanisms.  So instead
/// I provide a destructor that calls Dispose().  Even though that is normally not best practice (for performance reasons),
/// it works very well in this situation; you can create it and forget about it.  (Which seems to be
/// the approach used by pyvisa).  When your program terminates the connection will be closed.
/// We have a static reference to a single instance in this class so the garbage collector doesn't close it.
/// 
/// </summary>
public class Resource_Manager : IDisposable
{
    /// <summary>
    /// This will persist an instance of the resource manager for the life of the app.
    /// The destructor will close the connection when the application terminates.
    /// </summary>
    private static Resource_Manager? _instance;
    public static Resource_Manager Get
    {
        get
        {
            if (_instance is null)
                _instance = new Resource_Manager(new Driver(Visa_Plugin.NativeVisa));
            return _instance;
        }
    }

    /// <summary>Resource Manager handle</summary>
    public int handle { get; private set; }

    /// <summary>Visa Plugin</summary>
    public Driver driver { get; private set; }

    /// <summary>Visa Manufacturer String</summary>
    public string visa_manufacturer { get; private set; }

    /// <summary>
    /// Visa Resource Manager
    /// Make sure to using var resource_manager so the connection will be closed when completed, otherwise it WILL be left open.
    /// </summary>
    /// <param name="manager_name"></param>
    /// <param name="driver"></param>
    private Resource_Manager(Driver driver)
    {
        this.driver = driver;
        try
        {
            this.handle = open();
            this.visa_manufacturer = _GetVisaManufacturer();
        }
        catch (DllNotFoundException ex)
        {
            throw new DllNotFoundException("VISA dll could not be loaded. Make sure you have installed VISA: " + ex.Message);
        }

    }

    /// <summary>
    /// See the description at the top of this file.
    /// </summary>
    ~Resource_Manager()
    {
        this.Dispose();
    }

    public override string ToString() => string.Format("Visa Resource Manager Handle {0} (plugin {1})", handle, driver.visa_plugin);

    /// <summary>
    /// Opens the resource Manager and sets the:
    /// - Handle
    /// - VisaManufacturer
    /// </summary>
    public int open()
    {
        this._ThrowOnError(this.driver.OpenDefaultRm(out int handle), "Opening the Default VISA Resource Manager -");
        return handle;
    }

    /// <summary>Closes the Resource Manager</summary>
    public void close()
    {
        int num = this.driver.Close(this.handle);
    }

    /// <summary>
    /// IMPORTANT: This is not called by the garbage collector.  You must use a using statement or call it directly.
    /// ALSO: It is important to call it because the garbage collector obviously doesn't know to call the dll's .Close function.
    /// https://stackoverflow.com/questions/45036/will-the-garbage-collector-call-idisposable-dispose-for-me
    /// https://stackoverflow.com/questions/151051/when-should-i-use-gc-suppressfinalize
    /// </summary>
    public void Dispose()
    {
        this.close();
    }

    /// <summary>Error handler for all the VISA Exceptions</summary>
    /// <param name="status">Return value from VISA functions</param>
    /// <param name="context">Additional optional text</param>
    private void _ThrowOnError(int status, string context = "")
    {
        if (status < 0)
            throw new Visa_Exception("Resource Manager", (context.Trim() + string.Format(" VISA Resource Manager error: VISA manufacturer: {0}, DLL: {1}, error code 0x{2:X}: {3}", visa_manufacturer, driver.VisaDllName, status, this._GetVISAStatusDesc(status))).Trim());
    }

    /// <summary>Converts the status code into human-readable message</summary>
    /// <param name="status">Status code from VISA functions</param>
    private string _GetVISAStatusDesc(int status)
    {
        byte[] numArray = new byte[256];
        int num = this.driver.StatusDesc(this.handle, status, numArray);
        return Encoding.ASCII.GetString(numArray).TrimEnd(char.MinValue);
    }

    /// <summary>
    /// Returns VISA manufacturer of the current Resource Manager
    /// </summary>
    private string _GetVisaManufacturer()
    {
        StringBuilder attrValue = new StringBuilder(256);
        this._ThrowOnError(this.driver.GetAttributeString(this.handle, 3221160308U, attrValue), "Get VISA Resource Manager Manufacturer Name -");
        return attrValue.ToString();
    }

    /// <summary>Find all the resources fitting the search expression</summary>
    public IEnumerable<string> FindResources(string expression = "?*", bool vxi11 = true, bool lxi = true)
    {
        StringBuilder desc1 = new StringBuilder(1024);
        List<string> source = new List<string>();
        int vi = 0;
        int status1 = this.driver.SetAttribute(this.handle, 263127042U, (UIntPtr)(ulong)(1 | (vxi11 ? 2 : 0) | (lxi ? 4 : 0)));
        if (status1 == -1073807331)
            status1 = 0;
        this._ThrowOnError(status1);
        try
        {
            int retCount;
            int status2 = this.driver.FindRsrc(this.handle, expression, out vi, out retCount, desc1);
            if (status2 == -1073807343)
            {
                status2 = 0;
                retCount = 0;
            }
            this._ThrowOnError(status2, "VISA Find Resource -");
            if (retCount > 0)
                source.Add(desc1.ToString());
            if (retCount > 1)
            {
                for (; retCount > 1; --retCount)
                {
                    StringBuilder desc2 = new StringBuilder(1024);
                    this._ThrowOnError(this.driver.FindNext(vi, desc2), "VISA Find Next Resource -");
                    source.Add(desc2.ToString());
                }
            }
            int num = this.driver.Close(vi);
        }
        catch (Visa_Exception ex)
        {
            if (vi > 0)
            {
                int num = this.driver.Close(vi);
            }
            throw;
        }
        return source.Distinct().ToList();
    }
}
