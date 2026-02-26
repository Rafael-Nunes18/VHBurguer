using VH_Burguer.Domains;
using VH_Burguer.DTOs.LogProdutoDTo;
using VH_Burguer.Interfaces;

namespace VHBurguer.Applications.Services
{
    public class LogAlteracaoProdutoService
    {
        private readonly ILogRepository _repository;

        public LogAlteracaoProdutoService(ILogRepository repository)
        {

            _repository = repository;
        }

        public List<LerLogProdutoDTo> Listar()
        {
            List<Log_AlteracaoProduto> logs = _repository.Listar();

            List<LerLogProdutoDTo> listaLogProduto = logs.Select(log => new LerLogProdutoDTo
            {
                LogID = log.Log_AlteracaoProdutoID,
                ProdutoID = log.ProdutoID,
                NomeAnterior = log.NomeAnterior,
                PrecoAnterior = log.PrecoAnterior,
                DataAlteracao = log.DataAlteracao
            }).ToList();

            return listaLogProduto;
        }

        public List<LerLogProdutoDTo> ListarPorProduto(int produtoId)
        {
            List<Log_AlteracaoProduto> logs = _repository.ListarPorProduto(produtoId);

            List<LerLogProdutoDTo> listaLogProduto = logs.Select(log => new LerLogProdutoDTo
            {
                LogID = log.Log_AlteracaoProdutoID,
                ProdutoID = log.ProdutoID,
                NomeAnterior = log.NomeAnterior,
                PrecoAnterior = log.PrecoAnterior,
                DataAlteracao = log.DataAlteracao
            }).ToList();

            return listaLogProduto;
        }

    }
}