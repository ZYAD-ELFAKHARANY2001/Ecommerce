using AutoMapper;
using Ecommerce.Application.Contracts;
using Ecommerce.Dtos;
using Ecommerce.Dtos.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class InvoiceServices:IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InvoiceServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResultDataList<GetAllInvoicesDto>> GetAllPagination(int items, int pagenumber) //10 , 3 -- 20 30
        {
            var AllData = (await _unitOfWork.InvoiceRepository.GetAllAsync());
            var SubCategory = AllData.Skip(items * (pagenumber - 1)).Take(items).ToList();
            var SubCategorys = _mapper.Map<List<GetAllInvoicesDto>>(SubCategory);
            /* var SubCategorys = AllData.Skip(item * (pagnumber - 1)).Take(item)
              .Select(c => new GetAllSubDto
              {
                  Id = c.Id,
                  Description = c.Description,
                  Image = c.Image,
                  CategoryName = c.Category.Name,



              }).ToList();*/

            ResultDataList<GetAllInvoicesDto> resultDataFor = new ResultDataList<GetAllInvoicesDto>();

            resultDataFor.Entities = SubCategorys;
            //resultDataFor.count = AllData.Count();


            return resultDataFor;
        }
    }
}
