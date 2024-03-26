using System;
using System.Collections.Generic;

namespace Ecommerce.Models
{
    public class City
    {
        public int Id { get; set; }

        public string CityName { get; set; }

        public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();
    }
}