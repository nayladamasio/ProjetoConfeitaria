using Confeitaria.Business.Enums;
using Confeitaria.Business.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Confeitaria.App.ViewModels
{
    public class PedidoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Data da Entrega")]
        public DateTime DataEntrega { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataPedido { get; set; }

        [DisplayName("Horario da Entrega")]
        public Horario HorarioEntrega { get; set; }

        [DisplayName("Forma de Pagamento")]
        public FormaPagamento FormaPagamento { get; set; }

        [DisplayName("Quantidade")]
        public int QuantProduto { get; set; }

        // Relacionamento
        public ClienteViewModel Cliente { get; set; }
        public EnderecoPedidoViewModel Endereco { get; set; }
        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
    }
}
