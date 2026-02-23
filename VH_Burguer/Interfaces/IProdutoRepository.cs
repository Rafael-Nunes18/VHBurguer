using VH_Burguer.Domains;

namespace VH_Burguer.Interfaces
{
    public interface IProdutoRepository
    {
        List<Produto> Listar();

        byte[] ObterImagem(int id);

        bool NomeExiste(string nome, int? produtoIdAtual = null);
        void Adicionar(Produto produto, List<int> categoriaIds);

        void Atualizar(Produto produto, List<int> categoriaIds);

        void Deletar(int id);
        Produto ObterPorId(int id);
    }
}
