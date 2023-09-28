namespace NetVisa;
public enum NativeVisaAttributes : uint
{
    /// <summary>
    /// Indicates the VI_ATTR_RSRC_IMPL_VERSION native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    ResourceImplementationVersion = 1073676291, // 0x3FFF0003
    /// <summary>
    /// Indicates the VI_ATTR_RSRC_LOCK_STATE native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    ResourceLockState = 1073676292, // 0x3FFF0004
    /// <summary>
    /// Indicates the VI_ATTR_MAX_QUEUE_LENGTH native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    MaximumEventQueueLength = 1073676293, // 0x3FFF0005
    /// <summary>
    /// Indicates the VI_ATTR_USER_DATA_32 native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    UserData32 = 1073676295, // 0x3FFF0007
    /// <summary>
    /// Indicates the VI_ATTR_FDC_CHNL native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    FastDataChannel = 1073676301, // 0x3FFF000D
    /// <summary>
    /// Indicates the VI_ATTR_FDC_MODE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    FastDataChannelMode = 1073676303, // 0x3FFF000F
    /// <summary>
    /// Indicates the VI_ATTR_FDC_USE_PAIR native VISA attribute. Use with GetAttributeBoolean.
    /// </summary>
    FastDataChannelUsePair = 1073676307, // 0x3FFF0013
    /// <summary>
    /// Indicates the VI_ATTR_SEND_END_EN native VISA attribute. Use with GetAttributeBoolean.
    /// </summary>
    SendEndEnabled = 1073676310, // 0x3FFF0016
    /// <summary>
    /// Indicates the VI_ATTR_TERMCHAR native VISA attribute. Use with GetAttributeByte.
    /// </summary>
    TerminationCharacter = 1073676312, // 0x3FFF0018
    /// <summary>
    /// Indicates the VI_ATTR_TMO_VALUE native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    TimeoutValue = 1073676314, // 0x3FFF001A
    /// <summary>
    /// Indicates the VI_ATTR_GPIB_READDR_EN native VISA attribute. Use with GetAttributeBoolean.
    /// </summary>
    GpibRepeatAddressingEnabled = 1073676315, // 0x3FFF001B
    /// <summary>
    /// Indicates the VI_ATTR_IO_PROT native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    IOProtocol = 1073676316, // 0x3FFF001C
    /// <summary>
    /// Indicates the VI_ATTR_DMA_ALLOW_EN native VISA attribute. Use with GetAttributeBoolean.
    /// </summary>
    AllowDma = 1073676318, // 0x3FFF001E
    /// <summary>
    /// Indicates the VI_ATTR_ASRL_BAUD native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    SerialBaud = 1073676321, // 0x3FFF0021
    /// <summary>
    /// Indicates the VI_ATTR_ASRL_DATA_BITS native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    SerialDataBits = 1073676322, // 0x3FFF0022
    /// <summary>
    /// Indicates the VI_ATTR_ASRL_PARITY native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    SerialParity = 1073676323, // 0x3FFF0023
    /// <summary>
    /// Indicates the VI_ATTR_ASRL_STOP_BITS native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    SerialStopBits = 1073676324, // 0x3FFF0024
    /// <summary>
    /// Indicates the VI_ATTR_ASRL_FLOW_CNTRL native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    SerialFlowControl = 1073676325, // 0x3FFF0025
    /// <summary>
    /// Indicates the VI_ATTR_RD_BUF_OPER_MODE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    ReadBufferOperationMode = 1073676330, // 0x3FFF002A
    /// <summary>
    /// Indicates the VI_ATTR_RD_BUF_SIZE native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    ReadBufferSize = 1073676331, // 0x3FFF002B
    /// <summary>
    /// Indicates the VI_ATTR_WR_BUF_OPER_MODE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    WriteBufferOperationMode = 1073676333, // 0x3FFF002D
    /// <summary>
    /// Indicates the VI_ATTR_WR_BUF_SIZE native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    WriteBufferSize = 1073676334, // 0x3FFF002E
    /// <summary>
    /// Indicates the VI_ATTR_SUPPRESS_END_EN native VISA attribute. Use with GetAttributeBoolean.
    /// </summary>
    SuppressEndEnabled = 1073676342, // 0x3FFF0036
    /// <summary>
    /// Indicates the VI_ATTR_TERMCHAR_EN native VISA attribute. Use with GetAttributeBoolean.
    /// </summary>
    TerminationCharacterEnabled = 1073676344, // 0x3FFF0038
    /// <summary>
    /// Indicates the VI_ATTR_DEST_ACCESS_PRIV native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    DestinationAccess = 1073676345, // 0x3FFF0039
    /// <summary>
    /// Indicates the VI_ATTR_DEST_BYTE_ORDER native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    DestinationByteOrder = 1073676346, // 0x3FFF003A
    /// <summary>
    /// Indicates the VI_ATTR_SRC_ACCESS_PRIV native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    SourceAccess = 1073676348, // 0x3FFF003C
    /// <summary>
    /// Indicates the VI_ATTR_SRC_BYTE_ORDER native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    SourceByteOrder = 1073676349, // 0x3FFF003D
    /// <summary>
    /// Indicates the VI_ATTR_SRC_INCREMENT native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    SourceIncrement = 1073676352, // 0x3FFF0040
    /// <summary>
    /// Indicates the VI_ATTR_DEST_INCREMENT native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    DestinationIncrement = 1073676353, // 0x3FFF0041
    /// <summary>
    /// Indicates the VI_ATTR_WIN_ACCESS_PRIV native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    WindowAccessPrivilege = 1073676357, // 0x3FFF0045
    /// <summary>
    /// Indicates the VI_ATTR_WIN_BYTE_ORDER native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    WindowByteOrder = 1073676359, // 0x3FFF0047
    /// <summary>
    /// Indicates the VI_ATTR_GPIB_ATN_STATE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    GpibAtnState = 1073676375, // 0x3FFF0057
    /// <summary>
    /// Indicates the VI_ATTR_GPIB_ADDR_STATE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    GpibAddressedState = 1073676380, // 0x3FFF005C
    /// <summary>
    /// Indicates the VI_ATTR_GPIB_CIC_STATE native VISA attribute. Use with GetAttributeBoolean.
    /// </summary>
    GpibIsControllerInCharge = 1073676382, // 0x3FFF005E
    /// <summary>
    /// Indicates the VI_ATTR_GPIB_NDAC_STATE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    GpibNdacState = 1073676386, // 0x3FFF0062
    /// <summary>
    /// Indicates the VI_ATTR_GPIB_SRQ_STATE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    GpibSrqState = 1073676391, // 0x3FFF0067
    /// <summary>
    /// Indicates the VI_ATTR_GPIB_SYS_CNTRL_STATE native VISA attribute. Use with GetAttributeBoolean.
    /// </summary>
    GpibIsSystemController = 1073676392, // 0x3FFF0068
    /// <summary>
    /// Indicates the VI_ATTR_GPIB_HS488_CBL_LEN native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    GpibHS488CableLength = 1073676393, // 0x3FFF0069
    /// <summary>
    /// Indicates the VI_ATTR_CMDR_LA native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    CommanderLogicalAddress = 1073676395, // 0x3FFF006B
    /// <summary>
    /// Indicates the VI_ATTR_VXI_DEV_CLASS native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    VxiDeviceClass = 1073676396, // 0x3FFF006C
    /// <summary>
    /// Indicates the VI_ATTR_MAINFRAME_LA native VISA attribute. Use with GetAttributeInt16.
    /// This corresponds to ChassisLogicalAddress in the IVxiSession and IVxiBackplaneSession
    /// interfaces.
    /// </summary>
    MainframeLogicalAddress = 1073676400, // 0x3FFF0070
    /// <summary>
    /// Indicates the VI_ATTR_VXI_VME_INTR_STATUS native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    VxiVmeInterruptStatus = 1073676427, // 0x3FFF008B
    /// <summary>
    /// Indicates the VI_ATTR_VXI_TRIG_STATUS native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    VxiTriggerStatus = 1073676429, // 0x3FFF008D
    /// <summary>
    /// Indicates the VI_ATTR_VXI_VME_SYSFAIL_STATE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    VxiVmeSystemFailureState = 1073676436, // 0x3FFF0094
    /// <summary>
    /// Indicates the VI_ATTR_WIN_BASE_ADDR_32 native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    WindowBaseAddress32 = 1073676440, // 0x3FFF0098
    /// <summary>
    /// Indicates the VI_ATTR_WIN_SIZE_32 native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    WindowSize32 = 1073676442, // 0x3FFF009A
    /// <summary>
    /// Indicates the VI_ATTR_WIN_BASE_ADDR_64 native VISA attribute. Use with GetAttributeInt64.
    /// </summary>
    WindowBaseAddress64 = 1073676443, // 0x3FFF009B
    /// <summary>
    /// Indicates the VI_ATTR_WIN_SIZE_64 native VISA attribute. Use with GetAttributeInt64.
    /// </summary>
    WindowSize64 = 1073676444, // 0x3FFF009C
    /// <summary>
    /// Indicates the VI_ATTR_ASRL_AVAIL_NUM native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    SerialAvailableByteCount = 1073676460, // 0x3FFF00AC
    /// <summary>
    /// Indicates the VI_ATTR_MEM_BASE_32 native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    MemoryBase32 = 1073676461, // 0x3FFF00AD
    /// <summary>
    /// Indicates the VI_ATTR_ASRL_CTS_STATE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    SerialCtsState = 1073676462, // 0x3FFF00AE
    /// <summary>
    /// Indicates the VI_ATTR_ASRL_DCD_STATE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    SerialDcdState = 1073676463, // 0x3FFF00AF
    /// <summary>
    /// Indicates the VI_ATTR_ASRL_DSR_STATE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    SerialDsrState = 1073676465, // 0x3FFF00B1
    /// <summary>
    /// Indicates the VI_ATTR_ASRL_DTR_STATE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    SerialDtrState = 1073676466, // 0x3FFF00B2
    /// <summary>
    /// Indicates the VI_ATTR_ASRL_END_IN native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    SerialEndIn = 1073676467, // 0x3FFF00B3
    /// <summary>
    /// Indicates the VI_ATTR_ASRL_END_OUT native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    SerialEndOut = 1073676468, // 0x3FFF00B4
    /// <summary>
    /// Indicates the VI_ATTR_ASRL_REPLACE_CHAR native VISA attribute. Use with GetAttributeByte.
    /// </summary>
    SerialReplaceCharacter = 1073676478, // 0x3FFF00BE
    /// <summary>
    /// Indicates the VI_ATTR_ASRL_RI_STATE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    SerialRIState = 1073676479, // 0x3FFF00BF
    /// <summary>
    /// Indicates the VI_ATTR_ASRL_RTS_STATE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    SerialRtsState = 1073676480, // 0x3FFF00C0
    /// <summary>
    /// Indicates the VI_ATTR_ASRL_XON_CHAR native VISA attribute. Use with GetAttributeByte.
    /// </summary>
    SerialXOnCharacter = 1073676481, // 0x3FFF00C1
    /// <summary>
    /// Indicates the VI_ATTR_ASRL_XOFF_CHAR native VISA attribute. Use with GetAttributeByte.
    /// </summary>
    SerialXOffCharacter = 1073676482, // 0x3FFF00C2
    /// <summary>
    /// Indicates the VI_ATTR_WIN_ACCESS native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    WindowAccess = 1073676483, // 0x3FFF00C3
    /// <summary>
    /// Indicates the VI_ATTR_RM_SESSION native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    ResourceManagerSession = 1073676484, // 0x3FFF00C4
    /// <summary>
    /// Indicates the VI_ATTR_MEM_BASE_64 native VISA attribute. Use with GetAttributeInt64.
    /// </summary>
    MemoryBase64 = 1073676496, // 0x3FFF00D0
    /// <summary>
    /// Indicates the VI_ATTR_MEM_SIZE_64 native VISA attribute. Use with GetAttributeInt64.
    /// </summary>
    MemorySize64 = 1073676497, // 0x3FFF00D1
    /// <summary>
    /// Indicates the VI_ATTR_VXI_LA native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    VxiLogicalAddress = 1073676501, // 0x3FFF00D5
    /// <summary>
    /// Indicates the VI_ATTR_MANF_ID native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    ManufacturerId = 1073676505, // 0x3FFF00D9
    /// <summary>
    /// Indicates the VI_ATTR_MEM_SIZE_32 native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    MemorySize32 = 1073676509, // 0x3FFF00DD
    /// <summary>
    /// Indicates the VI_ATTR_MEM_SPACE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    MemorySpace = 1073676510, // 0x3FFF00DE
    /// <summary>
    /// Indicates the VI_ATTR_MODEL_CODE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    ModelCode = 1073676511, // 0x3FFF00DF
    /// <summary>
    /// Indicates the VI_ATTR_SLOT native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    Slot = 1073676520, // 0x3FFF00E8
    /// <summary>
    /// Indicates the VI_ATTR_IMMEDIATE_SERV native VISA attribute. Use with GetAttributeBoolean.
    /// </summary>
    ImmediateServant = 1073676544, // 0x3FFF0100
    /// <summary>
    /// Indicates the VI_ATTR_INTF_PARENT_NUM native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    InterfaceParentNumber = 1073676545, // 0x3FFF0101
    /// <summary>
    /// Indicates the VI_ATTR_RSRC_SPEC_VERSION native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    ResourceSpecificationVersion = 1073676656, // 0x3FFF0170
    /// <summary>
    /// Indicates the VI_ATTR_INTF_TYPE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    InterfaceType = 1073676657, // 0x3FFF0171
    /// <summary>
    /// Indicates the VI_ATTR_GPIB_PRIMARY_ADDR native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    GpibPrimaryAddress = 1073676658, // 0x3FFF0172
    /// <summary>
    /// Indicates the VI_ATTR_GPIB_SECONDARY_ADDR native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    GpibSecondaryAddress = 1073676659, // 0x3FFF0173
    /// <summary>
    /// Indicates the VI_ATTR_RSRC_MANF_ID native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    ResourceManufacturerId = 1073676661, // 0x3FFF0175
    /// <summary>
    /// Indicates the VI_ATTR_INTF_NUM native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    InterfaceNumber = 1073676662, // 0x3FFF0176
    /// <summary>
    /// Indicates the VI_ATTR_TRIG_ID native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    TriggerId = 1073676663, // 0x3FFF0177
    /// <summary>
    /// Indicates the VI_ATTR_GPIB_REN_STATE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    GpibRenState = 1073676673, // 0x3FFF0181
    /// <summary>
    /// Indicates the VI_ATTR_GPIB_UNADDR_EN native VISA attribute. Use with GetAttributeBoolean.
    /// </summary>
    GpibUnaddressEnabled = 1073676676, // 0x3FFF0184
    /// <summary>
    /// Indicates the VI_ATTR_DEV_STATUS_BYTE native VISA attribute. Use with GetAttributeByte.
    /// </summary>
    DeviceStatusByte = 1073676681, // 0x3FFF0189
    /// <summary>
    /// Indicates the VI_ATTR_FILE_APPEND_EN native VISA attribute. Use with GetAttributeBoolean.
    /// </summary>
    FileAppendEnabled = 1073676690, // 0x3FFF0192
    /// <summary>
    /// Indicates the VI_ATTR_VXI_TRIG_SUPPORT native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    VxiTriggerSupport = 1073676692, // 0x3FFF0194
    /// <summary>
    /// Indicates the VI_ATTR_TCPIP_PORT native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    TcpPort = 1073676695, // 0x3FFF0197
    /// <summary>
    /// Indicates the VI_ATTR_TCPIP_NODELAY native VISA attribute. Use with GetAttributeBoolean.
    /// </summary>
    TcpNoDelay = 1073676698, // 0x3FFF019A
    /// <summary>
    /// Indicates the VI_ATTR_TCPIP_KEEPALIVE native VISA attribute. Use with GetAttributeBoolean.
    /// </summary>
    TcpKeepAlive = 1073676699, // 0x3FFF019B
    /// <summary>
    /// Indicates the VI_ATTR_4882_COMPLIANT native VISA attribute. Use with GetAttributeBoolean.
    /// </summary>
    Is4882Compliant = 1073676703, // 0x3FFF019F
    /// <summary>
    /// Indicates the VI_ATTR_USB_INTFC_NUM native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    UsbInterfaceNumber = 1073676705, // 0x3FFF01A1
    /// <summary>
    /// Indicates the VI_ATTR_USB_PROTOCOL native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    UsbProtocol = 1073676711, // 0x3FFF01A7
    /// <summary>
    /// Indicates the VI_ATTR_USB_MAX_INTR_SIZE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    UsbMaximumInterruptSize = 1073676719, // 0x3FFF01AF
    /// <summary>
    /// Indicates the VI_ATTR_PXI_DEV_NUM native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiDeviceNumber = 1073676801, // 0x3FFF0201
    /// <summary>
    /// Indicates the VI_ATTR_PXI_FUNC_NUM native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiFunctionNumber = 1073676802, // 0x3FFF0202
    /// <summary>
    /// Indicates the VI_ATTR_PXI_BUS_NUM native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiBusNumber = 1073676805, // 0x3FFF0205
    /// <summary>
    /// Indicates the VI_ATTR_PXI_CHASSIS native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiChassis = 1073676806, // 0x3FFF0206
    /// <summary>
    /// Indicates the VI_ATTR_PXI_SLOT_LBUS_LEFT native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiSlotLocalBusLeft = 1073676808, // 0x3FFF0208
    /// <summary>
    /// Indicates the VI_ATTR_PXI_SLOT_LBUS_RIGHT native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiSlotLocalBusRight = 1073676809, // 0x3FFF0209
    /// <summary>
    /// Indicates the VI_ATTR_PXI_TRIG_BUS native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiTriggerBus = 1073676810, // 0x3FFF020A
    /// <summary>
    /// Indicates the VI_ATTR_PXI_STAR_TRIG_BUS native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiStarTriggerBus = 1073676811, // 0x3FFF020B
    /// <summary>
    /// Indicates the VI_ATTR_PXI_STAR_TRIG_LINE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiStarTriggerLine = 1073676812, // 0x3FFF020C
    /// <summary>
    /// Indicates the VI_ATTR_PXI_SRC_TRIG_BUS native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiBackplaneSourceTriggerBus = 1073676813, // 0x3FFF020D
    /// <summary>
    /// Indicates the VI_ATTR_PXI_DEST_TRIG_BUS native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiBackplaneDestinationTriggerBus = 1073676814, // 0x3FFF020E
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_TYPE_BAR0 native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiMemoryTypeBar0 = 1073676817, // 0x3FFF0211
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_TYPE_BAR1 native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiMemoryTypeBar1 = 1073676818, // 0x3FFF0212
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_TYPE_BAR2 native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiMemoryTypeBar2 = 1073676819, // 0x3FFF0213
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_TYPE_BAR3 native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiMemoryTypeBar3 = 1073676820, // 0x3FFF0214
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_TYPE_BAR4 native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiMemoryTypeBar4 = 1073676821, // 0x3FFF0215
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_TYPE_BAR5 native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiMemoryTypeBar5 = 1073676822, // 0x3FFF0216
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_BASE_BAR0_32 native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    PxiMemoryBase32Bar0 = 1073676833, // 0x3FFF0221
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_BASE_BAR1_32 native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    PxiMemoryBase32Bar1 = 1073676834, // 0x3FFF0222
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_BASE_BAR2_32 native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    PxiMemoryBase32Bar2 = 1073676835, // 0x3FFF0223
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_BASE_BAR3_32 native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    PxiMemoryBase32Bar3 = 1073676836, // 0x3FFF0224
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_BASE_BAR4_32 native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    PxiMemoryBase32Bar4 = 1073676837, // 0x3FFF0225
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_BASE_BAR5_32 native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    PxiMemoryBase32Bar5 = 1073676838, // 0x3FFF0226
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_BASE_BAR0_64 native VISA attribute. Use with GetAttributeInt64.
    /// </summary>
    PxiMemoryBase64Bar0 = 1073676840, // 0x3FFF0228
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_BASE_BAR1_64 native VISA attribute. Use with GetAttributeInt64.
    /// </summary>
    PxiMemoryBase64Bar1 = 1073676841, // 0x3FFF0229
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_BASE_BAR2_64 native VISA attribute. Use with GetAttributeInt64.
    /// </summary>
    PxiMemoryBase64Bar2 = 1073676842, // 0x3FFF022A
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_BASE_BAR3_64 native VISA attribute. Use with GetAttributeInt64.
    /// </summary>
    PxiMemoryBase64Bar3 = 1073676843, // 0x3FFF022B
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_BASE_BAR4_64 native VISA attribute. Use with GetAttributeInt64.
    /// </summary>
    PxiMemoryBase64Bar4 = 1073676844, // 0x3FFF022C
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_BASE_BAR5_64 native VISA attribute. Use with GetAttributeInt64.
    /// </summary>
    PxiMemoryBase64Bar5 = 1073676845, // 0x3FFF022D
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_SIZE_BAR0_32 native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    PxiMemorySize32Bar0 = 1073676849, // 0x3FFF0231
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_SIZE_BAR1_32 native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    PxiMemorySize32Bar1 = 1073676850, // 0x3FFF0232
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_SIZE_BAR2_32 native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    PxiMemorySize32Bar2 = 1073676851, // 0x3FFF0233
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_SIZE_BAR3_32 native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    PxiMemorySize32Bar3 = 1073676852, // 0x3FFF0234
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_SIZE_BAR4_32 native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    PxiMemorySize32Bar4 = 1073676853, // 0x3FFF0235
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_SIZE_BAR5_32 native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    PxiMemorySize32Bar5 = 1073676854, // 0x3FFF0236
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_SIZE_BAR0_64 native VISA attribute. Use with GetAttributeInt64.
    /// </summary>
    PxiMemorySize64Bar0 = 1073676856, // 0x3FFF0238
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_SIZE_BAR1_64 native VISA attribute. Use with GetAttributeInt64.
    /// </summary>
    PxiMemorySize64Bar1 = 1073676857, // 0x3FFF0239
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_SIZE_BAR2_64 native VISA attribute. Use with GetAttributeInt64.
    /// </summary>
    PxiMemorySize64Bar2 = 1073676858, // 0x3FFF023A
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_SIZE_BAR3_64 native VISA attribute. Use with GetAttributeInt642.
    /// </summary>
    PxiMemorySize64Bar3 = 1073676859, // 0x3FFF023B
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_SIZE_BAR4_64 native VISA attribute. Use with GetAttributeInt64.
    /// </summary>
    PxiMemorySize64Bar4 = 1073676860, // 0x3FFF023C
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MEM_SIZE_BAR5_64 native VISA attribute. Use with GetAttributeInt64.
    /// </summary>
    PxiMemorySize64Bar5 = 1073676861, // 0x3FFF023D
    /// <summary>
    /// Indicates the VI_ATTR_PXI_IS_EXPRESS native VISA attribute. Use with GetAttributeBoolean.
    /// </summary>
    PxiIsExpress = 1073676864, // 0x3FFF0240
    /// <summary>
    /// Indicates the VI_ATTR_PXI_SLOT_LWIDTH native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiSlotLinkWidth = 1073676865, // 0x3FFF0241
    /// <summary>
    /// Indicates the VI_ATTR_PXI_MAX_LWIDTH native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiMaximumLinkWidth = 1073676866, // 0x3FFF0242
    /// <summary>
    /// Indicates the VI_ATTR_PXI_ACTUAL_LWIDTH native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiActualLinkWidth = 1073676867, // 0x3FFF0243
    /// <summary>
    /// Indicates the VI_ATTR_PXI_DSTAR_BUS native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiDStarBus = 1073676868, // 0x3FFF0244
    /// <summary>
    /// Indicates the VI_ATTR_PXI_DSTAR_SET native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiDStarSet = 1073676869, // 0x3FFF0245
    /// <summary>
    /// Indicates the VI_ATTR_PXI_ALLOW_WRITE_COMBINE native VISA attribute. Use with GetAttributeBoolean.
    /// </summary>
    AllowWriteCombining = 1073676870, // 0x3FFF0246
    /// <summary>
    /// Indicates the VI_ATTR_TCPIP_HISLIP_OVERLAP_EN native VISA attribute. Use with GetAttributeBoolean.
    /// </summary>
    TcpHiSLIPOverlapEnabled = 1073677056, // 0x3FFF0300
    /// <summary>
    /// Indicates the VI_ATTR_TCPIP_HISLIP_VERSION native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    TcpHiSLIPVersion = 1073677057, // 0x3FFF0301
    /// <summary>
    /// Indicates the VI_ATTR_TCPIP_HISLIP_MAX_MESSAGE_KB native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    TcpHiSLIPMaximumMessageSizeKB = 1073677058, // 0x3FFF0302
    /// <summary>
    /// Indicates the VI_ATTR_TCPIP_IS_HISLIP native VISA attribute. Use with GetAttributeBoolean.
    /// </summary>
    TcpIsHiSLIP = 1073677059, // 0x3FFF0303
    /// <summary>
    /// Indicates the VI_ATTR_JOB_ID native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    JobId = 1073692678, // 0x3FFF4006
    /// <summary>
    /// Indicates the VI_ATTR_EVENT_TYPE native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    EventType = 1073692688, // 0x3FFF4010
    /// <summary>
    /// Indicates the VI_ATTR_RECV_TRIG_ID native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    ReceivedTriggerId = 1073692690, // 0x3FFF4012
    /// <summary>
    /// Indicates the VI_ATTR_INTR_STATUS_ID native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    ReceivedInterruptStatusId = 1073692707, // 0x3FFF4023
    /// <summary>
    /// Indicates the VI_ATTR_STATUS native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    Status = 1073692709, // 0x3FFF4025
    /// <summary>
    /// Indicates the VI_ATTR_RET_COUNT_32 native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    AsyncReturnCount32 = 1073692710, // 0x3FFF4026
    /// <summary>
    /// Indicates the VI_ATTR_RET_COUNT_64 native VISA attribute. Use with GetAttributeInt64.
    /// </summary>
    AsyncReturnCount64 = 1073692712, // 0x3FFF4028
    /// <summary>
    /// Indicates the VI_ATTR_RECV_INTR_LEVEL native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    ReceivedInterruptLevel = 1073692737, // 0x3FFF4041
    /// <summary>
    /// Indicates the VI_ATTR_GPIB_RECV_CIC_STATE native VISA attribute. Use with GetAttributeBoolean.
    /// </summary>
    GpibReceivedIsControllerInCharge = 1073693075, // 0x3FFF4193
    /// <summary>
    /// Indicates the VI_ATTR_USB_RECV_INTR_SIZE native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    UsbReceivedInterruptSize = 1073693104, // 0x3FFF41B0
    /// <summary>
    /// Indicates the VI_ATTR_PXI_RECV_INTR_SEQ native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    PxiReceivedInterruptSequence = 1073693248, // 0x3FFF4240
    /// <summary>
    /// Indicates the VI_ATTR_PXI_RECV_INTR_DATA native VISA attribute. Use with GetAttributeInt32.
    /// </summary>
    PxiReceivedInterruptData = 1073693249, // 0x3FFF4241
    /// <summary>
    /// Indicates the VI_ATTR_RSRC_CLASS native VISA attribute. Use with GetAttributeString.
    /// </summary>
    ResourceClass = 3221159937, // 0xBFFF0001
    /// <summary>
    /// Indicates the VI_ATTR_RSRC_NAME native VISA attribute. Use with GetAttributeString.
    /// </summary>
    ResourceName = 3221159938, // 0xBFFF0002
    /// <summary>
    /// Indicates the VI_ATTR_MANF_NAME native VISA attribute. Use with GetAttributeString.
    /// </summary>
    ManufacturerName = 3221160050, // 0xBFFF0072
    /// <summary>
    /// Indicates the VI_ATTR_MODEL_NAME native VISA attribute. Use with GetAttributeString.
    /// </summary>
    ModelName = 3221160055, // 0xBFFF0077
    /// <summary>
    /// Indicates the VI_ATTR_INTF_INST_NAME native VISA attribute. Use with GetAttributeString.
    /// </summary>
    InterfaceName = 3221160169, // 0xBFFF00E9
    /// <summary>
    /// Indicates the VI_ATTR_RSRC_MANF_NAME native VISA attribute. Use with GetAttributeString.
    /// </summary>
    ResourceManufacturerName = 3221160308, // 0xBFFF0174
    /// <summary>
    /// Indicates the VI_ATTR_TCPIP_ADDR native VISA attribute. Use with GetAttributeString.
    /// </summary>
    TcpAddress = 3221160341, // 0xBFFF0195
    /// <summary>
    /// Indicates the VI_ATTR_TCPIP_HOSTNAME native VISA attribute. Use with GetAttributeString.
    /// </summary>
    TcpHostName = 3221160342, // 0xBFFF0196
    /// <summary>
    /// Indicates the VI_ATTR_TCPIP_DEVICE_NAME native VISA attribute. Use with GetAttributeString.
    /// </summary>
    TcpDeviceName = 3221160345, // 0xBFFF0199
    /// <summary>
    /// Indicates the VI_ATTR_USB_SERIAL_NUM native VISA attribute. Use with GetAttributeString.
    /// </summary>
    UsbSerialNumber = 3221160352, // 0xBFFF01A0
    /// <summary>
    /// Indicates the VI_ATTR_PXI_SLOTPATH native VISA attribute. Use with GetAttributeString.
    /// </summary>
    PxiSlotPath = 3221160455, // 0xBFFF0207
    /// <summary>
    /// Indicates the VI_ATTR_SIGP_STATUS_ID native VISA attribute. Use with GetAttributeInt16.
    /// </summary>
    ReceivedSignalProcessorStatusId = 3221176337, // 0xBFFF4011
    /// <summary>
    /// Indicates the VI_ATTR_OPER_NAME native VISA attribute. Use with GetAttributeString.
    /// </summary>
    OperationName = 3221176386, // 0xBFFF4042
    /// <summary>
    /// Indicates the VI_ATTR_RECV_TCPIP_ADDR native VISA attribute. Use with GetAttributeString.
    /// </summary>
    ReceivedTcpAddress = 3221176728, // 0xBFFF4198
}
