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
        [ValidateAntiForgeryToken()]
        public IActionResult Inserir(string txtNome, string txtPreco, string txtQuantidade, string txtObservacoes)
        {
            bool result = false;

            produto.Nome = txtNome;//parametro
            produto.Preco = Convert.ToDecimal(txtPreco);
            produto.Quantidade = Convert.ToInt32(txtQuantidade);
            produto.Observacoes = txtObservacoes;

            result = produtoDAL.inserirProduto(produto);//recebe o resultado

            if (result == true)
            {
                TempData["message"] = "Produto cadastrado com sucesso";
            }
            else
            {
                TempData["message"] = "Erro ao cadastrar produto";
            }

            return RedirectToAction("Cadastro","Produto");

        }
        public IActionResult Alterar(int id)
        {
            var produtos = produtoDAL.pesqProdutoCodigo(id).FirstOrDefault();
            
            return View(produtos);
        }

        [HttpPost]
        public IActionResult Alterar(string txtCodigo, string txtNome, string txtPreco, string txtQuantidade, string txtObservacoes)
        {
            bool result;

            produto.idProduto = Convert.ToInt32(txtCodigo);
            produto.Nome = txtNome;
            produto.Preco = Convert.ToDecimal(txtPreco);
            produto.Quantidade = Convert.ToInt32(txtQuantidade);
            produto.Observacoes = txtObservacoes;

            result = produtoDAL.alterarProduto(produto);

            return RedirectToAction("Index", "Produto");

        }



    }
}
