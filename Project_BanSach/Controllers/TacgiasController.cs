using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_BanSach.Models;

namespace Project_BanSach.Controllers
{
    public class TacgiasController : Controller
    {
        private readonly WebBanSachSqlContext _context;

        public TacgiasController(WebBanSachSqlContext context)
        {
            _context = context;
        }

        // GET: Tacgias
        public async Task<IActionResult> Index()
        {
              return _context.Tacgia != null ? 
                          View(await _context.Tacgia.ToListAsync()) :
                          Problem("Entity set 'WebBanSachSqlContext.Tacgia'  is null.");
        }

        // GET: Tacgias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tacgia == null)
            {
                return NotFound();
            }

            var tacgia = await _context.Tacgia
                .FirstOrDefaultAsync(m => m.MaTg == id);
            if (tacgia == null)
            {
                return NotFound();
            }

            return View(tacgia);
        }

        // GET: Tacgias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tacgias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTg,TenTacGia")] Tacgia tacgia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tacgia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tacgia);
        }

        // GET: Tacgias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tacgia == null)
            {
                return NotFound();
            }

            var tacgia = await _context.Tacgia.FindAsync(id);
            if (tacgia == null)
            {
                return NotFound();
            }
            return View(tacgia);
        }

        // POST: Tacgias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTg,TenTacGia")] Tacgia tacgia)
        {
            if (id != tacgia.MaTg)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tacgia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TacgiaExists(tacgia.MaTg))
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
            return View(tacgia);
        }

        // GET: Tacgias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tacgia == null)
            {
                return NotFound();
            }

            var tacgia = await _context.Tacgia
                .FirstOrDefaultAsync(m => m.MaTg == id);
            if (tacgia == null)
            {
                return NotFound();
            }

            return View(tacgia);
        }

        // POST: Tacgias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tacgia == null)
            {
                return Problem("Entity set 'WebBanSachSqlContext.Tacgia'  is null.");
            }
            var tacgia = await _context.Tacgia.FindAsync(id);
            if (tacgia != null)
            {
                _context.Tacgia.Remove(tacgia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TacgiaExists(int id)
        {
          return (_context.Tacgia?.Any(e => e.MaTg == id)).GetValueOrDefault();
        }
    }
}
