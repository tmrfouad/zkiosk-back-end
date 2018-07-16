using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Zkiosk.Core.Repositories;
using Zkiosk.Data.Models;

namespace Zkiosk.Data.Repositories
{
    public class OptionRepository : Repository<Option>, IOptionRepository
    {
        public ZkioskContext ZkioskContext
        {
            get
            {
                return _context as ZkioskContext;
            }
        }
        public OptionRepository(DbContext context) : base(context)
        {
        }

        public Option GetWithValues(int Id)
        {
            return ZkioskContext.Options
                .Include(o => o.Values)
                .SingleOrDefault(o => o.Id == Id);
        }

        public Option GetWithProductAndValues(int Id)
        {
            return ZkioskContext.Options
                .Include(o => o.Product)
                .Include(o => o.Values)
                .SingleOrDefault(o => o.Id == Id);
        }

        public IEnumerable<Option> GetAllWithValues()
        {
            return ZkioskContext.Options
                .Include(p => p.Values)
                .ToList();
        }

        public IEnumerable<Option> GetAllWithProductAndValues()
        {
            return ZkioskContext.Options
                .Include(o => o.Product)
                .Include(p => p.Values)
                .ToList();
        }

        public IEnumerable<Option> GetByProduct(int ProductId)
        {
            return ZkioskContext.Options
                .Where(v => v.ProductId == ProductId)
                .ToList();
        }

        public IEnumerable<Option> GetByProductWithValues(int ProductId)
        {
            return ZkioskContext.Options
                .Include(v => v.Values)
                .Include(v => v.Product)
                .Where(v => v.ProductId == ProductId)
                .ToList();
        }
    }
}