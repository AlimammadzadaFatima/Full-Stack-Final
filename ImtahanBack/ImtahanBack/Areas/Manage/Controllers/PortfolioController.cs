using ImtahanBack.Helper;
using ImtahanBack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImtahanBack.Areas.Manage.Controllers
{[Area("manage")]
[Authorize(Roles ="SuperAdmin")]
    public class PortfolioController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;

        public PortfolioController(DataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Portfolios.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            Portfolio portfolio = _context.Portfolios.FirstOrDefault(x => x.Id == id);
            return View(portfolio);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Portfolio portfolio)
        {
            if (!ModelState.IsValid) return NotFound();
           
            if (portfolio.FileImage == null)
            {
                ModelState.AddModelError("FileImage", "Sekil mutleq daxil edilmelidir.");
                return View();
            }
            if (portfolio.FileImage.ContentType !="image/png" && portfolio.FileImage.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("FileImage", "Sekil formati yanlisdir! (Yalniz 'image/png' ve ya 'image/jpeg' ola biler) ");
                return View();
            }
            if (portfolio.FileImage.Length>2097152)
            {
                ModelState.AddModelError("FileImage", "Sekil olcusu maximum 2MB ola biler!");
                return View();
            }
            portfolio.Image = FileManager.Save(_env.WebRootPath, "uploads/portfolios", portfolio.FileImage);
            _context.Add(portfolio);
            _context.SaveChanges();
            return RedirectToAction("index", "portfolio");
        }
        public IActionResult Delete(int id)
        {
            Portfolio portfolio = _context.Portfolios.FirstOrDefault(x => x.Id == id);
            if (portfolio.Image == null) return NotFound();
            //FileManager.Delete(_env.WebRootPath, "uploads/portfolios", portfolio.Image);
            //_context.Remove(portfolio);
            portfolio.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("index", "portfolio");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Portfolio portfolio)
        {
            if (!ModelState.IsValid) return NotFound();
            Portfolio existPortfolio = _context.Portfolios.FirstOrDefault(x => x.Id == portfolio.Id);
           
            if (portfolio.FileImage != null)
            {
                if (portfolio.FileImage.ContentType != "image/png" && portfolio.FileImage.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("FileImage", "Sekil formati yanlisdir! (Yalniz 'image/png' ve ya 'image/jpeg' ola biler) ");
                    return View();
                }
                if (portfolio.FileImage.Length > 2097152)
                {
                    ModelState.AddModelError("FileImage", "Sekil olcusu maximum 2MB ola biler!");
                    return View();
                }
                FileManager.Delete(_env.WebRootPath, "uploads/portfolios", existPortfolio.Image);
               existPortfolio.Image= FileManager.Save(_env.WebRootPath, "uploads/portfolios", portfolio.FileImage);
                _context.SaveChanges();
                return RedirectToAction("index", "portfolio");
            }
            return RedirectToAction("index", "portfolio");
        }
        //Muellim oyretdiyiniz her sey ucun tesekkurler))
    }
}
