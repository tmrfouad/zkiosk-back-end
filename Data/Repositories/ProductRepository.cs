using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Zkiosk.Core.Repositories;
using Zkiosk.Data.Models;

namespace Zkiosk.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ZkioskContext ZkioskContext
        {
            get
            {
                return _context as ZkioskContext;
            }
        }
        public ProductRepository(DbContext context) : base(context)
        {
        }

        public int GetVariantCount(int Id)
        {
            return ZkioskContext.Variants.Where(v => v.ProductId == Id).Count();
        }

        public Product GetWithVariants(int Id)
        {
            return ZkioskContext.Products.Include(p => p.Variants).SingleOrDefault(p => p.Id == Id);
        }

        public IEnumerable<Product> GetAllWithVariants()
        {
            return ZkioskContext.Products.Include(p => p.Variants).ToList();
        }
    }
}