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

