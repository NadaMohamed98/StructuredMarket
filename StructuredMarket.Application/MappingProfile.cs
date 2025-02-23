using AutoMapper;
using StructuredMarket.Application.Features.Products.Models;
using StructuredMarket.Application.Features.Users.Models;
using StructuredMarket.Domain.Entities;

namespace StructuredMarket.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Product, ProductResModel>().ReverseMap();
        }
    }
}
