using System;
using System.ComponentModel.DataAnnotations;

namespace sge_escola_api.Models
{
    public class Aluno
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string RegistroAcademico { get; set; }
        // Novos campos
        public string NomePai { get; set; }
        public string NomeMae { get; set; }
        public string NomeResponsavel { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public bool PossuiPendenciaDocumental { get; set; }
        public string DocumentosPendentes { get; set; } // Armazena a lista/tipo da pendência

        public DateTime DataNascimento { get; set; }
        public string StatusMatricula { get; set; }
        public double PercentualFrequencia { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public string Senha { get; set; }
    }
}