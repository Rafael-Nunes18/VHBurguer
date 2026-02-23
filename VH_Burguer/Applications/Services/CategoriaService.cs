using VH_Burguer.Domains;
using VH_Burguer.DTOs.CategoriaDTo;
using VH_Burguer.Interfaces;

namespace VH_Burguer.Applications.Services
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository

        public List<LerCategoriaDto> Listar()
        {
            List<Categoria> 
        }










        private static void ValidarNome(string nome)
        {
            if(string IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome é obrigatorio");
            }
        }

        public void Adicionar(CriarCategoriaDTo criarDTo)
        {
            ValidarNome(criarDTo.Nome);

        }

        public void Atualizar(int id, CriarCategoriaDTo criarDto)
        {
            ValidarNome(criarDto.Nome);
            Categoria categoriaBanco = _repository.ObterPorId(id);

            if(categoriaBanco == null)
            {
                thorow new DomainException("Ja existe uma categoria com esse nome");
            }

            categoriaBanco.Nome = criarDto.Nome;
            _repository Atualizar(CategoriaBanco);
        }

        public void Remover(int id)
        {
            Categoria categoriaBanco = _repository.ObterPorId(id);
  
            if(categoriaBanco == null)
            {
                throw new DomainException("categoria nao encontrada");
            }


        }
    
    }
}
            