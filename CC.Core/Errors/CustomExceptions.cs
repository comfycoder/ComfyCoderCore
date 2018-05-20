using System;
using System.Runtime.Serialization;

namespace CC.Core.Errors
{
    /// <summary>
    /// Header Not Found Exception
    /// </summary>
    [Serializable]
    public class HeaderNotFoundException : Exception
    {
        /// <summary>
        /// Constructer that accepts a message
        /// </summary>
        /// <param name="message">
        /// An error message.
        /// </param>
        public HeaderNotFoundException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Claim Not Found Exception
    /// </summary>
    [Serializable]
    public class ClaimNotFoundException : Exception
    {
        /// <summary>
        /// Constructer that accepts a message
        /// </summary>
        /// <param name="message">
        /// An error message.
        /// </param>
        public ClaimNotFoundException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Record Not Found Exception
    /// </summary>
    [Serializable]
    public class RecordNotFoundException : Exception
    {
        /// <summary>
        /// Constructer that accepts a message
        /// </summary>
        /// <param name="message">
        /// An error message.
        /// </param>
        public RecordNotFoundException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Security Exception
    /// </summary>
    [Serializable]
    public class IdentityMissingException : Exception
    {
        public IdentityMissingException()
        {
        }

        /// <summary>
        /// Constructer that accepts a message
        /// </summary>
        /// <param name="message">
        /// An error message.
        /// </param>
        public IdentityMissingException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Data Access Exception
    /// </summary>
    [Serializable]
    public class DataAccessException : Exception
    {
        public int Code { get; set; }

        /// <summary>
        /// Constructer that accepts a message
        /// </summary>
        /// <param name="message">
        /// An error message.
        /// </param>
        public DataAccessException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructer that accepts a message and an inner exception
        /// </summary>
        /// <param name="message">
        /// An error message.
        /// </param>
        /// <param name="innerException"></param>
        public DataAccessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor that accepts a SerializationInfo and StreamingContext object instances.
        /// </summary>
        /// <param name="info">
        /// A SerializationInfo object instance.
        /// </param>
        /// <param name="context">
        /// A SerializationInfo object instance.
        /// </param>
        protected DataAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
