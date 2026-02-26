using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VH_Burguer.Applications.Services;
using VH_Burguer.DTOs.PromocaoDTo;
using VH_Burguer.Exceptions;
using VHBurguer.Applications.Services;

namespace VHBurguer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocaoController : ControllerBase
    {
        private readonly PromocaoService _service;

        public PromocaoController(PromocaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerPromocaoDTo>> Listar()
        {
            List<LerPromocaoDTo> promocoes = _service.Listar();
            return Ok(promocoes);
        }

        [HttpGet("{id}")]
        public ActionResult<LerPromocaoDTo> ObterPorId(int id)
        {
            try
            {
                LerPromocaoDTo promocao = _service.ObterPorId(id);
                return Ok(promocao);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Adicionar(CriarPromocaoDTo promoDto)
        {
            try
            {
                _service.Adicionar(promoDto);
                return StatusCode(201);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Atualizar(int id, CriarPromocaoDTo promoDto)
        {
            try
            {
                _service.Atualizar(id, promoDto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
                //return StatusCode(400, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
