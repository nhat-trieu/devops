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
    public class NccsController : Controller
    {
        private readonly WebBanSachSqlContext _context;

        public NccsController(WebBanSachSqlContext context)
        {
            _context = context;
        }

        // GET: Nccs
        public async Task<IActionResult> Index()
        {
              return _context.Nccs != null ? 
                          View(await _context.Nccs.ToListAsync()) :
                          Problem("Entity set 'WebBanSachSqlContext.Nccs'  is null.");
        }

        // GET: Nccs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nccs == null)
            {
                return NotFound();
            }

            var ncc = await _context.Nccs
                .FirstOrDefaultAsync(m => m.MaNcc == id);
            if (ncc == null)
            {
                return NotFound();
            }

            return View(ncc);
        }

        // GET: Nccs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nccs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNcc,TenNcc")] Ncc ncc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ncc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ncc);
        }

        // GET: Nccs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nccs == null)
            {
                return NotFound();
            }

            var ncc = await _context.Nccs.FindAsync(id);
            if (ncc == null)
            {
                return NotFound();
            }
            return View(ncc);
        }

        // POST: Nccs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNcc,TenNcc")] Ncc ncc)
        {
            if (id != ncc.MaNcc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ncc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NccExists(ncc.MaNcc))
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
            return View(ncc);
        }

        // GET: Nccs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nccs == null)
            {
                return NotFound();
            }

            var ncc = await _context.Nccs
                .FirstOrDefaultAsync(m => m.MaNcc == id);
            if (ncc == null)
            {
                return NotFound();
            }

            return View(ncc);
        }

        // POST: Nccs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nccs == null)
            {
                return Problem("Entity set 'WebBanSachSqlContext.Nccs'  is null.");
            }
            var ncc = await _context.Nccs.FindAsync(id);
            if (ncc != null)
            {
                _context.Nccs.Remove(ncc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NccExists(int id)
        {
          return (_context.Nccs?.Any(e => e.MaNcc == id)).GetValueOrDefault();
        }
    }
}
