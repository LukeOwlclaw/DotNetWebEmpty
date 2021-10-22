using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Constants
{
    public static class Names
    {
        /// <summary>
        /// Internal name of web application. Use for logging only.
        /// </summary>
        internal const string AppName = "AspNetCoreEmpty";

        /// <summary>
        /// URL that shows exception log if application started in ExceptionStartup mode.
        /// </summary>
        internal const string ExceptionViewUrl = "/oops";
    }
}
