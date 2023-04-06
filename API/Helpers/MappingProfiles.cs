using API.DTOs;
using API.DTOs.Account;
using API.DTOs.Customer;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

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

            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>();
            
            CreateMap<BasketItemsDto, BasketItems>();

            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();


        }
    }
}
