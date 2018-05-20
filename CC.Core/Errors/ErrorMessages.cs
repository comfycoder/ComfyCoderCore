namespace CC.Core.Errors
{
    /// <summary>
    /// String constants for error messages returned from the My Investment View API.
    /// </summary>
    public class ErrorMessages
    {
        /// <summary>
        /// Missing financial account identifier.
        /// </summary>
        public const string MissingFinancialAccountId = "Missing financial account identifier.";

        /// <summary>
        /// User has not been authorized
        /// </summary>
        public const string Unauthorized = "User not authorized.";

        /// <summary>
        /// Method has not been implemented
        /// </summary>
        public const string NotImplemented = "Method has not been implemented.";

        /// <summary>
        /// An unhandled error condition occured.
        /// </summary>
        public const string InternalServerError = "An unhandled error condition has occurred.";

        /// <summary>
        /// Record not found.
        /// </summary>
        public const string RecordNotFound = "Unable to find the record.";

        /// <summary>
        /// Header key value not found.
        /// </summary>
        public const string HeaderNotFound = "Missing header information.";

        /// <summary>
        /// Header key value not found.
        /// </summary>
        public const string ClaimNotFound = "Missing claim information.";

        /// <summary>
        /// Data Access error condition has occurred.
        /// </summary>
        public const string DataAccessError = "Data Access error condition has occurred.";
    }
}
