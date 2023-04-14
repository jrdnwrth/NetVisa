using System;
using System.Runtime.InteropServices;
using System.Text;

namespace NetVisa;

/// <summary>
/// VISA-C dll wrapper, provides delegates for all the required methods
/// </summary>
public class Driver
{
    public DelViOpenDefaultRm OpenDefaultRm;
    public DelViFindRsrc FindRsrc;
    public DelViFindNext FindNext;
    public DelViOpen Open;
    public DelViClose Close;
    public DelViGetAttributeInt GetAttributeInt;
    public DelViGetAttributeString GetAttributeString;
    public DelViClear Clear;
    public DelViWrite Write;
    public DelViRead Read;
    public DelViReadStb ReadStb;
    public DelViSetAttribute SetAttribute;
    public DelViStatusDesc StatusDesc;
    public DelViEnableEvent EnableEvent;
    public DelViDisableEvent DisableEvent;
    public DelViDiscardEvents DiscardEvents;
    public DelViWaitOnEvent WaitOnEvent;
    public DelViInstallHandler InstallHandler;
    public DelViUninstallHandler UninstallHandler;

    /// <summary>Returns the currently selected VISA plugin</summary>
    public Visa_Plugin visa_plugin { get; private set; }

    /// <summary>Returns loaded VISA library name</summary>
    public string VisaDllName { get; private set; }

    public Driver(Visa_Plugin visa_plugin)
    {
        switch (visa_plugin)
        {
            case Visa_Plugin.Visa_Socket:
                this.SelectVisa_Socket();
                break;
            default:
                this.SelectNativeVisa();
                break;
        }
    }

    public override string ToString() => this.VisaDllName;

    public void SelectNativeVisa()
    {
        this.visa_plugin = Visa_Plugin.NativeVisa;
        if (!Environment.Is64BitProcess)
            throw new Exception("Only Visa64.dll is supported.  Do not run this in 32 bit.");

        this.VisaDllName = "Visa64.dll (64-bit)";
        this.OpenDefaultRm = new DelViOpenDefaultRm(VisaNative64.viOpenDefaultRM);
        this.FindRsrc = new DelViFindRsrc(VisaNative64.viFindRsrc);
        this.FindNext = new DelViFindNext(VisaNative64.viFindNext);
        this.Open = new DelViOpen(VisaNative64.viOpen);
        this.Close = new DelViClose(VisaNative64.viClose);
        this.GetAttributeInt = new DelViGetAttributeInt(VisaNative64.viGetAttribute);
        this.GetAttributeString = new DelViGetAttributeString(VisaNative64.viGetAttribute);
        this.Clear = new DelViClear(VisaNative64.viClear);
        this.Write = new DelViWrite(VisaNative64.viWrite);
        this.Read = new DelViRead(VisaNative64.viRead);
        this.ReadStb = new DelViReadStb(VisaNative64.viReadSTB);
        this.SetAttribute = new DelViSetAttribute(VisaNative64.viSetAttribute);
        this.StatusDesc = new DelViStatusDesc(VisaNative64.viStatusDesc);
        this.EnableEvent = new DelViEnableEvent(VisaNative64.viEnableEvent);
        this.DisableEvent = new DelViDisableEvent(VisaNative64.viDisableEvent);
        this.DiscardEvents = new DelViDiscardEvents(VisaNative64.viDiscardEvents);
        this.WaitOnEvent = new DelViWaitOnEvent(VisaNative64.viWaitOnEvent);
        this.InstallHandler = new DelViInstallHandler(VisaNative64.viInstallHandler);
        this.UninstallHandler = new DelViUninstallHandler(VisaNative64.viUninstallHandler);
    }

    public void SelectVisa_Socket()
    {
        this.visa_plugin = Visa_Plugin.Visa_Socket;
        this.VisaDllName = "SocketIO";
        this.OpenDefaultRm = new DelViOpenDefaultRm(Socket_Io.viOpenDefaultRM);
        this.FindRsrc = new DelViFindRsrc(Socket_Io.viFindRsrc);
        this.FindNext = new DelViFindNext(Socket_Io.viFindNext);
        this.Open = new DelViOpen(Socket_Io.viOpen);
        this.Close = new DelViClose(Socket_Io.viClose);
        this.SetAttribute = new DelViSetAttribute(Socket_Io.viSetAttribute);
        this.StatusDesc = new DelViStatusDesc(Socket_Io.viStatusDesc);
        this.GetAttributeInt = new DelViGetAttributeInt(Socket_Io.viGetAttribute);
        this.GetAttributeString = new DelViGetAttributeString(Socket_Io.viGetAttribute);
        this.Clear = new DelViClear(Socket_Io.viClear);
        this.Write = new DelViWrite(Socket_Io.viWrite);
        this.Read = new DelViRead(Socket_Io.viRead);
        this.ReadStb = null;
        this.EnableEvent = null;
        this.DisableEvent = null;
        this.DiscardEvents = null;
        this.WaitOnEvent = null;
        this.InstallHandler = null;
        this.UninstallHandler = null;
    }

