using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using NitStore.Data;
using NitStore.Models.Domain;
using NitStore.Models.DTO;

namespace NitStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly NitDbContext dbContext;

        public ProductsController(NitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> AddProduct()
        {
            List<Category> categoryList = dbContext.categories.ToList();
            ProductAddDTO dto = new ProductAddDTO();
            dto.CategoryList = new SelectList(categoryList, "Id", "Name");
            return View(dto);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductAddDTO dto)
        {
            if (dto.Image != null)
            {
                //var uniqueFileName = GetUniqueFileName(dto.Image.FileName);
                //model.MyImage.CopyTo(new FileStream(filePath, FileMode.Create));

                //to do : Save uniqueFileName  to your db table   
            }
            return View();
        }
        //private string GetUniqueFileName(string fileName)
        //{
        //    fileName = Path.GetFileName(fileName);
        //    return Path.GetFileNameWithoutExtension(fileName)
        //              + "_"
        //              + Guid.NewGuid().ToString().Substring(0, 4)
        //              + Path.GetExtension(fileName);
        //}

        // GET: Products
        public async Task<IActionResult> Index()
        {
              return View(await dbContext.products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || dbContext.products == null)
            {
                return NotFound();
            }

            var product = await dbContext.products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Status,Quantity,Category,Total")] Product product)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(product);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || dbContext.products == null)
            {
                return NotFound();
            }

            var product = await dbContext.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Status,Quantity,Category,Total")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Update(product);
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || dbContext.products == null)
            {
                return NotFound();
            }

            var product = await dbContext.products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (dbContext.products == null)
            {
                return Problem("Entity set 'NitDbContext.products'  is null.");
            }
            var product = await dbContext.products.FindAsync(id);
            if (product != null)
            {
                dbContext.products.Remove(product);
            }
            
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return dbContext.products.Any(e => e.Id == id);
        }
    }
}
