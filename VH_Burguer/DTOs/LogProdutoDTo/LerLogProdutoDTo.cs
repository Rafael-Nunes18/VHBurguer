namespace VH_Burguer.DTOs.LogProdutoDTo
{
    public class LerLogProdutoDTo
    {
        public  int LogID { get; set; }
        public int? ProdutoID { get; set; }
        public string NomeAnterior { get; set; } = null!;
        public decimal? PrecoAnterior { get; set; }
        public DateTime DataAlteracao { get; set; }
    }

}
