using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                    .ForMember(productDto => productDto.productBrand,
                               options => options.MapFrom(product => product.productBrand.name))

                     .ForMember(productDto => productDto.productType,
                               options => options.MapFrom(product => product.productType.name))

                     .ForMember(productDto => productDto.pictureUrl, options => options.MapFrom<ProductUrlResolver>());
        }
    }
}
