using Microsoft.EntityFrameworkCore;
using NET_Core_API.Data.Entities;
using NET_Core_API.Data.Repositories.Repos_Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET_Core_API.Data.Repositories.Repositories
{
    public class ProductRepo : IProductRepo
    {

        private readonly ProductContext _context;

        public ProductRepo(ProductContext context)
        {
            _context = context;
        }

        public async Task<Product> Get(int id)
        {
            var product = _context.Products.FirstOrDefaultAsync
                        (i => i.ID == id);

            return await product;
        }

        public async Task<List<Product>> GetAll()
        {
            var products = _context.Products.ToListAsync();

            return await products;
        }

        public async Task<int> Save(Product item)
        {
            if(item.ID == 0)
            {
                _context.Products.Add(item);
            }
            else
            {
                _context.Products.Update(item);
            }

            return await _context.SaveChangesAsync();
        }


        public async Task<int> Delete(Product item)
        {
            _context.Remove(item);

            return await _context.SaveChangesAsync();
        }
    }
}
