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
    public class CampaignsController : Controller
    {
        private readonly NitDbContext dbContext;

        public CampaignsController(NitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: Campaigns
        public async Task<IActionResult> Index()
        {
              return View(await dbContext.campaigns.ToListAsync());
        }

        // GET: Campaigns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || dbContext.campaigns == null)
            {
                return NotFound();
            }

            var campaign = await dbContext.campaigns
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // GET: Campaigns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Campaigns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Status,StartDate,EndDate")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(campaign);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(campaign);
        }

        public async Task<bool> AddCampaign(Campaign campaign)
        {
            if (campaign.Name == null || campaign.Name.Trim() == "")
            {
                return false;
            }
            else
            {
                dbContext.Add(campaign);
                await dbContext.SaveChangesAsync();
                return true;
            }
        }

        // GET: Campaigns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || dbContext.campaigns == null)
            {
                return NotFound();
            }

            var campaign = await dbContext.campaigns.FindAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }
            return View(campaign);
        }

        // POST: Campaigns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Status,StartDate,EndDate")] Campaign campaign)
        {
            if (id != campaign.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Update(campaign);
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampaignExists(campaign.Id))
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
            return View(campaign);
        }

        // GET: Campaigns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || dbContext.campaigns == null)
            {
                return NotFound();
            }

            var campaign = await dbContext.campaigns
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // POST: Campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (dbContext.campaigns == null)
            {
                return Problem("Entity set 'NitDbContext.campaigns'  is null.");
            }
            var campaign = await dbContext.campaigns.FindAsync(id);
            if (campaign != null)
            {
                dbContext.campaigns.Remove(campaign);
            }
            
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampaignExists(int id)
        {
          return dbContext.campaigns.Any(e => e.Id == id);
        }
    }
}
