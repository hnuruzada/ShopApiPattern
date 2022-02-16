using AutoMapper;
using Shop.Api.Apps.AdminApi.DTOs.CategoryDtos;
using Shop.Api.Apps.AdminApi.DTOs.ProductDtos;
using Shop.Core.Entities;

namespace Shop.Api.Apps.AdminApi.Profiles
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Category, CategoryGetDto>();
            CreateMap<Category, CategoryInProductGetDto>();
            

            CreateMap<Product, ProductGetDto>()
                .ForMember(dest => dest.Profit, map => map.MapFrom(src => src.SalePrice - src.CostPrice));
            CreateMap<Product, ProductPostDto>();

        }
    }
}
