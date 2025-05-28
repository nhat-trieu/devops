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
    public class LoaisachesController : Controller
    {
        private readonly WebBanSachSqlContext _context;

        public LoaisachesController(WebBanSachSqlContext context)
        {
            _context = context;
        }

        // GET: Loaisaches
        public async Task<IActionResult> Index()
        {
              return _context.Loaisaches != null ? 
                          View(await _context.Loaisaches.ToListAsync()) :
                          Problem("Entity set 'WebBanSachSqlContext.Loaisaches'  is null.");
        }

        // GET: Loaisaches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Loaisaches == null)
            {
                return NotFound();
            }

            var loaisach = await _context.Loaisaches
                .FirstOrDefaultAsync(m => m.MaLoaiSach == id);
            if (loaisach == null)
            {
                return NotFound();
            }

            return View(loaisach);
        }

        // GET: Loaisaches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Loaisaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoaiSach,TenLoaiSach")] Loaisach loaisach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaisach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaisach);
        }

        // GET: Loaisaches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Loaisaches == null)
            {
                return NotFound();
            }

            var loaisach = await _context.Loaisaches.FindAsync(id);
            if (loaisach == null)
            {
                return NotFound();
            }
            return View(loaisach);
        }

        // POST: Loaisaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLoaiSach,TenLoaiSach")] Loaisach loaisach)
        {
            if (id != loaisach.MaLoaiSach)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaisach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaisachExists(loaisach.MaLoaiSach))
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
            return View(loaisach);
        }

        // GET: Loaisaches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Loaisaches == null)
            {
                return NotFound();
            }

            var loaisach = await _context.Loaisaches
                .FirstOrDefaultAsync(m => m.MaLoaiSach == id);
            if (loaisach == null)
            {
                return NotFound();
            }

            return View(loaisach);
        }

        // POST: Loaisaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Loaisaches == null)
            {
                return Problem("Entity set 'WebBanSachSqlContext.Loaisaches'  is null.");
            }
            var loaisach = await _context.Loaisaches.FindAsync(id);
            if (loaisach != null)
            {
                _context.Loaisaches.Remove(loaisach);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaisachExists(int id)
        {
          return (_context.Loaisaches?.Any(e => e.MaLoaiSach == id)).GetValueOrDefault();
        }
    }
}
