using System;
using System.Collections.Generic;

namespace Ecommerce.Models
{
    public class Cashier
    {
        public int Id { get; set; }

        public string CashierName { get; set; }

        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual ICollection<InvoiceHeader> InvoiceHeaders { get; set; } = new List<InvoiceHeader>();
    }
}