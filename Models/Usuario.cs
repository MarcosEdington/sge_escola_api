using System.ComponentModel.DataAnnotations;

namespace sge_escola_api.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        public string Nome { get; set; }

        // Perfil: "Admin", "Secretaria", "Professor", "Aluno"
        public string Perfil { get; set; }

        public DateTime UltimoAcesso { get; set; } = DateTime.Now;

        public bool PrimeiroAcesso { get; set; } = true;
    }
}