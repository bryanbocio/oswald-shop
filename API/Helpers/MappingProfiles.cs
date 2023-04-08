using API.DTOs;
using API.DTOs.Account;
using API.DTOs.Customer;
using API.DTOs.Order;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;

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

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>();
            
            CreateMap<BasketItemsDto, BasketItems>();

            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();

            CreateMap<Order, OrderToReturnDto>()
                     .ForMember(order => order.DeliveryMethod, options => options.MapFrom(source => source.DeliveryMethod.ShortName))
                     .ForMember(order => order.ShippingPrice,  options => options.MapFrom(source => source.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                     .ForMember(order => order.ProductId,   options => options.MapFrom(source => source.ItemOrdered.ProductItemId))
                     .ForMember(order => order.ProductName, options => options.MapFrom(source => source.ItemOrdered.ProductName))
                     .ForMember(order => order.PictureUrl,  options => options.MapFrom(source => source.ItemOrdered.PictureUrl))
                     .ForMember(order => order.PictureUrl,  options => options.MapFrom<OrderItemUrlResolver>());


        }
    }
}
