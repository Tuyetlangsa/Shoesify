namespace Shoesify.Apis.Common;

public class ApiResponse
{
    public string? Message { get; set; }
    // Payload: is data want to send.
    public object? Payload { get; set; }
}