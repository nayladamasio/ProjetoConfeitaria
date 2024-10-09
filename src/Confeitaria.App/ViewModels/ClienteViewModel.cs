using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Confeitaria.App.ViewModels
{
    public class ClienteViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome do cliente deve ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Nome Completo")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF do cliente é obrigatório")]
        [StringLength(11, ErrorMessage = "O CPF deve ter entre {1} caracteres")]
        [DisplayName("CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo email é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo email deve ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo celular é obrigatório")]
        [StringLength(9, ErrorMessage = "O campo celular deve ter entre {1} caracteres")]
        [DisplayName("Celular")]
        public string Telefone { get; set; }

        public IEnumerable<PedidoViewModel> Pedidos { get; set; }
    }
}
