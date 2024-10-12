using Confeitaria.Business.Models;

namespace Confeitaria.Business.Interfaces
{
    public interface IPedidoProdutoRepository 
    {
        Task<IEnumerable<PedidoProduto>> ObterPorPedido(Guid pedidoId);
        Task<IEnumerable<PedidoProduto>> ObterTodos();
        Task<PedidoProduto> ObterPorId(Guid id);
        Task Adicionar(PedidoProduto pedidoProduto);
        Task Atualizar(PedidoProduto pedidoProduto);
        Task Remover(Guid id);
        Task<int> SaveChanges();


    }
}
