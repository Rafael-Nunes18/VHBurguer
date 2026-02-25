using VH_Burguer.DTOs.LogProdutoDTo;
using VH_Burguer.Interfaces;

namespace VH_Burguer.Applications.Services
{
    public class LogService
    {
        private readonly ILogRepository _repository
    }

    public List<LerLogProdutoDTo> listaLogProduto = logs.Select(log => new LerLogProdutoDTo)
    {
            logId = log.Log_AlteracaoProdutoID,
            ProdutoID = log.ProdutoID,
            NomeAnterior = log.NomeAnterior,
            PrecoAnteriro = log.PrecoAnterior,
            DataAlteracao = LogService.DataAlteracao
}).ToList();

return listaLogProduto;

    }

public List<>
}
