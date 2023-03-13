using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NitStore.Data;
using NitStore.Models.Domain;
using System.Web;

namespace NitStore.Controllers
{
    public class ImagesController : Controller
    {
        private readonly NitDbContext dbContext;

        public ImagesController(NitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: Images
        public async Task<IActionResult> Index()
        {
              return View(await dbContext.images.ToListAsync());
        }

        // GET: Images/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || dbContext.images == null)
            {
                return NotFound();
            }

            var image = await dbContext.images
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageData,Description")] Image image)
        {
            if (ModelState.IsValid)
            {
                AddImage(image);
                return RedirectToAction(nameof(Index));
            }
            return View(image);
        }

        public async Task<bool> AddImage(Image img)
        {
            if (img.ImageData == null)
            {
                return false;
            }
            else
            {
                dbContext.Add(img);
                await dbContext.SaveChangesAsync();
                return true;
            }
        }

        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || dbContext.images == null)
            {
                return NotFound();
            }

            var image = await dbContext.images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageData,Description")] Image image)
        {
            if (id != image.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Update(image);
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageExists(image.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(image);
        }

        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || dbContext.images == null)
            {
                return NotFound();
            }

            var image = await dbContext.images
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (dbContext.images == null)
            {
                return Problem("Entity set 'NitDbContext.images'  is null.");
            }
            var image = await dbContext.images.FindAsync(id);
            if (image != null)
            {
                dbContext.images.Remove(image);
            }
            
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
          return dbContext.images.Any(e => e.Id == id);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Import(HttpPostedFileBase[] files)
        //{
        //    if (files.Length < 0)
        //    {
        //        TempData["ErrorMessage"] = "Import Error. Image folder cannot empty!";
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {

        //        foreach (var file in files)
        //        {
        //            Image image = new Image();
        //            image = this.InsertImage(image, file, "Test");
        //        }

        //    }
        //    return RedirectToAction("Index");
        //}

        //private Image InsertImage(Image img, HttpPostedFileBase singleFile, string Name)
        //{

        //    string path = AppDomain.CurrentDomain.BaseDirectory +
        //        "images\\";
        //    //get image from file: [file:*.*]
        //    string filePat = @".*(?<file>\[file:.*\]).*";

        //    string imgPath = "" + Name;

        //    imgPath = path + "image";
        //    singleFile.SaveAs(imgPath);
        //    FileStream fs = new FileStream(imgPath, FileMode.Open);
        //    byte[] buf = new byte[(int)fs.Length];
        //    fs.Read(buf, 0, (int)fs.Length);
        //    img.ImageData = buf;
        //    fs.Close();
        //    System.IO.File.Delete(imgPath);

        //    return img;
        //}
    }
}
