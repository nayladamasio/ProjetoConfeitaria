using Confeitaria.Business.Enums;
using Confeitaria.Business.Models;

namespace Confeitaria.Business.Interfaces
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
        Task<Produto> ObterProdutoId(Guid id);
      
    }
}
