using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Confeitaria.App.ViewModels
{
    public class EnderecoPedidoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("CEP")]
        [Required(ErrorMessage = "O CEP é obrigatório")]
        [StringLength(8, ErrorMessage = "O CEP deve ter entre {1} caracteres", MinimumLength = 8)]
        public string Cep { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório")]
        [StringLength(100, ErrorMessage = "O estado deve ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Estado { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória")]
        [StringLength(100, ErrorMessage = "A cidade deve ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O bairro é obrigatório")]
        [StringLength(100, ErrorMessage = "O bairro deve ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "O logradouro é obrigatório")]
        [StringLength(100, ErrorMessage = "O logradouro deve ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Logradouro { get; set; }

        [DisplayName("Número")]
        [Required(ErrorMessage = "O número é obrigatório")]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        [HiddenInput]
        public Guid PedidoId { get; set; }
        public PedidoViewModel Pedido { get; set; }
    }
}
