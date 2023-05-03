namespace WebApi.Helpers;

using AutoMapper;
using api.Models.Products;

public class AutoMapperProfile : Profile
{
  public AutoMapperProfile()
  {
    // CreateRequest -> Products
    CreateMap<ProductCreateRequest, Products>().ReverseMap();

    // UpdateRequest -> Products
    CreateMap<ProductUpdateRequest, Products>().ReverseMap();
        // .ForAllMembers(x => x.Condition(
        //     (src, dest, prop) =>
        //     {
        //       // ignore both null & empty string properties
        //       if (prop == null) return false;
        //       if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

        //       return true;
        //     }
        // ));

  }
}