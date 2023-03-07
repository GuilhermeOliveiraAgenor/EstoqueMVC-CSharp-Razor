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
        public static void Login(string email)
        {
            emailUsuario = email;
        }

        public static string getEmail()
        {
            return emailUsuario;
        }


    }
}
