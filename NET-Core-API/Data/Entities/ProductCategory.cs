using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET_Core_API.Data.Entities
{
    public class ProductCategory
    {
        public int ProductID { get; set; }

        public Product Product { get; set; }

        public int CategoryID { get; set; }

        public Category Category { get; set; }
    }
}
