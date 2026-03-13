using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sge_escola_api.Models
{
    public class Turma
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } // Ex: 1º Ano A
        public string AnoLetivo { get; set; } // Ex: 2026
        public string Turno { get; set; } // Matutino, Vespertino, Noturno
        public int Capacidade { get; set; }

        // Lista de IDs dos alunos que pertencem a esta turma (Enturmação)
        public List<int> AlunosIds { get; set; } = new List<int>();
    }
}