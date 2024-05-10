using EstoqueMVC.Models;
using System.Data;

namespace EstoqueMVC.Interface
{
    public interface IUsuarioDAL
    {
        DataTable selecionarUsuario(string email);
        DataTable Login(Usuario usuario);
        bool inserirUsuario(Usuario usuario);
        bool alterarSenha(Usuario usuario);
    }
}
