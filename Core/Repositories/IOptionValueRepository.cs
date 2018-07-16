using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Zkiosk.Data.Models;

namespace Zkiosk.Core.Repositories
{
    public interface IOptionValueRepository : IRepository<OptionValue>
    {
        IEnumerable<OptionValue> GetByOption(int OptionId);
    }
}