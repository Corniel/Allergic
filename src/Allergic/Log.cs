using System;
using System.Diagnostics;
using System.IO;

namespace Allergic
{
    /// <summary>Lightweight file log.</summary>
    public static class Log
    {
        /// <summary>The file to log to.</summary>
        public static readonly string File = Path.Combine(Path.GetTempPath(), "allergic.log");

        /// <summary>Writes a message to the log file.</summary>
        [DebuggerStepThrough]
        public static void Write(string format, params object[] args)
        {
            try
            {
                using (var writer = new StreamWriter(File, true))
                {
                    writer.Write(DateTime.Now.ToString("HH:mm:ss.fff "));
                    writer.WriteLine(format, args);
                }
            }
            catch { /* it is logging, it should never fail. */ }
        }
    }
}
