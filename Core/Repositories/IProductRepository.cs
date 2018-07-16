using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Zkiosk.Data.Models;

namespace Zkiosk.Core.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        int GetVariantCount(int Id);
        Product GetWithVariants(int Id);
        IEnumerable<Product> GetAllWithVariants();
    }
}