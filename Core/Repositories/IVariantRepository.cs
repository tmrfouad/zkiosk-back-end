using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Zkiosk.Data.Models;

namespace Zkiosk.Core.Repositories
{
    public interface IVariantRepository : IRepository<Variant>
    {
        Variant GetWithValues(int Id);
        Variant GetWithProductAndValues(int Id);
        IEnumerable<Variant> GetAllWithValues();
        IEnumerable<Variant> GetAllWithProductAndValues();
        IEnumerable<Variant> GetByProduct(int ProductId);
        IEnumerable<Variant> GetByProductWithValues(int ProductId);
    }
}