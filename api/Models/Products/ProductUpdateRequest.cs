namespace api.Models.Products;

using System.Text.Json.Serialization;

public class ProductUpdateRequest
{
  public ProductUpdateRequest() { }
  public string Name { get; set; }
  public string Barcode { get; set; }
  public string Description { get; set; }
  public string CategoryName { get; set; }
  public string Weighted { get; set; }
  public string ProductStatus { get; set; }
}