namespace VH_Burguer.DTOs.ProdutoDtos
{
    public class CriarProdutoDto
    {
        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public string Descricao { get; set; } = null!;

        public IFormFile imagem { get; set; } = null!;

        public List<int> CategoriaIds { get; set; } = new();

    }
}
