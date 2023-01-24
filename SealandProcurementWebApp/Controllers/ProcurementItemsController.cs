using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SealandProcurementWebApp.Data;
using SealandProcurementWebApp.Models;

namespace SealandProcurementWebApp.Controllers
{
    public class ProcurementItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProcurementItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProcurementItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProcurementItem.ToListAsync());
        }

        // GET: ProcurementItems/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // PoST: ProcurementItems/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.ProcurementItem.Where( j => j.CompName.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: ProcurementItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procurementItem = await _context.ProcurementItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (procurementItem == null)
            {
                return NotFound();
            }

            return View(procurementItem);
        }

        // GET: ProcurementItems/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProcurementItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompName,Price,Quantity,HireTime,Source,Date")] ProcurementItem procurementItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(procurementItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(procurementItem);
        }

        // GET: ProcurementItems/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procurementItem = await _context.ProcurementItem.FindAsync(id);
            if (procurementItem == null)
            {
                return NotFound();
            }
            return View(procurementItem);
        }

        // POST: ProcurementItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompName,Price,Quantity,HireTime,Source,Date")] ProcurementItem procurementItem)
        {
            if (id != procurementItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(procurementItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcurementItemExists(procurementItem.Id))
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
            return View(procurementItem);
        }

        // GET: ProcurementItems/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procurementItem = await _context.ProcurementItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (procurementItem == null)
            {
                return NotFound();
            }

            return View(procurementItem);
        }

        // POST: ProcurementItems/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var procurementItem = await _context.ProcurementItem.FindAsync(id);
            _context.ProcurementItem.Remove(procurementItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcurementItemExists(int id)
        {
            return _context.ProcurementItem.Any(e => e.Id == id);
        }
    }
}
