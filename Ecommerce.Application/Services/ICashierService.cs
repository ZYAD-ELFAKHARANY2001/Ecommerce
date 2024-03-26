using Ecommerce.Dtos.Cashier;
using Ecommerce.Dtos.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public interface ICashierService
    {
        Task<ResultView<CreateOrUpdateCashierDto>> Create(CreateOrUpdateCashierDto cashierDto);
        Task<ResultView<CreateOrUpdateCashierDto>> HardDelete(CreateOrUpdateCashierDto cashierDto);
        Task<ResultDataList<GetAllCashiersDto>> GetAllPagination(int items, int pagenumber);
        Task<GetAllCashiersDto> GetOne(int ID);
    }
}
