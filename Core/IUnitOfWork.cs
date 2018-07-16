using System;
using Zkiosk.Core.Repositories;

namespace Zkiosk.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IOptionRepository Options { get; }
        IVariantRepository Variants { get; }
        IVariantValueRepository VariantValues { get; }
        IOptionValueRepository OptionValues { get; }
        int Complete();
    }
}