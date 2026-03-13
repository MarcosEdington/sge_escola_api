using Microsoft.AspNetCore.Mvc;
using sge_escola_api.Models;
using sge_escola_api.Repositories;

namespace sge_escola_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtividadesController : ControllerBase
    {
        private readonly AtividadeRepository _repository;

        public AtividadesController()
        {
            _repository = new AtividadeRepository();
        }

        [HttpGet]
        public IActionResult Get() => Ok(_repository.GetAll());

        [HttpGet("turma/{turmaId}")]
        public IActionResult GetByTurma(int turmaId)
        {
            var lista = _repository.GetAll().Where(a => a.TurmaId == turmaId).ToList();
            return Ok(lista);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Atividade atividade)
        {
            _repository.Save(atividade);
            return Ok(new { message = "Atividade salva com sucesso!" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok(new { message = "Atividade excluída com sucesso!" });
        }
    }
}