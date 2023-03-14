using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NitStore.Data;
using NitStore.Models.Domain;

namespace NitStore.Controllers
{
    public class FeedbacksController : Controller
    {
        private readonly NitDbContext dbContext;

        public FeedbacksController(NitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: Feedbacks
        public async Task<IActionResult> Index()
        {
              return View(await dbContext.feedbacks.ToListAsync());
        }

        // GET: Feedbacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || dbContext.feedbacks == null)
            {
                return NotFound();
            }

            var feedback = await dbContext.feedbacks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // GET: Feedbacks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public async Task<IActionResult> Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                AddFeedback(feedback);
                return RedirectToAction(nameof(Index));
            }
            return View(feedback);
        }

        public async Task<bool> AddFeedback(Feedback feedback)
        {
            dbContext.Add(feedback);
            await dbContext.SaveChangesAsync();
            return true;
        }


        // GET: Feedbacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || dbContext.feedbacks == null)
            {
                return NotFound();
            }

            var feedback = await dbContext.feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public async Task<IActionResult> Edit(int id, Feedback feedback)
        {
            if (id != feedback.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Update(feedback);
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackExists(feedback.Id))
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
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || dbContext.feedbacks == null)
            {
                return NotFound();
            }

            var feedback = await dbContext.feedbacks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (dbContext.feedbacks == null)
            {
                return Problem("Entity set 'NitDbContext.feedbacks'  is null.");
            }
            var feedback = await dbContext.feedbacks.FindAsync(id);
            if (feedback != null)
            {
                dbContext.feedbacks.Remove(feedback);
            }
            
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedbackExists(int id)
        {
          return dbContext.feedbacks.Any(e => e.Id == id);
        }
    }
}
