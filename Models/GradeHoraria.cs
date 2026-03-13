using System.ComponentModel.DataAnnotations;

namespace sge_escola_api.Models
{
    public class GradeHoraria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TurmaId { get; set; }

        [Required]
        public int ProfessorId { get; set; }

        [Required]
        public string Disciplina { get; set; } = string.Empty;

        [Required]
        public string DiaSemana { get; set; } = string.Empty; // Segunda, Terça...

        [Required]
        public string HorarioInicio { get; set; } = string.Empty; // Ex: "07:00"

        [Required]
        public string HorarioFim { get; set; } = string.Empty;    // Ex: "07:50"
    }
}