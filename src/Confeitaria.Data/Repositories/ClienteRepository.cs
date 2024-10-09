using Confeitaria.Business.Interfaces;
using Confeitaria.Business.Models;
using Confeitaria.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Confeitaria.Data.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente> , IClienteRepository
    {
        public ClienteRepository(AppDbContext context) : base(context) { }

        public async Task<Cliente> ObterClientePedidos(Guid id)
        {
           return await Db.Clientes.AsNoTracking().Include(c => c.Pedidos).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
