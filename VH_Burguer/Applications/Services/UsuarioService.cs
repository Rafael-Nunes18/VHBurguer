using System.Security.Cryptography;
using System.Text;
using VH_Burguer.Domains;
using VH_Burguer.DTOs.UsuarioDtos;
using VH_Burguer.Exceptions;
using VH_Burguer.Interfaces;

namespace VH_Burguer.Applications.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        private static LerUsuarioDto LerDto(Usuario usuario)
        {
            LerUsuarioDto LerUsuario = new LerUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                StatusUsuario = usuario.StatusUsuario ?? true 
            };
            return LerUsuario;
        }

        public List<LerUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();
            List<LerUsuarioDto> usuariosDto = usuarios.Select(usuarioBanco => LerDto(usuarioBanco)) 
                .ToList();
            return usuariosDto;

        }

        private static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new DomainException("Email invalido.");
            }
        }

        private static byte[] HashSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new DomainException("Senha e obrigatoria.");
            }

            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }
        public LerUsuarioDto ObterPorId(int id)
        {
            Usuario? usuario = _repository.ObterPorId(id);

            if (usuario == null)
            {
                throw new DomainException("Usuario nao existe");
            }

            return LerDto(usuario);
        }

        public LerUsuarioDto ObterPorEmail(string email)
        {
            Usuario? usuario = _repository.ObterPorEmail(email);

            if (usuario == null)
            {
                throw new DomainException("Usuario nao existe");
            }

            return LerDto(usuario);
        }

        public LerUsuarioDto Adicionar(CriarUsuarioDto usuarioDto)
        {
            ValidarEmail(usuarioDto.Email);

            if (_repository.EmailExiste(usuarioDto.Email))
            {
                throw new DomainException("Ja existe um usuario com este e-mail");
            }

            Usuario usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = HashSenha(usuarioDto.Senha),
                StatusUsuario = true
            };

            _repository.Adicionar(usuario);

            return LerDto(usuario); 


        }

        public LerUsuarioDto Atualizar (int id, CriarUsuarioDto usuarioDto)
        {

            Usuario ? usuarioBanco = _repository.ObterPorId(id);

            if (usuarioBanco == null)
            {
                throw new DomainException("Usuario nao encontrado");
            }

            ValidarEmail(usuarioDto.Email);

            Usuario? usuarioComMesmoEmail = _repository.ObterPorEmail(usuarioDto.Email);

            if (usuarioComMesmoEmail != null && usuarioComMesmoEmail.UsuarioID != id)
            {
                throw new DomainException("Ja existe um usuario com este e-mail");
            }

            usuarioBanco.Nome = usuarioDto.Nome;
            usuarioBanco.Email = usuarioDto.Email;
            usuarioBanco.Senha = HashSenha(usuarioDto.Senha);

            _repository.Atualizar(usuarioBanco);

            return LerDto(usuarioBanco);

        }

        public void Remover(int id)
        {
            Usuario ? usuario = _repository.ObterPorId(id);

            if (usuario == null)
            {
                throw new DomainException("Usuario nao encontrado.");
            }


            _repository.Remover(id);
        }







    }
}
