using System;

namespace sge_escola_api.Models
{
    public class Matricula
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public string Turma { get; set; }
        public string AnoLetivo { get; set; }
        public DateTime DataMatricula { get; set; }
        public bool DocumentacaoCompleta { get; set; }
        public string SituacaoAcademica { get; set; }
    }
}