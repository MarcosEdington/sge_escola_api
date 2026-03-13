using Microsoft.AspNetCore.Mvc;
using sge_escola_api.Models;
using sge_escola_api.Repositories;

namespace sge_escola_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly AlunoRepository _repository;

        public AlunosController()
        {
            _repository = new AlunoRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAll());
        }

        //[HttpPost]
        //public IActionResult Post([FromBody] Aluno aluno)
        //{
        //    if (aluno == null) return BadRequest("Dados inválidos");

        //    // Mantendo o status vindo do front se não houver lógica de frequência
        //    if (string.IsNullOrEmpty(aluno.StatusMatricula))
        //        aluno.StatusMatricula = "Ativo";

        //    _repository.Save(aluno);
        //    return Ok(new { message = "Aluno cadastrado!" });
        //}

        [HttpPost]
        public IActionResult Post([FromBody] Aluno aluno)
        {
            if (aluno == null) return BadRequest("Dados inválidos");

            if (string.IsNullOrEmpty(aluno.StatusMatricula))
                aluno.StatusMatricula = "Ativo";

            // 1. Salva o Aluno
            _repository.Save(aluno);

            // 2. Cria o usuário automaticamente (aqui está o "pulo do gato")
            // Importante: Seu modelo Aluno precisa ter o campo Senha para isso funcionar
            var userRepo = new UsuarioRepository();
            userRepo.Adicionar(new Usuario
            {
                Email = aluno.RegistroAcademico, // O RA vira o login
                Senha = aluno.Senha,             // A senha que você vai enviar do front
                Nome = aluno.Nome,
                Perfil = "Aluno"
            });

            return Ok(new { message = "Aluno e acesso criados!" });
        }


        // NOVO ENDPOINT: Atualizar (PUT)
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Aluno aluno)
        {
            if (aluno == null || id != aluno.Id) return BadRequest("Dados inconsistentes");
            _repository.Update(aluno);
            return Ok(new { message = "Aluno atualizado com sucesso!" });
        }

        // NOVO ENDPOINT: Deletar (DELETE)
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok(new { message = "Aluno removido com sucesso!" });
        }
    }
}