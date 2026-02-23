using VH_Burguer.Domains;
using VH_Burguer.DTOs.ProdutoDtos;

namespace VHBurguer.Applications.Conversoes
{
    public class ProdutoParaDTO
    {
        public static LerProdutoDto ConverterParaDto(Produto produto)
        {
            return new LerProdutoDto
            {
                ProdutoID = produto.ProdutoID,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Descricao = produto.Descricao,
                StatusProduto = produto.StatusProduto,
                CategoriaIds = produto.Categoria.Select(c => c.CategoriaID).ToList(),
                Categorias = produto.Categoria.Select(c => c.Nome).ToList(),
                UsuarioID = produto.UsuarioID,
                UsuarioEmail = produto.Usuario.Email
            };
        }
    }
}