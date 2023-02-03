using EstoqueMVC.DAL;
using EstoqueMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EstoqueMVC.Controllers
{
    public class UsuarioController : Controller
    {
        Usuario usuario = new Usuario();
        UsuarioDAL usuarioDAL = new UsuarioDAL();
        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logar(string txtEmail, string txtSenha)
        {
            DataTable dt = new DataTable();

            usuario.Email = txtEmail;//parametros
            usuario.Senha = txtSenha;

            dt = usuarioDAL.Login(usuario);//recebe o resultado

            if (dt.Rows.Count == 1)
            {
                return RedirectToAction("Index","Produto");
            }
            else
            {
                TempData["Message"] = "Usuário ou senha inválidos";
                return RedirectToAction("Login","Usuario");
            }

        }

    }
}
