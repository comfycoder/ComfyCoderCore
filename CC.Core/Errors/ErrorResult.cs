using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Core.Errors
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorResult
    {
        /// <summary>
        /// 
        /// </summary>
        public ErrorResponse Error { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public int StatusCode { get; internal set; }
    }
}
