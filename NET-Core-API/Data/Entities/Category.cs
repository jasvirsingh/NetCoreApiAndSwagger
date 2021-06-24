using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET_Core_API.Data.Entities
{
    public class Category
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public ICollection<ProductCategory> Products { get; set; }
    }
}
