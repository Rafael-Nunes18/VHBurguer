using VH_Burguer.Applications.Regras;
using VH_Burguer.Domains;
using VH_Burguer.DTOs.PromocaoDTo;
using VH_Burguer.Exceptions;
using VH_Burguer.Interfaces;

namespace VH_Burguer.Applications.Services
{
    public class PromocaoService
    {
        private readonly IPromocaoRepository _repository;

        public PromocaoService(IPromocaoRepository repository)
        {
            _repository = repository;
        }

        public List<LerPromocaoDTo> Listar()
        {
            List<Promocao> promocoes = _repository.Listar();

            List<LerPromocaoDTo> promocaoDto = promocoes.Select
              (promocao => new LerPromocaoDTo
              {
                  PromocaoID = promocao.PromocaoID,
                  Nome = promocao.Nome,
                  DataExpiracao = promocao.DataExpiracao,
                  StatusPromocao = promocao.StatusPromocao

              }).ToList();

            return promocaoDto;
        }


            public LerPromocaoDTo ObterPorId(int id)
        {
            Promocao promocao = _repository.ObterPorID(id);

            if(promocao == null)
            {
                throw new DomainException("Promocao nao encontrada.");
            }

            LerPromocaoDTo promocaoDTo = new LerPromocaoDTo
            {
                PromocaoID = promocao.PromocaoID,
                Nome = promocao.Nome,
                DataExpiracao = promocao.DataExpiracao,
                StatusPromocao = promocao.StatusPromocao

                return promocaoDTo;
            }
        private static void ValidarDataExpiracao(DateTime dataExpiracao)
        {
            if (dataExpiracao <= DateTime.Now)
            {
                throw new DomainException("Nome é obrigatorio");
            }
        }
        public void Adicionar(CriarPromocaoDTo promocaoDTo)
        {
            ValidarNome(promocaoDTo.Nome);
            ValidarDataExpiracaoPromocao.ValidarDataExpiracao
                (promocaoDTo.DataExpiracao);

            if (_repository.NomeExiste(promocaoDTo.Nome))
            {
                throw new DomainException("Promocao ja existente");
            }

            Promocao promocao = new Promocao();



        }

       public void Atualizar(int id, CriarPromocaoDTo promoDto)
        {
            ValidarNome(promoDto.Nome);

            Promocao promocaoBanco = _repository.ObterPorID(id);

            if(promocaoBanco == null)
            {
                throw new DomainException("Promocao nao encontrada.");
            }

            if (_repository.NomeExiste(promoDto.Nome,promocaoBanco: id))
            {
                throw new DomainException("Ja existe outra promocao com esse nome");
            }

            promocaoBanco.Nome = promoDto.Nome;
            promocaoBanco.DataExpiracao = promoDto.DataExpiracao;
            promocaoBanco.StatusPromocao = promoDto.StatusPromocao;

            _repository.Atualizar(promocaoBanco);
        }

        public void Remover(int id)
        {
            Promocao promocaoBanco = _repository.ObterPorID(id);

            if(promocaoBanco == null)
            {
                throw new DomainException("Promocao nao encontrado");
            }
            _repository.Remover(id);
        }
    }
}

