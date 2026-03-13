namespace sge_escola_api.Models
{
    public class Atividade
    {
        public int Id { get; set; }
        public int TurmaId { get; set; }
        public string Disciplina { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string DataEntrega { get; set; } = string.Empty; // Formato "dd/MM/yyyy"
        public string Status { get; set; } = "Pendente"; // Pendente, Concluída, etc.
    }
}