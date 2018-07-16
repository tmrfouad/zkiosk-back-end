using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Zkiosk.Core.Repositories;
using Zkiosk.Data.Models;

namespace Zkiosk.Data.Repositories
{
    public class OptionValueRepository : Repository<OptionValue>, IOptionValueRepository
    {
        public ZkioskContext ZkioskContext
        {
            get
            {
                return _context as ZkioskContext;
            }
        }
        public OptionValueRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<OptionValue> GetByOption(int OptionId)
        {
            return ZkioskContext.OptionValues
                .Where(v => v.OptionId == OptionId)
                .ToList();
        }
    }
}