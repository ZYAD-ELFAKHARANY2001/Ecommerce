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
    public class ProductService :IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper) {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ResultView<CreateOrUpdateProductDTO>> Create(CreateOrUpdateProductDTO product)
        {
            var Query = (await _productRepository.GetAllAsync()); // se;ect * from product
            var OldProduct = Query.Where(p=>p.Name == product.Name).FirstOrDefault();
            if(OldProduct != null)
            {
                return new ResultView<CreateOrUpdateProductDTO> { Entity = null, IsSuccess = false, Message = "Already Exist" };
            }
            else
            {
                var Prd = _mapper.Map<Product>(product);
                var NewPrd = await _productRepository.CreateAsync(Prd);
                await _productRepository.SaveChangesAsync();
                var PrdDto = _mapper.Map<CreateOrUpdateProductDTO>(NewPrd);
                return new ResultView<CreateOrUpdateProductDTO> { Entity = PrdDto , IsSuccess = true , Message = "Created Successfully"};
            }

        }
        
        public async Task<ResultView<CreateOrUpdateProductDTO>> HardDelete(CreateOrUpdateProductDTO product)
        {
            try
            {
                var PRd = _mapper.Map<Product>(product);
                var Oldprd = _productRepository.DeleteAsync(PRd);
                await _productRepository.SaveChangesAsync();
                var PrdDto = _mapper.Map<CreateOrUpdateProductDTO>(Oldprd);
                return new ResultView<CreateOrUpdateProductDTO> { Entity = PrdDto, IsSuccess = true, Message = "Deleted Successfully" };
            }
            catch(Exception ex) 
            {
                return new ResultView<CreateOrUpdateProductDTO> { Entity = null, IsSuccess = false, Message = ex.Message };

            }
        }
        public async Task<ResultView<CreateOrUpdateProductDTO>> SoftDelete(CreateOrUpdateProductDTO product)
        {
            try
            {
                var PRd = _mapper.Map<Product>(product);
                var Oldprd = (await _productRepository.GetAllAsync()).FirstOrDefault(p=>p.Id == product.Id);
                Oldprd.IsDeleted = true;
                await _productRepository.SaveChangesAsync();
                var PrdDto = _mapper.Map<CreateOrUpdateProductDTO>(Oldprd);
                return new ResultView<CreateOrUpdateProductDTO> { Entity = PrdDto, IsSuccess = true, Message = "Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new ResultView<CreateOrUpdateProductDTO> { Entity = null, IsSuccess = false, Message = ex.Message };

            }
        }

        public async Task<ResultDataList<GetAllProductDTO>> GetAllPagination(int items , int pagenumber) //10 , 3 -- 20 30
        {
            var AlldAta = (await _productRepository.GetAllAsync());
            var Prds = AlldAta.Skip(items * (pagenumber - 1)).Take(items)
                                              .Select(p => new GetAllProductDTO()
                                              {
                                                  Id = p.Id,
                                                  Name = p.Name,
                                                  Price = p.Price,
                                                  CategoryName = p.Category.Name
                                              }).ToList();
            ResultDataList<GetAllProductDTO> resultDataList = new ResultDataList<GetAllProductDTO>();
            resultDataList.Entities = Prds;
            resultDataList.Count = AlldAta.Count();
            return resultDataList;
        }
        
        public async Task<CreateOrUpdateProductDTO> GetOne(int ID)
        {
            var prd =await _productRepository.GetByIdAsync(ID);
            var REturnPrd = _mapper.Map<CreateOrUpdateProductDTO>(prd);
            return REturnPrd;
        }
    }
}
