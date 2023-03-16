using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NitStore.Data;
using NitStore.Models.Domain;
using NitStore.Models.DTO;

namespace NitStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly NitDbContext dbContext;

        public OrdersController(NitDbContext context)
        {
            this.dbContext = dbContext;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
              return View(await dbContext.orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || dbContext.orders == null)
            {
                return NotFound();
            }

            var order = await dbContext.orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(order);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || dbContext.orders == null)
            {
                return NotFound();
            }

            var order = await dbContext.orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Update(order);
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || dbContext.orders == null)
            {
                return NotFound();
            }

            var order = await dbContext.orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (dbContext.orders == null)
            {
                return Problem("Entity set 'NitDbContext.orders'  is null.");
            }
            var order = await dbContext.orders.FindAsync(id);
            if (order != null)
            {
                dbContext.orders.Remove(order);
            }
            
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return dbContext.orders.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(PaymentMethodDTO dto)
        {
            int userId = -1;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            }
            //get customer order
            Order order = dbContext.orders.Where(x => x.CustomerId == userId && x.Status == 0).FirstOrDefault();
            List<OrderDetail> detail = new List<OrderDetail>();
            if (order != null)
            {
                detail = dbContext.ordersDetail.Where(x => x.OrderId == order.Id).ToList();
            }
            if(detail.Count > 0)
            {
                // process cart to order
                if (dto.PayNow == true)
                {
                    //paypal
                }
                else
                {
                    // put out quantity of a product
                    decimal totalPrice = 0; 
                    foreach(OrderDetail item in detail)
                    {
                        Product product = dbContext.products.Where(x => x.Id== item.ProductId).First();
                        product.Quantity = product.Quantity - item.Quantity;
                        totalPrice = totalPrice + product.Price;
                        dbContext.SaveChanges();
                    }
                    // put cart into order
                    order.Status = 1;
                    order.UpdatedDate= DateTime.Now;
                    order.Total = totalPrice;
                    dbContext.SaveChanges();
                    return RedirectToAction("LandingPage","Home", new { area = "" });
                }
            }
            
            return View();
        }
    }
}
