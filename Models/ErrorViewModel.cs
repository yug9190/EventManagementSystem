namespace EventManagementSystem.Models
{
    /// <summary>
    /// Represents the error details to be shown in the error view.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets the request identifier.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Indicates whether the RequestId should be shown.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
