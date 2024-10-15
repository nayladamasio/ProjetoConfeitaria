namespace Confeitaria.Business.Models
{
    public class FaleConosco : BaseEntity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Mensagem { get; set; }

        public DateTime DataEnvio { get; set; }

        public FaleConosco()
        {
            DataEnvio = DateTime.Now;
        }

    }
}
