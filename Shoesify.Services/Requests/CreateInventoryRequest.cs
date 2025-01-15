namespace Shoesify.Services.Requests;

public sealed record CreateInventoryRequest(string InventoryId, string Name, string Location);