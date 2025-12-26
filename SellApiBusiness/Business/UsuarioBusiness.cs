using SellApiBusiness.Model;
using SellApiBusiness.Repository;
using SellApiBusiness.InfraExtruture.Context;

namespace SellApiBusiness.Business
{
    public class UsuarioBusiness
    {
        private readonly UsuarioData _usuarioRepository;

        public UsuarioBusiness()
        {
            PostgresConnection connection = new PostgresConnection("Host=localhost;Port=5433;Database=teste;Username=postgres;Password=postgres");
            _usuarioRepository = new UsuarioData(connection);
        }

        // public Usuario ObterUsuarioPorId(int id)
        // {
        //     return _usuarioRepository.ObterPorId(id);
        // }

        // public IEnumerable<Usuario> ListarUsuarios()
        // {
        //     return _usuarioRepository.ListarTodos();
        // }

        public void AdicionarUsuario(Usuario usuario)
        {
            _usuarioRepository.InsertUsuario(usuario);
        }

        // public void AtualizarUsuario(Usuario usuario)
        // {
        //     _usuarioRepository.Atualizar(usuario);
        // }

        // public void RemoverUsuario(int id)
        // {
        //     _usuarioRepository.Remover(id);
        // }
    }
}