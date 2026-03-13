using Microsoft.AspNetCore.Mvc;
using sge_escola_api.Models;
using sge_escola_api.Repositories;

namespace sge_escola_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinanceiroController : ControllerBase
    {
        private readonly MensalidadeRepository _repository;

        public FinanceiroController()
        {
            _repository = new MensalidadeRepository();
        }

        [HttpGet("mensalidades")]
        public IActionResult GetMensalidades() => Ok(_repository.GetAll());

        [HttpGet("aluno/{alunoId}")]
        public IActionResult GetPorAluno(int alunoId) => Ok(_repository.GetPorAluno(alunoId));

        [HttpPost("gerar")]
        public IActionResult GerarMensalidade([FromBody] Mensalidade mensalidade)
        {
            _repository.Adicionar(mensalidade);
            return Ok(mensalidade);
        }

        [HttpPatch("pagar/{id}")]
        public IActionResult RegistrarPagamento(int id)
        {
            _repository.AtualizarStatus(id, "Pago", DateTime.Now);
            return Ok(new { message = "Pagamento registrado com sucesso!" });
        }
    }
}