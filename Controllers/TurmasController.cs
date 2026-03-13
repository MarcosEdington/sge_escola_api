using Microsoft.AspNetCore.Mvc;
using sge_escola_api.Models;
using sge_escola_api.Repositories;

namespace sge_escola_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmasController : ControllerBase
    {
        private readonly TurmasRepository _repository;

        public TurmasController()
        {
            _repository = new TurmasRepository();
        }

        [HttpGet]
        public IActionResult Get() => Ok(_repository.GetAll());

        [HttpPost]
        public IActionResult Post([FromBody] Turma turma)
        {
            _repository.Save(turma);
            return Ok(new { message = "Turma criada com sucesso!" });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Turma turma)
        {
            turma.Id = id;
            _repository.Update(turma);
            return Ok(new { message = "Turma atualizada!" });
        }

       
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok(new { message = "Turma removida com sucesso!" });
        }
    }
}