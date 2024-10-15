using Confeitaria.Business.Interfaces;
using Confeitaria.Business.Models;
using Confeitaria.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Confeitaria.Data.Repositories
{
    public class FaleConoscoRepository : IFaleConoscoRepository
    {
        private readonly AppDbContext Db;

        public FaleConoscoRepository(AppDbContext db)
        {
            Db = db;
        }

        public async Task Adicionar(FaleConosco faleConosco)
        {
            Db.FaleConosco.Add(faleConosco);
            await SaveChanges();
        }
        public async Task Remover(Guid id)
        {
            var faleConosco = await ObterPorId(id);
            if (faleConosco != null)
            {
                Db.FaleConosco.Remove(faleConosco);
                await SaveChanges();
            }
        }

        public async Task<FaleConosco> ObterPorId(Guid id)
        {
            return await Db.FaleConosco.FindAsync(id);
        }

        public async Task<IEnumerable<FaleConosco>> ObterTodos()
        {
            return await Db.FaleConosco.ToArrayAsync();
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
