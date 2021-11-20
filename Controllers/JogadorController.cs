using futebol2022.Data;
using futebol2022.Models;
using futebol2022.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace futebol2022.Controllers
{
    public class JogadorController : Controller
    {
        private readonly ApplicationDbContext _context;
        public JogadorController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        // GET: HomeController/Details/5
        public ActionResult DetalhesJogador(int id)
        {
            var jogador = _context.TB_Jogadores.Find(id);
            return View(jogador);
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
                long tamanhoArquivos = 0;

                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Ocorreu um error ao salvar os dados do jogador.";
                    return View(model);
                }

                if (model.FotoJogador != null)
                {
                    tamanhoArquivos = model.FotoJogador.Length;
                }

                if (tamanhoArquivos > 50000000)
                {
                    ViewBag.Error = "O tamanho da imagem excede 50 mb";
                    return View(model);
                }

                var storage = new Storage();

                if (model.FotoJogador != null)
                {
                    var caminhoArquivo = Path.GetTempFileName();

                    string pasta = @"C:\Temp\Futeba2022";
                    string nomeArquivo = $"{model.FotoJogador.FileName}";
                    string caminhoDestinoArquivo = $@"{pasta}";
                    string caminhoDestinoArquivoOriginal = $@"{caminhoDestinoArquivo}\{nomeArquivo}";

                    if (!Directory.Exists(pasta))
                    {
                        Directory.CreateDirectory(pasta);
                    }

                    using (var stream = new FileStream(caminhoDestinoArquivoOriginal, FileMode.Create))
                    {
                        model.FotoJogador.CopyToAsync(stream);
                    }

                    storage.Arquivo = Util.ConvertArquivoToBytes.ToByteArray(model.FotoJogador);
                    storage.ContentType = model.FotoJogador.ContentType;
                    storage.Diretorio = caminhoDestinoArquivo;
                    storage.Extensao = Path.GetExtension(model.FotoJogador.FileName);
                    storage.NomeArquivo = nomeArquivo;
                    storage.Tamanho = model.FotoJogador.Length.ToString();

                    _context.TB_Storage.Add(storage);
                    _context.SaveChanges();
                }
                else
                {
                    _context.TB_Storage.Add(storage);
                    _context.SaveChanges();
                }


                var jogador = new Jogador()

                {
                    Apelido = model.Apelido,
                    DataNascimento = model.DataNascimento,
                    NomeJogador = model.NomeJogador,
                };

                _context.TB_Jogadores.Add(jogador);
                _context.SaveChanges();

                var jogadorStorage = new JogadorStorage()
                { 
                    JogadorId = jogador.Id,
                    StorageId = storage.StorageId
                };

                _context.TB_JogadorStorage.Add(jogadorStorage);
                _context.SaveChanges();

                ts.Commit();

                TempData["SuccessMsg"] = "Jogador criado com sucesso!";

                return RedirectToAction("Index", "Home");
            }

            catch (Exception e)
            {
                ts.Rollback();
                ViewBag.Error = "Ocorreu um erro ao criar o Jogador";
                return View(model);
            }
        }
        //GET: HomeController/Edit/5
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
                //var jogador = _context.TB_Jogadores.FirstOrDefault(a => a.Id == id);
                //model.StorageId = jogador.StorageId;
                _context.Update(model);
                _context.SaveChanges();
                TempData["SuccessMsg"] = "Jogador alterado com sucesso!";
                return RedirectToAction("Index","Home");
            }
            catch(Exception e)
            {
                ViewBag.Error= $"Não foi possivel alterar o jogador! motivo: {e.Message}"; // "$" = interpolacion
                return View(model);
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
