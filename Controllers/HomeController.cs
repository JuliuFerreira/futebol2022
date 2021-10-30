using futebol2022.Data;
using futebol2022.Models;
using futebol2022.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

            var listaJogador = _context.TB_Jogadores.ToList();

            return View(listaJogador);
        }

        private void Mensagens()
        {
            if (TempData.ContainsKey("ErrorMsg"))
                ViewData["MsgError"] = TempData["ErrorMsg"].ToString();

            if (TempData.ContainsKey("SuccessMsg"))
                ViewData["MsgSuccess"] = TempData["SuccessMsg"].ToString();
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
            using var ts = _context.Database.BeginTransaction();
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Ocorreu um error ao salvar os dados do jogador.";
                    return View(model);
                }

                long tamanhoArquivos = model.FotoJogador.Length;

                if (tamanhoArquivos > 50000000)
                {
                    ViewBag.Error = "O tamanho da imagem excede 50 mb";
                    return View(model);
                }

                var caminhoArquivo = Path.GetTempFileName();

                string pasta = @"C:\Temp";
                string nomeArquivo = $"{model.FotoJogador.FileName}";
                string caminhoDestinoArquivo = $@"{pasta}";
                string caminhoDestinoArquivoOriginal = $@"{caminhoDestinoArquivo}\{nomeArquivo}";

                using (var stream = new FileStream(caminhoDestinoArquivoOriginal, FileMode.Create))
                {
                    model.FotoJogador.CopyToAsync(stream);
                }

                var storege = new Storage()
                {
                    Arquivo = Util.ConvertArquivoToBytes.ToByteArray(model.FotoJogador),
                    ContentType = model.FotoJogador.ContentType,
                    Diretorio = caminhoDestinoArquivo,
                    Extensao = Path.GetExtension(model.FotoJogador.FileName),
                    NomeArquivo = nomeArquivo,
                    Tamanho = model.FotoJogador.Length.ToString(),
                    
                };

                _context.TB_Storage.Add(storege);
                _context.SaveChanges();

                var jogador = new Jogador()

                {
                    Apelido = model.Apelido,
                    DataNascimento = model.DataNascimento,
                    NomeJogador = model.NomeJogador,
                    StorageId = storege.StorageId,
                };

                _context.TB_Jogadores.Add(jogador);
                _context.SaveChanges();

                ts.Commit();

                TempData["SuccessMsg"] = "Jogador criado com sucesso!";

                return RedirectToAction("Index");
            }

            catch (Exception e)
            {
                ts.Rollback();
                ViewBag.Error = "Ocorreu um erro ao criar o Jogador";
                return View(model);
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
