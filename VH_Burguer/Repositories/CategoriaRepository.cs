using Microsoft.Identity.Client;
using VH_Burguer.Contexts;
using VH_Burguer.Domains;
using VH_Burguer.Interfaces;

namespace VH_Burguer.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly VH_BurguerContext _context;

        public CategoriaRepository(VH_BurguerContext context)
        {
            _context = context;
        }

        public List<Categoria> Listar()
        {
            return _context.Categoria.ToList();
        }

        public Categoria ObterPorID(int id)
        {
            Categoria categoria = _context.Categoria.FirstOrDefault(c => c.CategoriaID == id);
            return categoria;

        }

        public bool NomeExiste(string nome, int? categoriaIdAtual = null)
        {
            var consulta = _context.Categoria.AsQueryable();


            if(categoriaIdAtual.HasValue)
            {
                consulta = consulta.Where(categoria => categoria.CategoriaID != categoriaIdAtual.Value);
            }

            return consulta.Any(c => c.Nome == nome);
        }

    public void Adicionar(Categoria categoria)
        {
            _context.Categoria.Add(Categoria);
            _context.SaveChanges();
        }

        public void Atualizar(Categoria categoria)
        {
            _context.Categoria.Add(categoria);
            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Categoria categoriaBanco = _context.Categoria, FirstOrDefault(c => c.CategoriaID == id);

            if(categoriaBanco ==  null)
        }

        }
    }
}
