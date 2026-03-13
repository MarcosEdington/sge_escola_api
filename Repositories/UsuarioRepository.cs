using System.Text.Json;
using sge_escola_api.Models;

namespace sge_escola_api.Repositories
{
    public class UsuarioRepository
    {
        private readonly string _filePath = "Data/usuarios.json";
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };

        public UsuarioRepository()
        {
            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
            if (!File.Exists(_filePath))
            {
                var admin = new List<Usuario> {
                    new Usuario { Id = 1, Email = "admin@escola.com", Senha = "123", Nome = "Administrador Beija-Flor", Perfil = "Admin", PrimeiroAcesso = false }
                };
                File.WriteAllText(_filePath, JsonSerializer.Serialize(admin, _options));
            }
        }

        // PADRÃO: Centralizamos a leitura aqui (igual ao seu AlunoRepository)
        public List<Usuario> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Usuario>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Usuario>>(json, _options) ?? new List<Usuario>();
        }

        // PADRÃO: Agora o Validar usa o GetAll()
        public Usuario Validar(string email, string senha)
        {
            var usuarios = GetAll();
            return usuarios.FirstOrDefault(u => u.Email.Trim().Equals(email.Trim(), StringComparison.OrdinalIgnoreCase) && u.Senha.Trim() == senha.Trim());
        }

        // PADRÃO: Agora o Adicionar usa o GetAll() e Save()
        public void Adicionar(Usuario usuario)
        {
            var usuarios = GetAll();
            usuario.Id = usuarios.Count > 0 ? usuarios.Max(u => u.Id) + 1 : 1;
            usuarios.Add(usuario);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(usuarios, _options));
        }

        // NOVO: Método Update para salvar alterações (como a senha) com segurança
        public void Update(Usuario usuarioAtualizado)
        {
            var usuarios = GetAll();
            var index = usuarios.FindIndex(u => u.Id == usuarioAtualizado.Id);
            if (index != -1)
            {
                usuarios[index] = usuarioAtualizado;
                File.WriteAllText(_filePath, JsonSerializer.Serialize(usuarios, _options));
            }
        }
    }
}