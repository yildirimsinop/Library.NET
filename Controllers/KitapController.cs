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
    public class KitapController : Controller
    {
        private readonly IKitapRepository _kitapRepository;
        private readonly IKitapTuruRepository _kitapTuruRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public KitapController(IKitapRepository kitapRepository, IKitapTuruRepository kitapTuruRepository, IWebHostEnvironment webHostEnvironment)
        {
            _kitapRepository = kitapRepository;
            _kitapTuruRepository = kitapTuruRepository;
            _webHostEnvironment = webHostEnvironment;   
        }

        public IActionResult Index()
        {
            List<Kitap> objBooklist = _kitapRepository.GetAll(includeProps:"KitapTuru").ToList();
            return View(objBooklist);
        }

        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.Title,
                    Value = k.Id.ToString()
                }
                );
            ViewBag.KitapTuruList = KitapTuruList;

            if (id == null || id == 0)
            {
                return View();
            }

            else
            {
                Kitap? bookeventVt = _kitapRepository.Get(u => u.Id == id);

                if (bookeventVt == null)
                {
                    return NotFound();
                }

                return View(bookeventVt);
            }
            
        }

        [HttpPost]
        public IActionResult EkleGuncelle(Kitap kitap, IFormFile? file )
        {
           //  var errors = ModelState.Values.SelectMany(x => x.Errors);

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath = Path.Combine(wwwRootPath, @"img");


                if (file != null)
                {

               
                using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                kitap.ResimUrl = @"\img\" +file.FileName;
                }

                if (kitap.Id == 0)
                {
                    _kitapRepository.Ekle(kitap);
                    TempData["successful"] = "Yeni Kitap basariyla olusturuldu!";
                }
                else
                {
                    _kitapRepository.Guncelle(kitap);
                    TempData["successful"] = "Kitap guncelleme basarili!";
                }

                _kitapRepository.Kaydet();
                return RedirectToAction("Index", "Kitap");
            }
            return View();
        }
        /*
        public IActionResult Guncelle(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Kitap? bookeventVt = _kitapRepository.Get(u => u.Id == id);

            if (bookeventVt == null)
            {
                return NotFound();
            }

            return View(bookeventVt);
        }
       

        [HttpPost]
        public IActionResult Guncelle(Kitap bookitems)
        {
            if (ModelState.IsValid)
            {
                _kitapRepository.Guncelle(bookitems);
                _kitapRepository.Kaydet();
                TempData["successful"] = "Book creation updated";
                return RedirectToAction("Index");
            }
            return View();
        }
        */
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Kitap? bookeventVt = _kitapRepository.Get(u => u.Id == id);

            if (bookeventVt == null)
            {
                return NotFound();
            }

            return View(bookeventVt);
        }

        [HttpPost, ActionName("Sil")]
        public IActionResult SilPost(int? id)
        {
            Kitap? bookItem = _kitapRepository.Get(u => u.Id == id);
            if (bookItem == null)
            {
                return NotFound();
            }

            _kitapRepository.Sil(bookItem);
            _kitapRepository.Kaydet();
            TempData["successful"] = "Book deletion successful";
            return RedirectToAction("Index");
        }
    }
}
