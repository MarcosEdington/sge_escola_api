using Microsoft.AspNetCore.Mvc;
using sge_escola_api.Models;
using sge_escola_api.Repositories;

namespace sge_escola_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Isso cria a rota /api/Professores
    public class ProfessoresController : ControllerBase
    {
        private readonly ProfessoresRepository _repository;

        public ProfessoresController()
        {
            _repository = new ProfessoresRepository();
        }

        [HttpGet]
        public IActionResult Get() => Ok(_repository.GetAll());

        [HttpPost]
        public IActionResult Post([FromBody] Professor professor)
        {
            _repository.Add(professor);
            return Ok(professor);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Professor professor)
        {
            _repository.Update(id, professor);
            return Ok(professor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok();
        }
    }
}