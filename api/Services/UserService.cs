using AutoMapper;
// using WebApi.Entities;
using api.Data;
using api.Models.Products;

public interface IUserService
{
  IEnumerable<Products> GetAll();
  ProductStatusResponse GetByStatus();
  Products GetById(int id);
  void Create(ProductCreateRequest model);
  void Update(int id, ProductUpdateRequest model);
  void Delete(int id);

  void sellItem(CreateSellRequest model);
}

public class UserService : IUserService
{
  private DataContext _context;
  private readonly IMapper _mapper;

  public UserService(
      DataContext context,
      IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public IEnumerable<Products> GetAll()
  {
    return _context.Products;
  }

  public Products GetById(int id)
  {
    return getUser(id);
  }

  public void Create(ProductCreateRequest model)
  {
    // validate
    if (_context.Products.Any(x => x.Name == model.Name))
      throw new Exception("Prd with the Name '" + model.Name + "' already exists");

    // map model to new user object
    var prd = _mapper.Map<Products>(model);


    // save user
    _context.Products.Add(prd);
    _context.SaveChanges();
  }

  public void Update(int id, ProductUpdateRequest model)
  {
    var prd = getUser(id);

    // validate
    if (model.Name != prd.Name && _context.Products.Any(x => x.Name == model.Name))
      throw new Exception("Prd with the Name '" + model.Name + "' already exists");

    // copy model to user and save
    _mapper.Map(model, prd);
    _context.Products.Update(prd);
    _context.SaveChanges();
  }

  public void Delete(int id)
  {
    var prd = getUser(id);
    _context.Products.Remove(prd);
    _context.SaveChanges();
  }

  // helper methods

  private Products getUser(int id)
  {
    var prd = _context.Products.Find(id);
    if (prd == null) throw new KeyNotFoundException("Product not found");
    return prd;
  }

  public ProductStatusResponse GetByStatus()
  {
    var countsByStatus = _context.Products
      .GroupBy(p => p.Status)
      .Select(g => new { Status = g.Key, Count = g.AmountInStock })
      .ToDictionary(x => x.Status, x => x.Count);

    int soldCount = countsByStatus.GetValueOrDefault(ProductsStatus.Sold, 0);
    int damagedCount = countsByStatus.GetValueOrDefault(ProductsStatus.Damaged, 0);
    int inStockCount = countsByStatus.GetValueOrDefault(ProductsStatus.InStock, 0);

    return new ProductStatusResponse(soldCount, damagedCount, inStockCount);
  }

  public void sellItem(CreateSellRequest model)
  {
    var getProduct = _context.Products.SingleOrDefault(p => p.Id == model.Id);

    if (getProduct.AmountInStock >= model.Amount)
    {
      var left = getProduct.AmountInStock - model.Amount;

      var prd = getUser(model.Id);
      prd.AmountInStock = left;
      _context.Products.Update(prd);
      _context.SaveChanges();
      return;
    }

    throw new Exception("Prd dont have the amount required :'" + model.Amount + "'");
  }
}