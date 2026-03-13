using Microsoft.AspNetCore.Mvc;
using sge_escola_api.Models;
using System.Collections.Generic;
using System.Linq;

namespace sge_escola_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeHorariaController : ControllerBase
    {
        // CORREÇÃO: A lista deve ser de GradeHoraria, não de ConfiguracaoHorario
        private static List<GradeHoraria> _grades = new List<GradeHoraria>();
        private static int _nextId = 1;

        [HttpGet("turma/{turmaId}")]
        public IActionResult GetByTurma(int turmaId)
            => Ok(_grades.Where(g => g.TurmaId == turmaId).ToList());

        [HttpPost]
        public IActionResult Post([FromBody] GradeHoraria grade)
        {
            // Validação de choque de horário:
            // Agora o compilador vai encontrar as propriedades ProfessorId, DiaSemana, etc.
            var conflito = _grades.Any(g =>
                g.ProfessorId == grade.ProfessorId &&
                g.DiaSemana == grade.DiaSemana &&
                g.HorarioInicio == grade.HorarioInicio);

            if (conflito) return BadRequest("Este professor já tem aula neste horário!");

            grade.Id = _nextId++;
            _grades.Add(grade);
            return Ok(grade);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _grades.RemoveAll(g => g.Id == id);
            return Ok();
        }
    }
}