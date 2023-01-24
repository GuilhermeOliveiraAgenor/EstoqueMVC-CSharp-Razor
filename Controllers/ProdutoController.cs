using EstoqueMVC.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueMVC.Controllers
{
    public class ProdutoController : Controller
    {
        ProdutoDAL produtoDAL = new ProdutoDAL();

        public IActionResult Index()
        {
            return View();
        }
    }
}
