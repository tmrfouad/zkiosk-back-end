using Zkiosk.Core;
using Zkiosk.Core.Repositories;
using Zkiosk.Data.Repositories;

namespace Zkiosk.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ZkioskContext _context;
        public IProductRepository Products { get; private set; }
        public IVariantRepository Variants { get; private set; }
        public IOptionRepository Options { get; private set; }
        public IVariantValueRepository VariantValues { get; private set; }
        public IOptionValueRepository OptionValues { get; private set; }

        public UnitOfWork(ZkioskContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            Variants = new VariantRepository(_context);
            Options = new OptionRepository(_context);
            VariantValues = new VariantValueRepository(_context);
            OptionValues = new OptionValueRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}