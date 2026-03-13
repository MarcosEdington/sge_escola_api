using Microsoft.AspNetCore.Mvc;
using sge_escola_api.Models;
using sge_escola_api.Repositories;
using System.Linq;

namespace sge_escola_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarioController : ControllerBase
    {
        private readonly CalendarioRepository _repository;

        public CalendarioController()
        {
            _repository = new CalendarioRepository();
        }

        [HttpGet]
        public IActionResult Get() => Ok(_repository.GetAll());

        // Endpoint específico para o Portal do Aluno (Agenda de Provas)
        [HttpGet("provas/turma/{turmaId}")]
        public IActionResult GetProvasPorTurma(int turmaId)
        {
            var provas = _repository.GetAll()
                .Where(e => e.TurmaId == turmaId && e.TipoEvento == "Prova" && e.Ativo)
                .OrderBy(e => e.DataInicio)
                .ToList();
            return Ok(provas);
        }

        [HttpPost]
        public IActionResult Post([FromBody] EventoCalendario evento)
        {
            if (evento == null) return BadRequest("Dados inválidos");
            _repository.Save(evento);
            return Ok(new { message = "Evento de calendário salvo com sucesso!" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok(new { message = "Evento removido com sucesso!" });
        }
    }
}