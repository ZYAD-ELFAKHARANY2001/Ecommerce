using AutoMapper;
using Ecommerce.Application.Contracts;
using Ecommerce.Dtos.Product;
using Ecommerce.Dtos.ViewResult;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public interface IProductService
    {
        Task<ResultView<CreateOrUpdateProductDTO>> Create(CreateOrUpdateProductDTO product);

        Task<ResultView<CreateOrUpdateProductDTO>> HardDelete(CreateOrUpdateProductDTO product);
        Task<ResultView<CreateOrUpdateProductDTO>> SoftDelete(CreateOrUpdateProductDTO product);
        Task<ResultDataList<GetAllProductDTO>> GetAllPagination(int items, int pagenumber);
        Task<CreateOrUpdateProductDTO> GetOne(int ID);
    }
}
