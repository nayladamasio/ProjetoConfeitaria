using Confeitaria.Business.Models;

namespace Confeitaria.Business.Interfaces
{
    public interface IPedidoRepository : IBaseRepository<Pedido>
    {
        Task<Pedido> ObterPedidoEndereco(Guid id);
        //Task<Pedido> ObterPedidoProdutos(Guid id);
    }
}
