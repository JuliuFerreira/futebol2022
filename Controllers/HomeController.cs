using futebol2022.Data;
using futebol2022.Models;
using futebol2022.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace futebol2022.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            this._context = context;
        }
        
        public ActionResult Index()
        {
            var listaJogador = _context.TB_Jogadores.ToList();

            return View(listaJogador);
        }

        // GET: HomeController/Details/5
        public ActionResult DetalhesJogador(int id)
        {
            return View();
        }

        // GET: HomeController/CriarJogador
        [HttpGet]
        public ActionResult CriarJogador()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarJogador(JogadorViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Ocorreu um error ao salvar os dados do jogador.";
                    return View(model);
                }



                return View();
            }

            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult EditarJogador(int id)
        {
            var buscarJogador = _context.TB_Jogadores.Find(id);

            return View(buscarJogador);
        }

        // POST: HomeController/EditarJogador/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarJogador(int id, Jogador model)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult DeletarJogador(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarJogador(int id, Jogador model)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
