using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Confeitaria.App.ViewModels
{
    public class FaleConoscoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome do cliente deve ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo email é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo email deve ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Mensagem:")]
        [Required(ErrorMessage = "O campo Mensagem é obrigatório")]
        public string Mensagem { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataEnvio { get; set; }

    }
}
