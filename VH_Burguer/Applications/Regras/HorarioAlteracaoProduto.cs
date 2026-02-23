using VH_Burguer.Exceptions;

namespace VH_Burguer.Applications.Regras
{
    public class HorarioAlteracaoProduto
    {

        public static void ValidarHorario()
        {
            var agora = DateTime.Now.TimeOfDay;
            var abertura = new TimeSpan(11, 0,0);
            var fechamento = new TimeSpan(22,0,0);

            var estarAberto = agora >= abertura && agora <= fechamento;

            if (estarAberto)
            {
                throw new DomainException("Produto so pode ser alterado depois do horario de funcionamento");
            }
        }
    }
}
