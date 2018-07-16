using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Zkiosk.Data.Models;

namespace Zkiosk.Core.Repositories
{
    public interface IOptionRepository : IRepository<Option>
    {
        Option GetWithValues(int Id);
        Option GetWithProductAndValues(int Id);
        IEnumerable<Option> GetAllWithValues();
        IEnumerable<Option> GetAllWithProductAndValues();
        IEnumerable<Option> GetByProduct(int ProductId);
        IEnumerable<Option> GetByProductWithValues(int ProductId);
    }
}