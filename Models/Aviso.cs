namespace sge_escola_api.Models
{
    public class Aviso
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;
        public string DataPublicacao { get; set; } = string.Empty;
        public string Prioridade { get; set; } = "Normal"; // Baixa, Normal, Alta
        public string Destino { get; set; } = "Todos"; // Dashboard, Portal, Todos
        public bool Ativo { get; set; } = true;
    }
}