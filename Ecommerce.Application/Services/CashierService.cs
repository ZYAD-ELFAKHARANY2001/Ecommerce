using AutoMapper;
using Ecommerce.Application.Contracts;
using Ecommerce.Dtos.Cashier;
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
    public class CashierService:ICashierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CashierService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResultView<CreateOrUpdateCashierDto>> Create(CreateOrUpdateCashierDto cashierDto)
        {
            var Query = (await _unitOfWork.CashierRepository.GetAllAsync()); // se;ect * from product
            var OldCashier = Query.Where(c => c.CashierName == cashierDto.CashierName).FirstOrDefault();
            if (OldCashier != null)
            {
                return new ResultView<CreateOrUpdateCashierDto> { Entity = null, IsSuccess = false, Message = "Already Exist" };
            }
            else
            {
                var Prd = _mapper.Map<Cashier>(cashierDto);
                var NewPrd = await _unitOfWork.CashierRepository.CreateAsync(Prd);
                await _unitOfWork.SaveChangesAsync();
                var PrdDto = _mapper.Map<CreateOrUpdateCashierDto>(NewPrd);
                return new ResultView<CreateOrUpdateCashierDto> { Entity = PrdDto, IsSuccess = true, Message = "Created Successfully" };
            }

        }

        public async Task<ResultView<CreateOrUpdateCashierDto>> HardDelete(CreateOrUpdateCashierDto cashierDto)
        {
            try
            {
                var PRd = _mapper.Map<Cashier>(cashierDto);
                var Oldprd = _unitOfWork.CashierRepository.DeleteAsync(PRd);
                await _unitOfWork.SaveChangesAsync();
                var PrdDto = _mapper.Map<CreateOrUpdateCashierDto>(Oldprd);
                return new ResultView<CreateOrUpdateCashierDto> { Entity = PrdDto, IsSuccess = true, Message = "Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new ResultView<CreateOrUpdateCashierDto> { Entity = null, IsSuccess = false, Message = ex.Message };

            }
        }
        public async Task<ResultDataList<GetAllCashiersDto>> GetAllPagination(int items, int pagenumber) //10 , 3 -- 20 30
        {
            var AlldAta = (await _unitOfWork.CashierRepository.GetAllAsync());
            var Prds = AlldAta.Skip(items * (pagenumber - 1)).Take(items)
                                              .Select(p => new GetAllCashiersDto()
                                              {
                                                  Id = p.Id,
                                                  CashierName = p.CashierName,
                                                  BranchId = p.BranchId
                                              }).ToList();
            ResultDataList<GetAllCashiersDto> resultDataList = new ResultDataList<GetAllCashiersDto>();
            resultDataList.Entities = Prds;
            resultDataList.Count = AlldAta.Count();
            return resultDataList;
        }

        public async Task<GetAllCashiersDto> GetOne(int ID)
        {
            var prd = await _unitOfWork.CashierRepository.GetByIdAsync(ID);
            var REturnPrd = _mapper.Map<GetAllCashiersDto>(prd);
            return REturnPrd;
        }
    }
}
