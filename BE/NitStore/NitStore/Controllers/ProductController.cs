using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NitStore.Data;
using NitStore.Models.Domain;

namespace NitStore.Controllers
{
    public class ProductController : Controller
    {

        private readonly NitDbContext dbContext;

        public ProductController(NitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> ViewAllProduct()
        {
            
            return View();
        }

        public async Task<bool> AddProduct(Product product)
        {
            if (product == null)
            {
                return false;
            }
            else if (product.Name == null || product.Description == null)
            {
                return false;
            }
            else if (product.Name.Trim() == "" || product.Description.Trim() == "")
            {
                return false;
            }
            else
            {
                dbContext.Add(product);
                await dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
