﻿using System;
using System.Collections.Generic;

namespace Ecommerce.Models
{
    public class InvoiceDetail
    {
        public long Id { get; set; }

        public long InvoiceHeaderId { get; set; }

        public string ItemName { get; set; }

        public double ItemCount { get; set; }

        public double ItemPrice { get; set; }

        public virtual InvoiceHeader InvoiceHeader { get; set; }
    }
}