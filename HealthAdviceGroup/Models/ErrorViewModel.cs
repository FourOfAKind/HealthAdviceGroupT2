namespace HealthAdviceGroup.Models
{
    // Model representing error details for display in views
    public class ErrorViewModel
    {
        // Property to store the request identifier associated with the error
        public string? RequestId { get; set; }

        // Property that returns true if the request identifier is not null or empty
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}