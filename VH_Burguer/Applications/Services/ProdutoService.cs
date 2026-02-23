using VH_Burguer.Applications.Conversoes;
using VH_Burguer.Applications.Regras;
using VH_Burguer.Domains;
using VH_Burguer.DTOs.ProdutoDtos;
using VH_Burguer.Exceptions;
using VH_Burguer.Interfaces;
using VHBurguer.Applications.Conversoes;


namespace VH_Burguer.Applications.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public List<LerProdutoDto> Listar()
        {
            List<Produto> produtos = _produtoRepository.Listar();

            List<LerProdutoDto> produtoDtos = produtos.Select(ProdutoParaDTO.ConverterParaDto).ToList();

            return produtoDtos;
        }
        public LerProdutoDto ObterPorId(int id)
        {
            Produto produto = _produtoRepository.ObterPorId(id);
            if (produto == null)
            {
                throw new DomainException("Produto não encontrado.");
            }
            return ProdutoParaDTO.ConverterParaDto(produto);
        }
        private static void ValidarCadastro(CriarProdutoDto produtoDto)
        {
            if (string.IsNullOrWhiteSpace(produtoDto.Nome))
            {
                throw new DomainException("O nome do produto é obrigatório!");
            }
            if (produtoDto.Preco < 0)
            {
                throw new DomainException("O preço do produto deve ser maior que 0!");
            }

            if (string.IsNullOrWhiteSpace(produtoDto.Descricao))
            {
                throw new DomainException("A descrição do produto é obrigatória!");
            }
            if (produtoDto.imagem == null || produtoDto.imagem.Length == 0)
            {
                throw new DomainException("A imagem do produto é obrigatória!");
            }
            if (produtoDto.CategoriaIds == null || produtoDto.CategoriaIds.Count == 0)
            {
                throw new DomainException("O produto deve pertencer a pelo menos uma categoria!");
            }
        }
        public byte[] ObterImagem(int id)
        {
            byte[] imagem = _produtoRepository.ObterImagem(id);
            if (imagem == null)
            {
                throw new DomainException("Imagem não encontrada.");
            }
            return imagem;
        }
        public LerProdutoDto Adicionar(CriarProdutoDto produtoDto, int usuarioId)
        {
            ValidarCadastro(produtoDto);
            if (_produtoRepository.NomeExiste(produtoDto.Nome))
            {
                throw new DomainException("Produto já existente.");
            }
            Produto produto = new Produto
            {
                Nome = produtoDto.Nome,
                Preco = produtoDto.Preco,
                Descricao = produtoDto.Descricao,
                Imagem = ImagemParaBytes.ConverterImagem(produtoDto.imagem),
                StatusProduto = true,
                UsuarioID = usuarioId
            };
            _produtoRepository.Adicionar(produto, produtoDto.CategoriaIds);
            return ProdutoParaDTO.ConverterParaDto(produto);
        }
        public LerProdutoDto Atualizar(int id, AtualizarProdutoDto produtoDto)
        {
            HorarioAlteracaoProduto.ValidarHorario();
            Produto produtoBanco = _produtoRepository.ObterPorId(id);
            if (produtoBanco == null)
            {
                throw new DomainException("Produto não encontrado");
            }
            //: serve para passar o valor do parametro
            if (_produtoRepository.NomeExiste(produtoDto.Nome, produtoIdAtual: id))
            {
                throw new DomainException("Já existe outro produto com esse nome.");
            }
            if (produtoDto.CategoriaIds == null || produtoDto.CategoriaIds.Count == 0)
            {
                throw new DomainException("O produto deve pertencer a pelo menos uma categoria!");
            }
            if (produtoDto.Preco < 0)
            {
                throw new DomainException("Preco deve ser maior que 0.");
            }
            produtoBanco.Nome = produtoDto.Nome;
            produtoBanco.Descricao = produtoDto.Descricao;
            produtoBanco.Preco = produtoDto.Preco;

            if (produtoDto.Imagem != null && produtoDto.Imagem.Length > 0)
            {
                produtoBanco.Imagem = ImagemParaBytes.ConverterImagem(produtoDto.Imagem);
            }
            if (produtoDto.StatusProduto.HasValue)
            {
                produtoBanco.StatusProduto = produtoDto.StatusProduto.Value;
            }
            _produtoRepository.Atualizar(produtoBanco, produtoDto.CategoriaIds);
            return ProdutoParaDTO.ConverterParaDto(produtoBanco);
        }

        public void Remover(int id)
        {
            HorarioAlteracaoProduto.ValidarHorario();
            Produto produto = _produtoRepository.ObterPorId(id);
            if (produto == null)
            {
                throw new DomainException("Produto não encontrado!");
            }

            _produtoRepository.Deletar(id);
        }
    }
}