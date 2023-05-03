namespace api.Models.Products;

public class ProductStatusResponse
{
  public ProductStatusResponse(int soldCount, int damagedCount, int inStockCount)
  {
    CountSold = soldCount;
    CountDamaged = damagedCount;
    CountInStock = inStockCount;
  }
  public int CountSold { get; set; }
  public int CountDamaged { get; set; }
  public int CountInStock { get; set; }
}