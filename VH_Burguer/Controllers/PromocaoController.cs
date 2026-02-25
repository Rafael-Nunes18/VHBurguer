using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VH_Burguer.Applications.Services;
using VH_Burguer.DTOs.PromocaoDTo;

namespace VH_Burguer.Controllers
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
            List<LerPromocaoDTo> promocoes = _service.Listar()
        }


        [HttpPost]
        [Authorize]

        public ActionResult Adicionar(CriarPromocaoDTo promoDto)
        {
            try
            {
                _service.Adicionar(promoDto);
                return StatusCode(200);

            }


        }
    }
        [HttpPut("{id}")]
        [Authorize]

        public ActionResult Atualizar(int id)
        {


        }




        [HttpDelete]
    }
}
