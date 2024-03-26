using Ecommerce.Dtos.ViewResult;
using Ecommerce.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public interface IInvoiceService
    {
        Task<ResultDataList<GetAllInvoicesDto>> GetAllPagination(int items, int pagenumber);
    }
}
