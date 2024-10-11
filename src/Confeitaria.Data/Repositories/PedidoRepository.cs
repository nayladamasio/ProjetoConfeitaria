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

        //public async Task<Pedido> ObterPedidoProdutos(Guid id)
        //{
        //    return await Db.Pedidos.AsNoTracking().Include(p => p.Produtos).FirstOrDefaultAsync(p => p.Id == id);
        //}
    }
}
