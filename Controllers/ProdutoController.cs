using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Imagens.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly Imagens.Data.AppContext _appContext;

        public ProdutoController(Imagens.Data.AppContext appContext)
        {
            _appContext = appContext;
        }

        // GET: ProdutoController
        public async Task<IActionResult> Index()
        {
            var allProducts = await _appContext.Produtos.ToListAsync();
            return View(allProducts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
