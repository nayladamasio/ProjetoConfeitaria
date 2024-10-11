namespace Confeitaria.Business.Models
{
    public class PedidoProduto 
    {
        public int Quantidade { get; set; }

        // Relacionamento
        public Guid PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; }
    }
}
