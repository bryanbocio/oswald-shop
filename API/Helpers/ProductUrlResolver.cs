﻿using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDto, string>
    {

        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.pictureUrl))
            {
                return _configuration["ApiUrl"]+source.pictureUrl;  
            }

            return null;
        }
    }
}
