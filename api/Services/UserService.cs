using AutoMapper;
// using WebApi.Entities;
using api.Data;
using api.Models.Products;

public interface IUserService
{
  IEnumerable<Products> GetAll();
  Products GetById(int id);
  void Create(ProductCreateRequest model);
  void Update(int id, ProductUpdateRequest model);
  void Delete(int id);
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
}