using Microsoft.AspNetCore.Mvc;
using sge_escola_api.Models;
using sge_escola_api.Repositories;

namespace sge_escola_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotasController : ControllerBase
    {
        private readonly NotasRepository _repository;

        public NotasController()
        {
            _repository = new NotasRepository();
        }

        // Busca todas as notas (útil para relatórios gerais)
        [HttpGet]
        public IActionResult Get() => Ok(_repository.GetAll());

        // Busca as notas de uma turma específica
        [HttpGet("turma/{turmaId}")]
        public IActionResult GetByTurma(int turmaId)
        {
            var notas = _repository.GetAll().Where(n => n.TurmaId == turmaId).ToList();
            return Ok(notas);
        }

        // Salva ou Atualiza várias notas de uma vez (Diário de Classe)
        [HttpPost("salvar-massa")]
        public IActionResult PostMassa([FromBody] List<Nota> notas)
        {
            if (notas == null || !notas.Any())
                return BadRequest("Nenhuma nota fornecida.");

            try
            {
                _repository.SaveMassa(notas);
                return Ok(new { message = "Notas processadas com sucesso!" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }


        [HttpGet("aluno/{alunoId}")]
        public IActionResult GetByAluno(int alunoId)
        {
            // LOG DE SEGURANÇA: Isso vai aparecer na janela de comando da sua API (onde ela roda)
            Console.WriteLine($"Buscando notas para o AlunoID: {alunoId}");

            if (alunoId <= 0)
                return BadRequest("ID de aluno inválido.");

            var notas = _repository.GetAll().Where(n => n.AlunoId == alunoId).ToList();

            // Se não encontrar notas, retorna um array vazio em vez de erro
            return Ok(notas);
        }


    }
}