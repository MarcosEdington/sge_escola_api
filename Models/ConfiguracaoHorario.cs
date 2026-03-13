namespace sge_escola_api.Models
{
    public class ConfiguracaoHorario
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty; // Ex: "1ª Aula", "Intervalo"
        public string Inicio { get; set; } = string.Empty; // Formato "HH:mm" (Ex: "07:00")
        public string Fim { get; set; } = string.Empty;    // Formato "HH:mm" (Ex: "07:50")
        public bool Ativo { get; set; } = true;
    }
}