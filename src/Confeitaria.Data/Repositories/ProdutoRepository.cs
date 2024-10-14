using Confeitaria.Business.Enums;
using Confeitaria.Business.Interfaces;
using Confeitaria.Business.Models;
using Confeitaria.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Confeitaria.Data.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base (context) { }

        public async Task<Produto> ObterProdutoId(Guid id)
        {
            return await Db.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        
    }
}
