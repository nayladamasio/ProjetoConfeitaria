using Confeitaria.Business.Models;

namespace Confeitaria.Business.Interfaces
{
    public interface IEnderecoPedidoRepository : IBaseRepository<EnderecoPedido>
    {
        Task<EnderecoPedido> ObterEnderecoPorPedido(Guid pedidoId);
    }
}
