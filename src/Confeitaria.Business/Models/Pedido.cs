using Confeitaria.Business.Enums;

namespace Confeitaria.Business.Models
{
    public class Pedido : BaseEntity
    {
        public DateTime DataEntrega { get; set; }
        public DateTime DataPedido { get; set; } 
        public Horario HorarioEntrega { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public int QuantProduto { get; set; }

        // Relacionamento
        public Cliente Cliente { get; set; }
        public EnderecoPedido Endereco { get; set; }
        public IEnumerable<Produto> Produtos { get; set; }


        public Pedido()
        {
            DataPedido = DateTime.Now;
        }

    }
}
