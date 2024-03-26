using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.Cashier
{
    public class CreateOrUpdateCashierDto
    {
        public int Id { get; set; }

        public string CashierName { get; set; }

        public int BranchId { get; set; }
    }
}
