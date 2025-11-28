using SellApiData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SellApiBusiness
{
    public class UsuarioBusiness
    {
        public void ValidarUsuario(UsuarioCadastro usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nome))
                throw new ArgumentException("O nome do usuário é obrigatório.");

            if (usuario.Senha.Length < 6)
                throw new ArgumentException("A senha deve ter pelo menos 6 caracteres.");
        }

        public List<UsuarioCadastro> FiltrarUsuariosAtivos(List<UsuarioCadastro> usuarios)
        {
            return usuarios.Where(u => u.Active).ToList();
        }
    }

    public class Class1
    {

    }
}
