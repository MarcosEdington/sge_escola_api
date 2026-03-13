namespace sge_escola_api.Models
{
    public class Frequencia
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public DateTime Data { get; set; }
        public bool Presente { get; set; }
        public string TurmaId { get; set; }
    }

}
