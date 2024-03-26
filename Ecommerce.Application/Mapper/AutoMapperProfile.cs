using AutoMapper;
using Ecommerce.Dtos;
using Ecommerce.Dtos.Cashier;
using Ecommerce.Dtos.Product;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Mapper
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<CreateOrUpdateProductDTO, Product>().ReverseMap();
            CreateMap<GetAllProductDTO, Product>().ReverseMap();
            CreateMap<GetAllInvoicesDto,InvoiceDetail>().ReverseMap();
            CreateMap<GetAllCashiersDto, Cashier>().ReverseMap();
            CreateMap<CreateOrUpdateCashierDto, Cashier>().ReverseMap();
        }
    }
}
