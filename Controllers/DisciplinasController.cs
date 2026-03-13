using Microsoft.AspNetCore.Mvc;
using sge_escola_api.Models;
using sge_escola_api.Repositories;

namespace sge_escola_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinasController : ControllerBase
    {
        private readonly DisciplinaRepository _repository;

        public DisciplinasController()
        {
            _repository = new DisciplinaRepository();
        }

        [HttpGet]
        public IActionResult Get() => Ok(_repository.GetAll());

        [HttpPost]
        public IActionResult Post([FromBody] Disciplina disciplina)
        {
            _repository.Save(disciplina);
            return Ok(new { message = "Disciplina salva com sucesso!" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok(new { message = "Disciplina removida!" });
        }
    }
}