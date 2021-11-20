using futebol2022.Data;
using futebol2022.Models;
using futebol2022.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
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
            Mensagens();

            var listaJogador = _context.TB_Jogadores
                .Include(p => p.Storage)
                .ToList();

            return View(listaJogador);
        }

        private void Mensagens()
        {
            if (TempData.ContainsKey("ErrorMsg"))
                ViewData["MsgError"] = TempData["ErrorMsg"].ToString();

            if (TempData.ContainsKey("SuccessMsg"))
                ViewData["MsgSuccess"] = TempData["SuccessMsg"].ToString();
        }

    }
}
