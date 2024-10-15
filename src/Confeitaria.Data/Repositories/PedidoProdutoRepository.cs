using Confeitaria.Business.Interfaces;
using Confeitaria.Business.Models;
using Confeitaria.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Confeitaria.Data.Repositories
{
    public class PedidoProdutoRepository : IPedidoProdutoRepository
    {

        private readonly AppDbContext Db;
        public PedidoProdutoRepository(AppDbContext db)
        {
            Db = db;
        }
        public async Task Adicionar(PedidoProduto pedidoProduto)
        {
            Db.PedidoProdutos.Add(pedidoProduto);
            await SaveChanges();
        }

        public async Task Atualizar(PedidoProduto pedidoProduto)
        {
            Db.PedidoProdutos.Update(pedidoProduto);
            await SaveChanges();
        }
        public async Task Remover(Guid id)
        {
            var pedidoProduto = await ObterPorId(id);
            if(pedidoProduto != null)
            {
                Db.PedidoProdutos.Remove(pedidoProduto);
                await SaveChanges();
            }
        }

        public async Task<PedidoProduto> ObterPorId(Guid id)
        {
            return await Db.PedidoProdutos.FindAsync(id);
        }

        public async Task<IEnumerable<PedidoProduto>> ObterPorPedido(Guid pedidoId)
        {
            return await Db.PedidoProdutos.Where(p => p.PedidoId == pedidoId).ToListAsync();
        }

        public async Task<IEnumerable<PedidoProduto>> ObterTodos()
        {
            return await Db.PedidoProdutos.ToListAsync();
        }
        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

    }
}