    public delegate int DelViOpenDefaultRm(out int rmSession);

    public delegate int DelViFindRsrc(
      int rmSession,
      string expr,
      out int vi,
      out int retCount,
      StringBuilder desc);

    public delegate int DelViFindNext(int rmSession, StringBuilder desc);

    public delegate int DelViOpen(
      int rmSession,
      string resource,
      uint accessMode,
      uint timeOut,
      out int session);

    public delegate int DelViClose(int session);

    public delegate int DelViGetAttributeInt(int session, uint attrName, out UIntPtr attrValue);

    public delegate int DelViGetAttributeString(
      int session,
      uint attrName,
      StringBuilder attrValue);

    public delegate int DelViClear(int session);

    public delegate int DelViWrite(int session, byte[] buffer, uint length, out uint written);

    public delegate int DelViRead(int session, byte[] buffer, uint length, out uint read);

    public delegate int DelViReadStb(int session, out uint status);

    public delegate int DelViSetAttribute(int session, uint attrName, UIntPtr attrValue);

    public delegate int DelViStatusDesc(int session, int status, byte[] buffer);

    public delegate int DelViEnableEvent(
      int session,
      uint eventType,
      short mechanism,
      int context);

    public delegate int DelViDisableEvent(int session, uint eventType, short mechanism);

    public delegate int DelViDiscardEvents(int session, uint eventType, short mechanism);

    public delegate int DelViWaitOnEvent(
      int session,
      uint inEventType,
      int timeout,
      out int outEventType,
      IntPtr outEventContext);

    public delegate int DelViInstallHandler(
      int session,
      uint inEventType,
      EventHandler inHandler,
      int inUserHandle);

    public delegate int DelViUninstallHandler(
      int vi,
      uint inEventType,
      EventHandler inHandler,
      int inUserHandle);

    public delegate int EventHandler(int vi, uint inEventType, int inContext, int inUserHandle);


    /// <summary>Wrapper for C - based Default VISA 64-bit</summary>
    public static class VisaNative64
    {
        /// <summary>VISA-C handler prototype</summary>
        public const string VisaDllName = "Visa64.dll";

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi)]
        public static extern int viOpenDefaultRM(out int rmSession);

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        public static extern int viFindRsrc(
          int rmSession,
          [MarshalAs(UnmanagedType.LPStr)] string expr,
          out int vi,
          out int retCount,
          StringBuilder desc);

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        public static extern int viFindNext(int rmSession, StringBuilder desc);

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        public static extern int viParseRsrcEx(
          int rmSession,
          [MarshalAs(UnmanagedType.LPStr)] string desc,
          out ushort intfType,
          out ushort intfNum,
          StringBuilder rsrcClass,
          StringBuilder expandedUnaliasedName,
          StringBuilder aliasIfExists);

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        public static extern int viOpen(
          int rmSession,
          [MarshalAs(UnmanagedType.LPStr)] string resource,
          uint accessMode,
          uint timeOut,
          out int session);

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi)]
        public static extern int viClose(int session);

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi)]
        public static extern int viClear(int session);

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi)]
        public static extern int viWrite(int session, [MarshalAs(UnmanagedType.LPArray)] byte[] buffer, uint length, out uint written);

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi)]
        public static extern int viRead(int session, [MarshalAs(UnmanagedType.LPArray)] byte[] buffer, uint length, out uint read);

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi)]
        public static extern int viReadSTB(int session, out uint status);

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi)]
        public static extern int viSetAttribute(int session, uint attrName, UIntPtr attrValue);

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi)]
        public static extern int viGetAttribute(int session, uint attrName, out UIntPtr attrValue);

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        public static extern int viGetAttribute(int session, uint attrName, [MarshalAs(UnmanagedType.LPStr)] StringBuilder attrValue);

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi)]
        public static extern int viStatusDesc(int session, int status, [MarshalAs(UnmanagedType.LPArray)] byte[] buffer);

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi)]
        public static extern int viEnableEvent(
          int session,
          uint eventType,
          short mechanism,
          int context);

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi)]
        public static extern int viDisableEvent(int session, uint eventType, short mechanism);

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi)]
        public static extern int viDiscardEvents(int session, uint eventType, short mechanism);

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi)]
        public static extern int viWaitOnEvent(
          int session,
          uint inEventType,
          int timeout,
          out int outEventType,
          IntPtr outEventContext);

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi)]
        public static extern int viInstallHandler(
          int session,
          uint inEventType,
          EventHandler inHandler,
          int inUserHandle);

        [DllImport("Visa64.dll", CharSet = CharSet.Ansi)]
        public static extern int viUninstallHandler(
          int vi,
          uint inEventType,
          EventHandler inHandler,
          int inUserHandle);
    }

}
