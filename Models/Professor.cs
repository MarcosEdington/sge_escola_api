namespace sge_escola_api.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Especializacao { get; set; } = string.Empty;
        public string Status { get; set; } = "Ativo";
        public DateTime DataContratacao { get; set; } = DateTime.Now;
    }
}