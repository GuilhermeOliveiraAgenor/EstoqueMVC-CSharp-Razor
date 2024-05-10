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

            if (HttpContext.Session.GetString("Sessao")  != null)
            {
                produtos = produtoDAL.listarProduto();
                return View(produtos);
            }
            
            return RedirectToAction("Login","Usuario");
            

        }
        public IActionResult Pesquisa(string txtPesquisa,string btnRadio)
        {
            List<Produto> produtos = new List<Produto>();

            if(HttpContext.Session.GetString("Sessao") != null)
            {
                if(btnRadio == "codigo")
                {
                    produtos = produtoDAL.pesqProdutoCodigo(Convert.ToInt32(txtPesquisa));
                }
                if (btnRadio == "nome")
                {
                    produtos = produtoDAL.pesqProdutoNome(txtPesquisa);
                }
                if (btnRadio == "preco")
                {
                    produtos = produtoDAL.pesqProdutoPreco(Convert.ToDecimal(txtPesquisa));

                }
                if (produtos.Count < 1)
                {
                    TempData["Message"] = "Erro ao pesquisar produto";
                }

                return View("Index",produtos);

            }
                
            return RedirectToAction("Login","Usuario");

        }
        public IActionResult Cadastro()
        {

            if (HttpContext.Session.GetString("Sessao") != null)
            {
                return View();
            }

            return RedirectToAction("Usuario", "Login");

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Inserir(string txtNome, string txtPreco, string txtQuantidade, string txtObservacoes)
        {
            bool result = false;

            if (HttpContext.Session.GetString("Sessao") != null)
            {
                produto.Nome = txtNome;
                produto.Preco = Convert.ToDecimal(txtPreco);
                produto.Quantidade = Convert.ToInt32(txtQuantidade);
                produto.Observacoes = txtObservacoes;

                result = produtoDAL.inserirProduto(produto);

                if (result == true)
                {
                    TempData["Message"] = "Produto cadastrado com sucesso";
                }
                else
                {
                    TempData["Message"] = "Erro ao cadastrar produtos";
                }

                return RedirectToAction("Menu", "Produto");


            }
            else
            {
                return RedirectToAction("Usuario", "Login");
            }
            
        }
        public IActionResult Alterar(int id)
        {
            if(HttpContext.Session.GetString("Sessao") != null)
            {
                idProduto = id;

                var produto = produtoDAL.pesqProdutoCodigo(id).FirstOrDefault();

                return View(produto);

            }
                
            return RedirectToAction("Login","Usuario");
            
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
                
            return RedirectToAction("Login","Usuario");    

        }

        public IActionResult Excluir(int id)
        {
            if (HttpContext.Session.GetString("Sessao") != null)
            {
                idProduto = id;//passa o id selecionado para a variavel

                var produtos = produtoDAL.pesqProdutoCodigo(idProduto).FirstOrDefault();

                return View(produtos);
            }
                
            return RedirectToAction("Login", "Usuario");
            
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
               
            return RedirectToAction("Login","Usuario");
            
        }

    }
}
