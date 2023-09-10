using Microsoft.AspNetCore.Mvc;
using WebApplication1.Utility;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;


namespace WebApplication1.Controllers
{
    public class BookEventController : Controller
    {
        private readonly IKitapTuruRepository _kitapTuruRepository;

        public BookEventController(IKitapTuruRepository Context)
        {
            _kitapTuruRepository = Context;
        }

        public IActionResult Index()
        {
            List<KitapTuru> objBooklist = _kitapTuruRepository.GetAll().ToList();  // DbSet adı ile aynı olmalı
            return View(objBooklist);
        }

        public IActionResult Details() 
        {
        return View();
        }

        [HttpPost]
        public IActionResult Details(KitapTuru bookitems)
        {
            if (ModelState.IsValid)
            {
                _kitapTuruRepository.Ekle(bookitems);
                _kitapTuruRepository.Save(); // SaveChanges () yapmazsaniz bilgiler veri tabanina eklenmez.
                TempData["successful"] = "New Book Items has been created";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            KitapTuru? bookeventVt = _kitapTuruRepository.Get(u=>u.Id==id);


            if (bookeventVt == null) 
            {
                return NotFound();
            }

            return View(bookeventVt);
        }

        [HttpPost]
        public IActionResult Update(KitapTuru bookitems)
        {
            if (ModelState.IsValid)
            {
                _kitapTuruRepository.Update(bookitems);
                _kitapTuruRepository.Save(); // SaveChanges () yapmazsaniz bilgiler veri tabanina eklenmez.
                TempData["successful"] = "Book creation updated";
                return RedirectToAction("Index");
            }
            return View();
        }

        private class BookEvent
        {
        }

        // GET ACTION
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            KitapTuru? bookeventVt = _kitapTuruRepository.Get(u => u.Id == id);


            if (bookeventVt == null)
            {
                return NotFound();
            }

            return View(bookeventVt);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            KitapTuru? bookItem = _kitapTuruRepository.Get(u => u.Id == id);
            if (bookItem == null)
            {
                return NotFound();
            }

            _kitapTuruRepository.Sil(bookItem);
            _kitapTuruRepository.Save();
            TempData["successful"] = "Book deletion successful";
            return RedirectToAction("Index");

        }
    }
}
