using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Zkiosk.Data.Models;

namespace Zkiosk.Core.Repositories
{
    public interface IVariantValueRepository : IRepository<VariantValue>
    {
        IEnumerable<VariantValue> GetByVariant(int VariantId);
    }
}