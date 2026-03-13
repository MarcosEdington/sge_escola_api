using Microsoft.AspNetCore.Mvc;
using sge_escola_api.Models;
using sge_escola_api.Repositories;

namespace sge_escola_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatriculasController : ControllerBase
    {
        private readonly MatriculaRepository _repository;

        public MatriculasController()
        {
            _repository = new MatriculaRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAll());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Matricula matricula)
        {
            if (matricula == null) return BadRequest("Dados da matrícula inválidos");

            matricula.DataMatricula = DateTime.Now;

            // Regra de negócio: Definir situação inicial
            if (string.IsNullOrEmpty(matricula.SituacaoAcademica))
                matricula.SituacaoAcademica = "Cursando";

            _repository.Save(matricula);
            return Ok(new { message = "Matrícula realizada com sucesso!" });
        }
    }
}