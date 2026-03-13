using System.ComponentModel.DataAnnotations;

namespace sge_escola_api.Models
{
    public class Nota
    {
        [Key]
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
        public string Disciplina { get; set; } = string.Empty;

        // Propriedades de Notas
        public decimal Bimestre1 { get; set; }
        public decimal Bimestre2 { get; set; }
        public decimal Bimestre3 { get; set; }
        public decimal Bimestre4 { get; set; }
        public int Faltas { get; set; }

        // CORREÇÃO DOS ERROS:
        // 1. Removi o espaço em "Media Final" (era o erro CS1002)
        // 2. Garanti que as propriedades acima existam antes do cálculo
        public decimal MediaFinal => (Bimestre1 + Bimestre2 + Bimestre3 + Bimestre4) / 4;

        public string Situacao => MediaFinal >= 6 ? "Aprovado" : "Recuperação";
    }
}