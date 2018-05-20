namespace CC.Core.Errors
{
    /// <summary>
    /// Error Codes enumeration.
    /// </summary>
    public class ErrorCodes
    {
        /// <summary>
        /// Bad Request
        /// </summary>
        public const int BadRequest = 400;

        /// <summary>
        /// Unauthorized User
        /// </summary>
        public const int Unauthorized = 401;

        /// <summary>
        /// Missing Header
        /// </summary>
        public const int MissingHeader = 1000;

        /// <summary>
        /// Missing Claim
        /// </summary>
        public const int MissingClaim = 1001;

        /// <summary>
        /// Data Access Exception
        /// </summary>
        public const int DataAccessException = 1100;

        /// <summary>
        /// Record Not Found
        /// </summary>
        public const int RecordNotFound = 1101;
    }
}
