using System;

namespace sge_escola_api.Models
{
    public class EventoCalendario
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string TipoEvento { get; set; } // "Prova", "Feriado", "Evento", "Recesso"
        public int? TurmaId { get; set; }       // Nulo para eventos gerais
        public string Disciplina { get; set; }  // Preenchido quando for Prova
        public bool Ativo { get; set; } = true;
    }
}