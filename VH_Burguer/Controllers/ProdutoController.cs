using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VH_Burguer.Applications.Services;
using VH_Burguer.DTOs.ProdutoDtos;
using VH_Burguer.Exceptions;
using VHBurguer.DTOs.ProdutoDto;

namespace VH_Burguer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _service;
        public ProdutoController(ProdutoService service)
        {
            _service = service;
        }
        private int ObterUsuarioIdLogado()
        {
            
            string? idTexto = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(idTexto))
            {
                throw new DomainException("Usuário não autenticado.");
            }

            return int.Parse(idTexto);
        }

        [HttpGet]
        public ActionResult<List<LerProdutoDto>> Listar()
        {
            List<LerProdutoDto> produtos = _service.Listar();
            return Ok(produtos);
        }
        [HttpGet("{id}")]
        public ActionResult<LerProdutoDto> ObterPorId(int id)
        {
            LerProdutoDto produtoDto = _service.ObterPorId(id);
            if (produtoDto == null)
            {
                return NotFound();
            }
            return Ok(produtoDto);
        }

        [HttpGet("{id}/imagem")]
        public IActionResult ObterImagem(int id)
        {
            try
            {
                var imagem = _service.ObterImagem(id);

                return File(imagem, "image/jpeg");
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        //indica que recebe dados no formato multipart/form-data, necessário para upload de arquivos
        [Consumes("multipart/form-data")]
        [Authorize] //exige login para acessar esse endpoint
                    //FromForm indica que os dados do produto serão enviados como parte de um formulário da requisição, necessário para upload de arquivos
        public ActionResult Adicionar([FromForm] CriarProdutoDto criarProdutoDto)
        {
            try
            {
                int usuarioId = ObterUsuarioIdLogado();
                _service.Adicionar(criarProdutoDto, usuarioId);
                return StatusCode(201); //Created
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public ActionResult Atualizar(int id, [FromForm] AtualizarProdutoDto produtoDto)
        {
            try
            {
                _service.Atualizar(id, produtoDto);
                return NoContent(); //204 - sem conteúdo, indica que a atualização foi bem sucedida mas não retorna dados
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent(); //204 - sem conteúdo, indica que a exclusão foi bem sucedida mas não retorna dados
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}

