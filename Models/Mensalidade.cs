using System;

namespace sge_escola_api.Models
{
    public class Mensalidade
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public string NomeAluno { get; set; } // Facilitar a exibição na tabela
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; } // Nullable pois pode não ter sido paga ainda
        public string Status { get; set; } // "Pendente", "Pago", "Atrasado"
        public string MesReferencia { get; set; } // Ex: "Março/2024"
    }
}