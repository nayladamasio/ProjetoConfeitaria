namespace Confeitaria.Business.Models
{
    public class EnderecoPedido : BaseEntity
    {
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }

        // Relacionamento
        public Guid PedidoId { get; set; }
        public Pedido Pedido { get; set; }
    }
}
