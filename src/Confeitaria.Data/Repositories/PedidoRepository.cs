using Confeitaria.Business.Interfaces;
using Confeitaria.Business.Models;
using Confeitaria.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Confeitaria.Data.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(AppDbContext context) : base(context) { }

        public async Task<Pedido> ObterPedidoEndereco(Guid id)
        {
            return await Db.Pedidos.AsNoTracking().Include(p => p.Endereco).FirstOrDefaultAsync(p => p.Id == id);  
        }

        public async Task<Pedido> ObterPedidoCliente(Guid id)
        {
            return await Db.Pedidos.AsNoTracking().Include(p => p.Cliente).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pedido>> ObterPedidoECliente()
        {
            return await Db.Pedidos.AsNoTracking().Include(p => p.Cliente).ToListAsync();
        }

        public async Task<Pedido> ObterPedidoClienteEndereco(Guid id)
        {
            return await Db.Pedidos.AsNoTracking().Include(p => p.Cliente).Include(p => p.Endereco).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Pedido> ObterPedidoCompleto(Guid id)
        {
            return await Db.Pedidos.AsNoTracking().Include(p => p.Cliente).Include(p => p.Endereco).Include(p => p.PedidoProdutos).ThenInclude(p => p.Produto).FirstOrDefaultAsync(p => p.Id == id);
        }

    }
}
