using Confeitaria.Business.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Confeitaria.App.ViewModels
{
    public class ProdutoViewModel
    {
        [Key]
        public Guid Id { get; set; }


        [DisplayName("Nome:")]
        [Required(ErrorMessage = "O nome do produto é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome do produto deve ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [DisplayName("Descrição:")]
        [Required(ErrorMessage = "A descrição do produto é obrigatória")]
        [StringLength(100, ErrorMessage = "A descrição do produto deve ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }

        [DisplayName("Imagem do Produto:")]
        public IFormFile ImageUpload { get; set; }

        public string Imagem { get; set; }

        [DisplayName("Peso:")]
        [Required(ErrorMessage = "O campo peso é obrigatório")]
        public string Peso { get; set; }

        [DisplayName("Valor:")]
        [Required(ErrorMessage = "O valor do produto é obrigatório")]
        public decimal Valor { get; set; }

        [DisplayName("Categoria:")]
        public Categoria Categoria { get; set; }

        [DisplayName("Disponivel")]
        public bool Disponivel { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        public int Quantidade { get; set; }

        public PedidoViewModel Pedido { get; set; }
    }
}
