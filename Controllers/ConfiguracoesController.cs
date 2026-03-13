using Microsoft.AspNetCore.Mvc;
using sge_escola_api.Models;
using sge_escola_api.Repositories; // Adicione esta linha
using System.Collections.Generic;

namespace sge_escola_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfiguracoesController : ControllerBase
    {
        private readonly ConfiguracaoRepository _repository;

        public ConfiguracoesController()
        {
            _repository = new ConfiguracaoRepository();
        }

        [HttpGet("horarios")]
        public IActionResult Get()
        {
            var horarios = _repository.GetAll();
            return Ok(horarios);
        }

        [HttpPost("horarios")]
        public IActionResult Post([FromBody] List<ConfiguracaoHorario> horarios)
        {
            if (horarios == null) return BadRequest("Lista de horários inválida");

            // Salva no arquivo JSON usando o Repository padrão
            _repository.SaveAll(horarios);

            return Ok(new { message = "Configurações de horários salvas no arquivo JSON!" });
        }
    }
}