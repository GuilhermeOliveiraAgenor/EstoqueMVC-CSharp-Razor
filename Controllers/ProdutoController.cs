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
        static int idProduto;

        public IActionResult Index()
        {
            List<Produto> produtos = new List<Produto>();
            produtos = produtoDAL.listarProduto();

            return View(produtos);
        }
        public IActionResult Pesquisa(string txtPesquisa)
        {
            List<Produto> produtos = 

            return View ("Index", pesqCodigo);

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
            idProduto = id;//passa o id selecionado para a variavel

            var produtos = produtoDAL.pesqProdutoCodigo(idProduto).FirstOrDefault();
            
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

            if (result == true)
            {
                TempData["Message"] = "Produto alterado com sucesso";
                idProduto = 0;
            }
            else
            {
                TempData["Message"] = "Erro ao alterar produto";
            }

            return RedirectToAction("Index", "Produto");

        }

        public IActionResult Excluir(int id)
        {
            idProduto = id;//passa o id selecionado para a variavel

            var produtos = produtoDAL.pesqProdutoCodigo(idProduto).FirstOrDefault();

            return View(produtos);
        }

        [HttpPost]
        public IActionResult Excluir()
        {
            bool result;

            result = produtoDAL.excluirProduto(idProduto);

            if (result == true)
            {
                TempData["Message"] = "Produto excluído com sucesso";
                idProduto = 0;
            }
            else
            {
                TempData["Message"] = "Erro ao excluir produto";
            }

            return RedirectToAction("Index","Produto");

        }



    }
}
