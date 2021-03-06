﻿using System;

namespace CC.Core.Errors
{
    /// <summary>
    /// Model class to return a field level error, typically as the result of the field failing validation
    /// </summary>
    /// <remarks>
    /// Typically an array of these objects are returned when an incoming object fails validation
    /// </remarks>
    public class RequestError
    {

        /// <summary>
        /// The name of the field with an issue.
        /// </summary>
        public String Field { get; set; }

        /// <summary>
        /// A message containing a description of the error
        /// </summary>
        public String Message { get; set; }
    }
}
