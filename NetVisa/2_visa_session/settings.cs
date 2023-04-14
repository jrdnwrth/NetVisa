using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace NetVisa
{
    /// <summary>
    /// Defines session-specific settings used by the Instrument class
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// If TRUE, each VISA read must end with TermChar. If not, the reading continues
        /// If FALSE, (default) the reading can end with any character
        /// </summary>
        public bool AssureResponseEndWithTc = false;

        /// <summary>
        /// Delay before each Write (not valid for segmented writes)
        /// </summary>
        public int WriteDelay { get; set; } = 0;

        /// <summary>
        /// Delay before each Read (not valid for segmented reads)
        /// </summary>
        public int ReadDelay { get; set; } = 0;

        /// <summary>
        /// Maximum read/write segment size when communicating with the instrument
        /// </summary>
        public int IoSegmentSize { get; set; } = 10000000;

        /// <summary>
        /// OPC timeout in milliseconds for all write/read with OPC sync operations
        /// </summary>
        public int OpcTimeout { get; set; } = 30000;

        /// <summary>VISA timeout in milliseconds for all VISA operations</summary>
        public int VisaTimeout { get; set; } = 10000;

        /// <summary>
        /// If &gt;0, during STB polling sets the VISA Timeout to a this number to avoid long waiting times by some instruments (NRP-S/SN) sensors
        /// </summary>
        public int ReadStbVisaTimeout { get; set; } = -1;

        /// <summary>
        /// Termination character for reading and writing. Default is LF
        /// </summary>
        public char TermChar { get; set; } = '\n';

        /// <summary>
        /// You can define the default VxiCapable value.
        /// It might be coerced to FALSE, depending on the session kind
        /// </summary>
        public bool VxiCapable { get; set; } = false;

        /// <summary>
        /// If TRUE (default is FALSE), all the *STB? queries are disabled and simulate returning 0
        /// </summary>
        public bool DisableStbQuery { get; set; } = false;
    }
}
