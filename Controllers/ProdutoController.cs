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

            if (HttpContext.Session.GetString("Sessao") != null)
            {
                produtos = produtoDAL.listarProduto();
                return View(produtos);
            }
            else
            {
                return RedirectToAction("Login","Usuario");
            }
        }
        public IActionResult Pesquisa(string txtPesquisa)
        {
            List<Produto> produtos = produtoDAL.pesqProdutoCodigo(Convert.ToInt32(txtPesquisa));
            
            if (HttpContext.Session.GetString("Sessao") != null)
            {
                if (produtos.Count >= 1)
                {
                    TempData["Message"] = "Produto encontrado";
                }
                else
                {
                    TempData["Message"] = "Erro ao encontrar produto";
                }
                return View("Index", produtos);
            }
            else
            {
                return RedirectToAction("Login","Usuario");
            }
        }
        public IActionResult Cadastro()
        {
            if (HttpContext.Session.GetString("Sessao") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login","Usuario");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Inserir(string txtNome, string txtPreco, string txtQuantidade, string txtObservacoes)
        {
            bool result = false;

            if (HttpContext.Session.GetString("Sessao") != null)
            {
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

                return RedirectToAction("Cadastro", "Produto");

            }
            else
            {
                return RedirectToAction("Login","Usuario");
            }

        }
        public IActionResult Alterar(int id)
        {
            if (HttpContext.Session.GetString("Sessao") != null)
            {
                idProduto = id;//passa o id selecionado para a variavel

                var produtos = produtoDAL.pesqProdutoCodigo(idProduto).FirstOrDefault();

                return View(produtos);
            }
            else
            {
                return RedirectToAction("Login","Usuario");
            }

        }

        [HttpPost]
        public IActionResult Alterar(string txtCodigo, string txtNome, string txtPreco, string txtQuantidade, string txtObservacoes)
        {
            bool result;

            if (HttpContext.Session.GetString("Sessao") != null)
            {
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
            else
            {
                return RedirectToAction("Login","Usuario");    
            }

        }

        public IActionResult Excluir(int id)
        {
            if (HttpContext.Session.GetString("Sessao") != null)
            {
                idProduto = id;//passa o id selecionado para a variavel

                var produtos = produtoDAL.pesqProdutoCodigo(idProduto).FirstOrDefault();

                return View(produtos);
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }

        }

        [HttpPost]
        public IActionResult Excluir()
        {
            bool result;

            if (HttpContext.Session.GetString("Sessao") != null)
            {
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

                return RedirectToAction("Index", "Produto");
            }
            else
            {
                return RedirectToAction("Login","Usuario");
            }

        }



    }
}
