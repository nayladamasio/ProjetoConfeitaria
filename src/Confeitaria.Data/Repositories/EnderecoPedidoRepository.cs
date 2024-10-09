using Confeitaria.Business.Interfaces;
using Confeitaria.Business.Models;
using Confeitaria.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Confeitaria.Data.Repositories
{
    public class EnderecoPedidoRepository : BaseRepository<EnderecoPedido> , IEnderecoPedidoRepository
    {
        public EnderecoPedidoRepository(AppDbContext db) : base(db) { }

        public async Task<EnderecoPedido> ObterEnderecoPorPedido(Guid pedidoId)
        {
            return await Db.PedidosEndereco.AsNoTracking().FirstOrDefaultAsync(e => e.PedidoId == pedidoId);
        }
    }
}
