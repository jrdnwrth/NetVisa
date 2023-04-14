namespace NetVisa;

/// <summary>Constants used in all the VISA implementations</summary>
public static class Constants
{
    public const string Visa64DllName = "Visa64.dll";
    /// <summary>
    /// This is attribute names for function viSetAttribute and viGetAttribute
    /// </summary>
    public const uint ViAttrTmoValue = 1073676314;
    public const uint ViAttrIntfType = 1073676657;
    public const uint ViAttrTermCharEn = 1073676344;
    public const uint ViAttrTermChar = 1073676312;
    public const uint ViAttrRsrcClass = 3221159937;
    public const uint ViAttrRsrcManfName = 3221160308;
    public const uint ViAttrSendEndEn = 1073676310;
    public const uint ViAttrAsrlEndIn = 1073676467;
    public const uint ViAttrAsrlEndOut = 1073676468;
    public const uint ViAttrTcpipIsHislip = 1073677059;
    public const int ViSuccessMaxCnt = 1073676294;
    public const int ViErrorTmo = -1073807339;
    public const int ViErrorRsrcNotFound = -1073807343;
    public const int ViErrorInvalidRsrcName = -1073807342;
    public const int ViErrorInvalidObject = -1073807346;
    public const int ViErrorNsupAttr = -1073807331;
    public const int ViErrorDirectStatusMessage = -5001;
    public const int ViSuccess = 0;

    public enum EventMechanism
    {
        Queue = 1,
        Handler = 2,
        AllMech = 3,
    }

}
