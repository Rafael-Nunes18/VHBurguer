using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using VH_Burguer.Contexts;
using VH_Burguer.Domains;
using VH_Burguer.Interfaces;

namespace VH_Burguer.Repositories
{
    public class ProdutoRepository : IProdutoRepositry
    {
        private readonly VH_BurguerContext _context;
    }

    public ProdutoRepository(VH_BurguerContext context)
        {
            _context = context;
        }

        public List<Produto> Listar()
        {
            List<Produto> produtos = _context.Produto
            .Include(ProdutoDb => ProdutoDb.Categoria)
            .Include(ProdutoDb => ProdutoDb.Usuario)
            .ToList();

            return produtos;
        }


        public Produto ObterPorID(int id)
        {
            Produto? produto = _context.Produto
                .Include(ProdutoDb => ProdutoDb.Categoria)
                .Include(ProdutoDb => ProdutoDb.Usuario)

                .FirstOrDefault(produtoDb => produtoDb.ProdutoID == id);

            return produto;
        }
        public bool NomeExiste(string nome, int? produtoIdAtual = null)
        {
            var produtoConsultado = _context.Produto.AsQueryable();


            if (produtoIdAtual.HasValue)
            {
                produtoConsultado = produtoConsultado.Where(produto => produto.ProdutoID != produtoIdAtual.Value);
            }

            return produtoConsultado.Any(produto => produto.Nome == nome);
        }

        public byte[] ObterImagem(int id)
        {
            var produto = _context.Produto
            .Where(produto => produto.ProdutoID == id)
            .Select(produto => produto.Imagem)
            .FirstOrDefault();

            return produto;
        }

        public void Adicionar(Produto produto, List<int> categoriaIds)
        {
            List<Categoria> Categoria = _context.Categoria
            .Where(Categoria => categoriaIds.Contains(Categoria.CategoriaID))
            .ToList();

            produto.Categoria = Categoria;

            _context.Produto.Add(produto);
            _context.SaveChanges();
        }

        public void Atualizar(Produto produto, List<int> categoriaIds)
        {
            Produto? produtoBanco = _context.Produto
            .Include(produto => produto.Categoria)
            .FirstOrDefault(produtoAux => produtoAux.ProdutoID == produto.ProdutoID);
        
             if(produtoBanco == null)
            {
                return;
            }


        produtoBanco.Nome = produto.Nome;
        produtoBanco.Preco = produto.Preco;
        produtoBanco.Descricao = produto.Descricao;

        if(produto.Imagem != null && produto.Imagem.Length > 0)
        {
        produtoBanco.Imagem = produto.Imagem;
        }
              

            if (produto.StatusProduto.HasValue)
            {

            }



            foreach(var categoria in categoria)
            {
                produtoBanco.Categoria.Add(categoria);
            }

            _context.SaveChanges();

            public void Remover(int id)
        {
            Produto? produto = _context.Produto.FirstOrDefault(produto => produto.ProdutoID == id);

            if (produto == null)
            {

            }
        }







        }
    }
}

