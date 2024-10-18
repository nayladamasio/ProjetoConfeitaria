using Confeitaria.Business.Models;

namespace Confeitaria.Business.Interfaces
{
    public interface IPedidoRepository : IBaseRepository<Pedido>
    {
        Task<Pedido> ObterPedidoEndereco(Guid id);
        Task<Pedido> ObterPedidoCliente(Guid id);
        Task<Pedido> ObterPedidoClienteEndereco(Guid id);
        Task<Pedido> ObterPedidoCompleto(Guid id);
        Task<IEnumerable<Pedido>> ObterPedidoECliente();

    }
}
