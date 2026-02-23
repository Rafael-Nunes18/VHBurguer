using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VH_Burguer.Applications.Services;
using VH_Burguer.Domains;
using VH_Burguer.DTOs.CategoriaDTo;
using VH_Burguer.Exceptions;

namespace VH_Burguer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly CategoriaService service;

        public CategoriaController(CategoriaService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult<List>LerCategoriaDto>> Listar()
        {
            List<LerCategoriaDto> categoria = _service.Listar()
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
            catch(DomainException ex)
            {
                return BadRequest(ex.Message);
            }      
        }
    }
}