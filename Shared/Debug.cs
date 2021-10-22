using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHelpers
{
    public static class Debug
    {
        /// <summary>
        /// Break execution in debug mode.
        /// </summary>
        public static void Break()
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Break();
            }
#endif
        }

#if DEBUG
        private static HashSet<string> gBreakOnceKeys = new HashSet<string>();
#endif

        /// <summary>
        /// Break execution in debug mode if condition is met.
        /// </summary>
        /// <param name="breakOnTrue">Function to check for break</param>
        /// <param name="breakOnceKey">If set, break will only be called once for given key</param>
        ///
#if !DEBUG
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1801:Nicht verwendete Parameter überprüfen", Justification = "<Ausstehend>")]
#endif
        public static void Break(Func<bool> breakOnTrue, string? breakOnceKey = null)
        {
#if DEBUG
            // Break
            // - Only when running in debugger
            // - When check function returns true or is not provided
            // - When key is provided for the first time (i.e. HashSet.Add succeeds) or not at all
            if (System.Diagnostics.Debugger.IsAttached
                && (breakOnTrue == null || breakOnTrue())
                && (breakOnceKey == null || gBreakOnceKeys.Add(breakOnceKey)))
            {
                System.Diagnostics.Debugger.Break();
            }
#endif
        }
    }
}
