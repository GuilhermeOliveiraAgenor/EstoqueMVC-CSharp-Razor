using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueMVC.Models
{
    public static class loginUsuario
    {
        static string emailUsuario;
        static string senhaUsuario;
        public static void Login(string email, string senha)
        {
            emailUsuario = email;
            senhaUsuario = senha;
        }

        public static string getEmail()
        {
            return emailUsuario;
        }


    }
}
