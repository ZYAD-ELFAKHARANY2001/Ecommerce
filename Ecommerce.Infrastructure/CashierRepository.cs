using Ecommerce.Application.Contracts;
using Ecommerce.Context;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure
{
    public class CashierRepository : Repository<Cashier, int>, ICashierRepository
    {
        public CashierRepository(EcommerceContext ecommerceContext) : base(ecommerceContext)
        {
        }
    }
}
