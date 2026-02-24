namespace VH_Burguer.DTOs.PromocaoDTo
{
    public class CriarPromocaoDTo
    {
        public string Nome { get; set; } = null!;

        public DateTime DataExpiracao { get; set; }

        public bool StatusPromocao { get; set; }
    }
}
