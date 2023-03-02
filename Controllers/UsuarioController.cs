﻿using EstoqueMVC.DAL;
using EstoqueMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EstoqueMVC.Controllers
{
    public class UsuarioController : Controller
    {
        Usuario usuario = new Usuario();
        UsuarioDAL usuarioDAL = new UsuarioDAL();

        private readonly IHttpContextAccessor session;
        public UsuarioController(IHttpContextAccessor login)
        {
           session = login;
        }

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
                HttpContext.Session.SetString("Sessao","Ok");
                loginUsuario.Login(usuario.Email, usuario.Senha);//faz a autenticação da classe
                return RedirectToAction("Index","Produto");
            }
            else
            {
                TempData["Message"] = "Usuário ou senha inválidos";
                return RedirectToAction("Login","Usuario");
            }
        }

        public IActionResult Logout()
        {

            HttpContext.Session.Remove("Sessao");
            return RedirectToAction("Login","Usuario");

        }
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Inserir(string txtEmail, string txtNome, string txtSenha)
        {
            bool result = false;

            usuario.Email = txtEmail;
            usuario.Nome = txtNome;
            usuario.Senha = txtSenha;

            result = usuarioDAL.inserirUsuario(usuario);

            if (result == true)
            {
                TempData["Message"] = "Usuario cadastrado com sucesso";
            }
            else
            {
                TempData["Message"] = "Erro ao cadastrar usuario";
            }

            return RedirectToAction("Cadastro","Usuario");
        }
        public IActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AlterarSenha(string txtSenha)
        {
            string email = loginUsuario.getEmail();
            int idUsuario;
            DataTable dt = usuarioDAL.selecionarUsuario(email);

            if(dt.Rows.Count == 1)
            {
                foreach (DataRow row in dt.Rows)
                {
                    idUsuario = Convert.ToInt32(row["idUsuario"].ToString());
                    usuario.Senha = txtSenha;
                    usuario.idUsuario = idUsuario;
                }
            }

        }

    }
}
