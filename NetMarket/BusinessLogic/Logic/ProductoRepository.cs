using BusinessLogic.Data;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly MarketDbContext _marketDbContext;

        public ProductoRepository(MarketDbContext marketDbContext)
        {
            _marketDbContext = marketDbContext;
        }

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            return await _marketDbContext.Producto
                            .Include(p => p.Marca)
                            .Include(p => p.Categoria)
                            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Producto>> GetProductosAsync()
        {
            return await _marketDbContext.Producto
                    .Include(p => p.Marca)
                    .Include(p => p.Categoria)
                    .ToListAsync();
        }

        public async Task<List<Producto>> GetAllProductosAsync()
        {
            return await _marketDbContext.Producto
                    .Include(p => p.Marca)
                    .Include(p => p.Categoria)
                    .ToListAsync();
        }
    }
}
