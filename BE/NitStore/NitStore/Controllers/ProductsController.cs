using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
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

        public async Task<IActionResult> ListProduct()
        {
            List<Product> productList = dbContext.products.ToList();
            List<ProductShowDTO> productShowList = new List<ProductShowDTO>();
            List<Category> categoryList = dbContext.categories.ToList();

            ViewBag.CategoryList = categoryList;

            foreach (Product item in productList)
            {
                ProductImage productImage = dbContext.productsImage.Where(x => x.ProductId == item.Id).First();
                Image image = dbContext.images.Where(x => x.Id == productImage.ImageId).First();
                ProductShowDTO productShowDTO = new ProductShowDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Status = item.Status,
                    Quantity = item.Quantity,
                    CategoryId = item.Category,
                    CategoryName = categoryList.Where(x => x.Id == item.Category).First().Name,
                    Price = item.Price,
                    imageBit = image.ImageData
                };
                productShowList.Add(productShowDTO);
            }
            ViewBag.ListProduct = productShowList;
            return View();
        }

        public async Task<IActionResult> ViewAllProduct()
        {
            List<Product> productList = dbContext.products.ToList();
            List<ProductShowDTO> productShowList = new List<ProductShowDTO>();
            List<Category> categoryList = dbContext.categories.ToList();
            
            foreach(Product item in productList)
            {
                ProductImage productImage = dbContext.productsImage.Where(x => x.ProductId == item.Id).First();
                Image image = dbContext.images.Where(x => x.Id== productImage.ImageId).First();
                ProductShowDTO productShowDTO = new ProductShowDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Status = item.Status,
                    Quantity = item.Quantity,
                    CategoryId = item.Category,
                    CategoryName = categoryList.Where(x => x.Id == item.Category).First().Name,
                    Price = item.Price,
                    imageBit = image.ImageData
                };
                productShowList.Add(productShowDTO);
            }
            ViewBag.ListProduct = productShowList;
            return View();
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
            
            List<Category> categoryList = dbContext.categories.ToList();
            ProductAddDTO dtos = new ProductAddDTO();
            dtos.CategoryList = new SelectList(categoryList, "Id", "Name");
            dto.CategoryList = new SelectList(categoryList, "Id", "Name");
            //if (ModelState.IsValid)
            //{
                Product product = new Product()
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Status = dto.Status,
                    Quantity = dto.Quantity,
                    Category = dto.CategoryId,
                    Price = dto.Price
                };
                dbContext.products.Add(product);
                dbContext.SaveChanges();
                if (dto.Image != null)
                {
                int index = 1;
                    foreach (IFormFile file in dto.Image)
                    {
                        
                        Image image = new Image();
                        byte[] bytes = ConvertToBytes(file);
                        image.ImageData = bytes;
                        image.Description = "Product_" + dto.Name + "_" + index;
                        index++;
                        dbContext.images.Add(image);
                        dbContext.SaveChanges();

                        ProductImage productImage = new ProductImage()
                        {
                            ProductId = product.Id,
                            ImageId = image.Id
                        };

                        dbContext.productsImage.Add(productImage);
                        dbContext.SaveChanges();
                    } 
                }
            //}
            
            return View(dtos);
        }
        private byte[] ConvertToBytes(IFormFile file)
        {
            Stream stream = file.OpenReadStream();
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
              return View(await dbContext.products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> ProductDetail(int? id)
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
            List<ProductImage> productImage = dbContext.productsImage.Where(x => x.ProductId == product.Id).ToList();
            List<Image> imageList = new List<Image>();
            foreach(ProductImage item in productImage)
            {
                Image image = dbContext.images.Where(x => x.Id == item.ImageId).First();
                imageList.Add(image);
            }
            ViewBag.Product = product;
            ViewBag.Images = imageList;
            return View();
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
