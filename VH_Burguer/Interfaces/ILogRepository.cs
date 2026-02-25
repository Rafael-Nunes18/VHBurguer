using VH_Burguer.Domains;

namespace VH_Burguer.Interfaces
{
    public interface ILogRepository
    {

        List<Log_AlteracaoProduto> Listar();

        List<Log_AlteracaoProduto> ListarPorProduto(int produtoId);
    }
}
