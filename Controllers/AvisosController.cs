using Microsoft.AspNetCore.Mvc;
using sge_escola_api.Models;
using sge_escola_api.Repositories;

namespace sge_escola_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvisosController : ControllerBase
    {
        private readonly AvisosRepository _repository;

        public AvisosController()
        {
            _repository = new AvisosRepository();
        }

        [HttpGet]
        public IActionResult Get() => Ok(_repository.GetAll());

        [HttpPost]
        public IActionResult Post([FromBody] Aviso aviso)
        {
            _repository.Save(aviso);
            return Ok(new { message = "Aviso criado com sucesso!" });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Aviso aviso)
        {
            aviso.Id = id;
            _repository.Update(aviso);
            return Ok(new { message = "Aviso atualizado!" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok(new { message = "Aviso removido com sucesso!" });
        }
    }
}