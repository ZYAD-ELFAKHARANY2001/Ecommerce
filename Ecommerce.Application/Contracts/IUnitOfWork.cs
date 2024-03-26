using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Contracts
{
    public interface IUnitOfWork
    {
        IInoviceDataRepository? InvoiceRepository { get; }
        IRepository<User,string>? UserRepository { get; }
        public ICashierRepository? CashierRepository { get; }
        Task SaveChangesAsync();


    }
}
