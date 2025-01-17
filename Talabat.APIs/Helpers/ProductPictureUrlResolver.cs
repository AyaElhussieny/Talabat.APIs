using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    // src image from [Product] , destination to [ProductToReturnDTO] , destination member PictureUrl [string]
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["ApiBaseURL"]}{source.PictureUrl}";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
