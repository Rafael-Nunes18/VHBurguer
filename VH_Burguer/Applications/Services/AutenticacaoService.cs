using VH_Burguer.Applications.Autenticacao;
using VH_Burguer.Controllers;
using VH_Burguer.Domains;
using VH_Burguer.DTOs.AutenticacaoDTo;
using VH_Burguer.Exceptions;
using VH_Burguer.Interfaces;


namespace VH_Burguer.Applications.Services
{
    public class AutenticacaoService
    {
        private readonly IUsuarioRepository _repository;
        private readonly GeradorTolkienJWT _tokenJWT;

        public AutenticacaoService(IUsuarioRepository repository, GeradorTolkienJWT tokenJWT)
        {
            _repository = repository;
            _tokenJWT = tokenJWT;
        }
        private static bool VerificarSenha(string senhaDigtada, byte[] senhaHashBanco)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var hashDigitado = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senhaDigtada));
            return hashDigitado.SequenceEqual(senhaHashBanco);
        }
        public TokenDto Login(LoginDto loginDto)
        {
            Usuario usuario = _repository.ObterPorEmail(loginDto.Email);
            if (usuario == null)
            {
                throw new DomainException("Email ou senha inválidos!");
            }
            if (!VerificarSenha(loginDto.Senha, usuario.Senha))
            {
                throw new DomainException("Email ou senha inválidos!");
            }

            //gerar token
            var token = _tokenJWT.GerarToken(usuario);

            TokenDto novoToken = new TokenDto { Token = token };
            return novoToken;
        }

    }
}