using Imagens.Models;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto, IList<IFormFile> Img)
        {
            // Verificar Tamanho da Imagem
            IFormFile uploadedImagem = Img.FirstOrDefault();
            MemoryStream ms = new MemoryStream();
            if (Img.Count > 0)
            {
                uploadedImagem.OpenReadStream().CopyTo(ms);
                produto.Foto = ms.ToArray();
            }

            _appContext.Produtos.Add(produto);
            await _appContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var produto = await _appContext.Produtos.FindAsync(id);
            if (produto == null)
            {
                return BadRequest();
            }
            return View(produto);
        }
    }
}
