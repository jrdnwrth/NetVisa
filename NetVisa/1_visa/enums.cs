using System;

namespace NetVisa;

/// <summary>Instrument's Status Byte flags</summary>
[Flags]
public enum Status_Byte
{
    /// <summary>Null value</summary>
    None = 0,
    /// <summary>
    /// Signals that the instrument's error queue contains at least one entry
    /// </summary>
    ErrorQueueNotEmpty = 4,
    /// <summary>Summary of Questionable Status register</summary>
    QuestionableStatusReg = 8,
    /// <summary>Instrument's output IO buffer is not empty</summary>
    MessageAvailable = 16, 
    /// <summary>
    /// Summary bit of Event Status Register filtered through the ESE byte
    /// </summary>
    EventStatusByte = 32,
    /// <summary>Service request was triggered</summary>
    RequestService = 64, 
    /// <summary>Summary of Operation Status register</summary>
    OperationStatusReg = 128,
}

/// <summary>Defines Visa Plugin</summary>
public enum Visa_Plugin
{
    NativeVisa,
    Visa_Socket,
}

///Serial communication parameters

public enum SerialParity
{
    /// <summary>No parity bit is present.</summary>
    None,
    /// <summary>
    /// The parity bit is set to 1 if the number of ones in data bits
    /// (not including the parity bit) is odd, and is otherwise set to 0.
    /// </summary>
    Odd,
    /// <summary>
    /// The parity bit is set to 1 if the number of ones in data bits
    /// (not including the parity bit) is even, and is otherwise set to 0.
    /// </summary>
    Even,
    /// <summary>The parity bit exists and is always 1.</summary>
    Mark,
    /// <summary>The parity bit exists and is always 0.</summary>
    Space,
}

public enum SerialStopBitsMode
{
    /// <summary>One stop bit.</summary>
    One,
    /// <summary>One-and-one-half (1.5) stop bits.</summary>
    OneAndOneHalf,
    /// <summary>Two stop bits.</summary>
    Two,
}

public enum SerialFlowControlModes
{
    /// <summary>
    /// The serial connection does not use flow control, and buffers
    /// on both sides of the connection are assumed to be large enough to hold all
    /// data transferred.
    /// </summary>
    None = 0,
    /// <summary>
    /// The serial connection uses the XON and XOFF characters to perform
    /// software flow control.
    /// </summary>
    XOnXOff = 1,
    /// <summary>
    /// The serial connection uses the RTS output signal and the CTS input
    /// signal to perform hardware flow control.
    /// </summary>
    RtsCts = 2,
    /// <summary>
    /// The serial connection uses the DTR output signal and the DSR input
    /// signal to perform hardware flow control.
    /// </summary>
    DtrDsr = 4,
}