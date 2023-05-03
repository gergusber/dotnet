namespace api.Models.Products;

// using System.Text.Json.Serialization;
// Name
// ○ Barcode
// ○ Description
// ○ Category name
// ○ Weighted or Non Weighted
// ○ Product Status (Sold, inStock, Damaged)
public class Products
{
  public Products() { }

  public int Id { get; set; }
  public string Name { get; set; }
  public string Barcode { get; set; }
  public string Description { get; set; }
  public string CategoryName { get; set; }
  public string Weighted { get; set; }
  public string ProductStatus { get; set; } 
}