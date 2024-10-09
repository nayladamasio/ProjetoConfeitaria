using System.Globalization;

namespace Confeitaria.Business.Models
{
    public class Cliente : BaseEntity
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        // Relacionamento 
        public IEnumerable<Pedido> Pedidos { get; set; }

    }
}
