using Microsoft.AspNetCore.Mvc;
using sge_escola_api.Models;
using sge_escola_api.Repositories;
using System.Text.Json;             // ADICIONE ESTA LINHA (Resolve os erros de JSON)
using System.Collections.Generic;   // ADICIONE ESTA LINHA (Para usar List<Usuario>)

namespace sge_escola_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UsuarioRepository _repository;

        public AuthController()
        {
            _repository = new UsuarioRepository();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var usuario = _repository.Validar(request.Email, request.Senha);

            if (usuario == null)
                return Unauthorized(new { message = "E-mail ou senha inválidos" });

            return Ok(new
            {
                id = usuario.Id,
                nome = usuario.Nome,
                perfil = usuario.Perfil,
                primeiroAcesso = usuario.PrimeiroAcesso
            });
        }

        [HttpPost("atualizar-senha-inicial")]
        public IActionResult AtualizarSenha([FromBody] TrocarSenhaRequest request)
        {
            // 1. Lê o arquivo JSON
            var json = System.IO.File.ReadAllText("Data/usuarios.json");

            // Usando opções para não ter erro de maiúsculas/minúsculas no JSON
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var usuarios = JsonSerializer.Deserialize<List<Usuario>>(json, options);

            // 2. Busca o usuário
            var usuario = usuarios.FirstOrDefault(u => u.Nome == request.Login || u.Email == request.Login);

            if (usuario != null)
            {
                usuario.Senha = request.NovaSenha;
                usuario.PrimeiroAcesso = false; // Aqui ele desativa o loop do primeiro acesso
                usuario.UltimoAcesso = DateTime.Now;

                // 3. Grava de volta no JSON bem formatado
                var novoJson = JsonSerializer.Serialize(usuarios, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText("Data/usuarios.json", novoJson);

                return Ok(new { message = "Senha atualizada com sucesso" });
            }

            return NotFound(new { message = "Usuário não encontrado" });
        }
    }

    public class TrocarSenhaRequest
    {
        public string Login { get; set; }
        public string NovaSenha { get; set; }
    }
}