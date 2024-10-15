using Confeitaria.Business.Models;

namespace Confeitaria.Business.Interfaces
{
    public interface IFaleConoscoRepository
    {
        Task Adicionar(FaleConosco faleConosco);
        Task Remover(Guid id);
        Task<IEnumerable<FaleConosco>> ObterTodos();
        Task<FaleConosco> ObterPorId(Guid id);
        Task<int> SaveChanges();
    }
}
