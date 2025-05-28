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
    public class NxbsController : Controller
    {
        private readonly WebBanSachSqlContext _context;

        public NxbsController(WebBanSachSqlContext context)
        {
            _context = context;
        }

        // GET: Nxbs
        public async Task<IActionResult> Index()
        {
              return _context.Nxbs != null ? 
                          View(await _context.Nxbs.ToListAsync()) :
                          Problem("Entity set 'WebBanSachSqlContext.Nxbs'  is null.");
        }

        // GET: Nxbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nxbs == null)
            {
                return NotFound();
            }

            var nxb = await _context.Nxbs
                .FirstOrDefaultAsync(m => m.MaNxb == id);
            if (nxb == null)
            {
                return NotFound();
            }

            return View(nxb);
        }

        // GET: Nxbs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nxbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNxb,TenNxb")] Nxb nxb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nxb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nxb);
        }

        // GET: Nxbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nxbs == null)
            {
                return NotFound();
            }

            var nxb = await _context.Nxbs.FindAsync(id);
            if (nxb == null)
            {
                return NotFound();
            }
            return View(nxb);
        }

        // POST: Nxbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNxb,TenNxb")] Nxb nxb)
        {
            if (id != nxb.MaNxb)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nxb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NxbExists(nxb.MaNxb))
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
            return View(nxb);
        }

        // GET: Nxbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nxbs == null)
            {
                return NotFound();
            }

            var nxb = await _context.Nxbs
                .FirstOrDefaultAsync(m => m.MaNxb == id);
            if (nxb == null)
            {
                return NotFound();
            }

            return View(nxb);
        }

        // POST: Nxbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nxbs == null)
            {
                return Problem("Entity set 'WebBanSachSqlContext.Nxbs'  is null.");
            }
            var nxb = await _context.Nxbs.FindAsync(id);
            if (nxb != null)
            {
                _context.Nxbs.Remove(nxb);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NxbExists(int id)
        {
          return (_context.Nxbs?.Any(e => e.MaNxb == id)).GetValueOrDefault();
        }
    }
}
