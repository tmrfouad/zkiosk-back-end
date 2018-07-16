using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Zkiosk.Core.Repositories;
using Zkiosk.Data.Models;

namespace Zkiosk.Data.Repositories
{
    public class VariantRepository : Repository<Variant>, IVariantRepository
    {
        public ZkioskContext ZkioskContext
        {
            get
            {
                return _context as ZkioskContext;
            }
        }
        public VariantRepository(DbContext context) : base(context)
        {
        }

        public Variant GetWithValues(int Id)
        {
            return ZkioskContext.Variants
                .Include(v => v.Values)
                    .ThenInclude(v => v.Value)
                .SingleOrDefault(v => v.Id == Id);
        }

        public Variant GetWithProductAndValues(int Id)
        {
            return ZkioskContext.Variants
                .Include(v => v.Product)
                .Include(v => v.Values)
                    .ThenInclude(v => v.Value)
                .SingleOrDefault(v => v.Id == Id);
        }

        public IEnumerable<Variant> GetAllWithValues()
        {
            return ZkioskContext.Variants
                .Include(v => v.Values)
                    .ThenInclude(v => v.Value)
                .ToList();
        }

        public IEnumerable<Variant> GetAllWithProductAndValues()
        {
            return ZkioskContext.Variants
                .Include(v => v.Product)
                .Include(v => v.Values)
                    .ThenInclude(v => v.Value)
                .ToList();
        }

        public IEnumerable<Variant> GetByProduct(int ProductId)
        {
            return ZkioskContext.Variants
                .Where(v => v.ProductId == ProductId)
                .ToList();
        }

        public IEnumerable<Variant> GetByProductWithValues(int ProductId)
        {
            return ZkioskContext.Variants
                .Where(v => v.ProductId == ProductId)
                .Include(v => v.Product)
                .Include(v => v.Values)
                    .ThenInclude(v => v.Value)
                .ToList();
        }
    }
}