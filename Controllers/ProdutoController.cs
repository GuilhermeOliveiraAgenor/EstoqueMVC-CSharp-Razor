using EstoqueMVC.DAL;
using EstoqueMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace EstoqueMVC.Controllers
{
    public class ProdutoController : Controller
    {
        ProdutoDAL produtoDAL = new ProdutoDAL();
        Produto produto = new Produto();

        public IActionResult Index()
        {
            List<Produto> produtos = new List<Produto>();
            produtos = produtoDAL.listarProduto();

            return View(produtos);
        }
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Inserir()
        {
            bool result = false;

            result = produtoDAL.inserirProduto(produto);
            
            return RedirectToAction("Index","Produto");

        }


    }
}
