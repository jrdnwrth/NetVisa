namespace NetVisa;

public class SerialVisaSession : Visa_Session
{
    public uint BaudRate
    {
        get
        {
            resource_manager.driver.GetAttributeInt(this.Session.Handle, (uint)NativeVisaAttributes.SerialBaud, out var value);
            return (uint)value;
        }
        set
        {
            var baudRate = new UIntPtr(value);
            resource_manager.driver.SetAttribute(this.Session.Handle, (uint)NativeVisaAttributes.SerialBaud, baudRate);
        }
    }
    public uint DataBits
    {
        get
        {
            resource_manager.driver.GetAttributeInt(this.Session.Handle, (uint)NativeVisaAttributes.SerialDataBits, out var value);
            return (uint)value;
        }
        set
        {
            var dataBits = new UIntPtr(value);
            resource_manager.driver.SetAttribute(this.Session.Handle, (uint)NativeVisaAttributes.SerialDataBits, dataBits);
        }
    }

    public SerialParity Parity
    {
        get
        {
            resource_manager.driver.GetAttributeInt(this.Session.Handle, (uint)NativeVisaAttributes.SerialParity, out var value);
            return (SerialParity)value;
        }
        set
        {
            var parity = new UIntPtr((uint)value);
            resource_manager.driver.SetAttribute(this.Session.Handle, (uint)NativeVisaAttributes.SerialParity, parity);
        }
    }

    public SerialStopBitsMode StopBits
    {
        get
        {
            resource_manager.driver.GetAttributeInt(this.Session.Handle, (uint)NativeVisaAttributes.SerialStopBits, out var value);
            return (SerialStopBitsMode)value;
        }
        set
        {
            var stopBits = new UIntPtr((uint)value);
            resource_manager.driver.SetAttribute(this.Session.Handle, (uint)NativeVisaAttributes.SerialStopBits, stopBits);
        }
    }

    public SerialFlowControlModes FlowControl
    {
        get
        {
            resource_manager.driver.GetAttributeInt(this.Session.Handle, (uint)NativeVisaAttributes.SerialFlowControl, out var value);
            return (SerialFlowControlModes)value;
        }
        set
        {
            var flowControl = new UIntPtr((uint)value);
            resource_manager.driver.SetAttribute(this.Session.Handle, (uint)NativeVisaAttributes.SerialFlowControl, flowControl);
        }
    }

    public SerialVisaSession(string resourceName, Resource_Manager resource_manager) : base(resourceName, resource_manager)
    {
    }
}