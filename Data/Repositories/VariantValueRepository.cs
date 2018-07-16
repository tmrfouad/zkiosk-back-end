using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Zkiosk.Core.Repositories;
using Zkiosk.Data.Models;

namespace Zkiosk.Data.Repositories
{
    public class VariantValueRepository : Repository<VariantValue>, IVariantValueRepository
    {
        public ZkioskContext ZkioskContext
        {
            get
            {
                return _context as ZkioskContext;
            }
        }
        public VariantValueRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<VariantValue> GetByVariant(int VariantId)
        {
            return ZkioskContext.VariantValues
                .Where(v => v.VariantId == VariantId)
                .ToList();
        }
    }
}