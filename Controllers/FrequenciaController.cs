using Microsoft.AspNetCore.Mvc;
using sge_escola_api.Models;
using sge_escola_api.Repositories;
using System;

namespace sge_escola_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FrequenciaController : ControllerBase
    {
        private readonly FrequenciaRepository _repository;

        public FrequenciaController()
        {
            _repository = new FrequenciaRepository();
        }

        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] FrequenciaRequest request)
        {
            if (request == null) return BadRequest("Dados inválidos");

            // Recebe a lista toda de uma vez
            foreach (var p in request.Presencas)
            {
                var freq = new Frequencia
                {
                    AlunoId = p.AlunoId,
                    Presente = p.Presente,
                    Data = DateTime.Parse(request.Data),
                    TurmaId = request.TurmaId
                };
                // Alteração: Apenas prepare a lista, não salve no disco a cada aluno
                _repository.AdicionarSemSalvar(freq);
            }

            _repository.SalvarNoDisco(); // Salva uma única vez após processar tudo
            return Ok(new { message = "Chamada registrada com sucesso!" });
        }

        [HttpGet("consultar/{turmaId}/{data}")]
        public IActionResult Consultar(string turmaId, string data)
        {
            var lista = _repository.GetAll();

            // Filtra apenas o que pertence à turma e data solicitada
            var frequenciasDaTurma = lista.Where(f =>
                f.TurmaId == turmaId &&
                f.Data.ToString("yyyy-MM-dd") == DateTime.Parse(data).ToString("yyyy-MM-dd")
            ).ToList();

            return Ok(frequenciasDaTurma);
        }


    }
}