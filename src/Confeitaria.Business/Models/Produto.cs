using Confeitaria.Business.Enums;
using System.Drawing;

namespace Confeitaria.Business.Models
{
    public class Produto : BaseEntity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public string Peso { get; set; }
        public decimal Valor { get; set; }
        public Categoria Categoria { get; set; }
        public bool Disponivel { get; set; }
        public DateTime DataCadastro { get; set; }

        public IEnumerable<PedidoProduto> PedidoProdutos { get; set; }

        public Produto()
        {
            DataCadastro = DateTime.Now;
        }



    }
}
