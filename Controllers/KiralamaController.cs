using Microsoft.AspNetCore.Mvc;
using WebApplication1.Utility;
using WebApplication1.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;


namespace WebApplication1.Controllers
{
    public class KiralamaController : Controller
    {

        private readonly IKiralamaRepository _kiralamaRepository;
        private readonly IKitapRepository _kitapRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public KiralamaController(IKiralamaRepository kiralamaRepository, IKitapRepository kitapRepository, IWebHostEnvironment webHostEnvironment)
        {
            _kiralamaRepository = kiralamaRepository;
            _kitapRepository = kitapRepository;
            _webHostEnvironment = webHostEnvironment;   
        }

        public IActionResult Index()
        {
            List<Kiralama> objKiralamalist = _kiralamaRepository.GetAll(includeProps:"Kitap").ToList();
            return View(objKiralamalist);
        }

        // GET
        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.KitapAdi,
                    Value = k.Id.ToString()
                }
                );
            ViewBag.KitapList = KitapList;

            if (id == null || id == 0)
            {

                // EKLE
                return View();
            }

            else
            {
                // GUNCELLEME
                Kiralama? kiralamaVt = _kiralamaRepository.Get(u => u.Id == id);

                if (kiralamaVt == null)
                {
                    return NotFound();
                }

                return View(kiralamaVt);
            }
            
        }

        [HttpPost]
        public IActionResult EkleGuncelle(Kiralama kiralama)
        {
           
            if (ModelState.IsValid)
            {
                
                if (kiralama.Id == 0)
                {
                    _kiralamaRepository.Ekle(kiralama);
                    TempData["basarili"] = "Yeni Kiralama kaydi basariyla olusturuldu!";
                }
                else
                {
                    _kiralamaRepository.Guncelle(kiralama);
                    TempData["basarili"] = "Kiralama kayit guncelleme basarili!";
                }

                _kiralamaRepository.Kaydet();
                return RedirectToAction("Index", "Kiralama");
            }
            return View();
        }
        
        // GET ACTION
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Kiralama? kiralamaVt = _kiralamaRepository.Get(u => u.Id == id);

            if (kiralamaVt == null)
            {
                return NotFound();
            }

            return View(kiralamaVt);
        }

        [HttpPost, ActionName("Sil")]
        public IActionResult SilPost(int? id)
        {
            Kiralama? kiralama = _kiralamaRepository.Get(u => u.Id == id);
            if (kiralama == null)
            {
                return NotFound();
            }

            _kiralamaRepository.Sil(kiralama);
            _kiralamaRepository.Kaydet();
            TempData["basarili"] = "Kayit Silme islemi basarili";
            return RedirectToAction("Index", "Kiralama");
        }
    }
}
