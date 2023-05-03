namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using api.Models.Products;
using System;

// using api.Services;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
  private IUserService _userService;
  private IMapper _mapper;

  public ProductsController(
      IUserService userService,
      IMapper mapper)
  {
    _userService = userService;
    _mapper = mapper;
  }

  [HttpGet]
  public IActionResult GetAll()
  {
    var users = _userService.GetAll();
    return Ok(users);
  }

  [HttpGet("{id}")]
  public IActionResult GetById(int id)
  {
    var user = _userService.GetById(id);
    return Ok(user);
  }

  [HttpPost]
  public IActionResult Create([FromBody] ProductCreateRequest model)
  {
    if (model is null)
    {
      throw new ArgumentNullException(nameof(model));
    }

    _userService.Create(model);
    return Ok(new { message = "User created" });
  }

  [HttpPut("{id}")]
  public IActionResult Update(int id, ProductUpdateRequest model)
  {
    _userService.Update(id, model);
    return Ok(new { message = "User updated" });
  }

  [HttpDelete("{id}")]
  public IActionResult Delete(int id)
  {
    _userService.Delete(id);
    return Ok(new { message = "User deleted" });
  }


  [HttpGet("getProductsSold")]
  public IActionResult getProductsSold()
  {
    var data = _userService.GetByStatus();
    return Ok(new[] {
        new { type = "Sold", count = data.CountSold },
        new { type = "Damaged", count = data.CountDamaged },
        new { type = "InStock", count = data.CountInStock }
        });
  }

  [HttpPost("sellProduct")]
  public IActionResult sellProduct([FromBody] CreateSellRequest model)
  {
    _userService.sellItem(model);
    return Ok(new { message = "User created" });
  }
}
