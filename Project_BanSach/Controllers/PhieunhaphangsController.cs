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
    public class PhieunhaphangsController : Controller
    {
        private readonly WebBanSachSqlContext _context;

        public PhieunhaphangsController(WebBanSachSqlContext context)
        {
            _context = context;
        }

        // GET: Phieunhaphangs
        public async Task<IActionResult> Index()
        {
            var webBanSachSqlContext = _context.Phieunhaphangs.Include(p => p.MaNccPnNavigation);
            return View(await webBanSachSqlContext.ToListAsync());
        }

        // GET: Phieunhaphangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Phieunhaphangs == null)
            {
                return NotFound();
            }

            var phieunhaphang = await _context.Phieunhaphangs
                .Include(p => p.MaNccPnNavigation)
                .FirstOrDefaultAsync(m => m.MaPn == id);
            if (phieunhaphang == null)
            {
                return NotFound();
            }

            return View(phieunhaphang);
        }

        // GET: Phieunhaphangs/Create
        public IActionResult Create()
        {
            ViewData["MaNccPn"] = new SelectList(_context.Nccs, "MaNcc", "MaNcc");
            return View();
        }

        // POST: Phieunhaphangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaPn,NgayNhap,MaNccPn")] Phieunhaphang phieunhaphang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phieunhaphang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNccPn"] = new SelectList(_context.Nccs, "MaNcc", "MaNcc", phieunhaphang.MaNccPn);
            return View(phieunhaphang);
        }

        // GET: Phieunhaphangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Phieunhaphangs == null)
            {
                return NotFound();
            }

            var phieunhaphang = await _context.Phieunhaphangs.FindAsync(id);
            if (phieunhaphang == null)
            {
                return NotFound();
            }
            ViewData["MaNccPn"] = new SelectList(_context.Nccs, "MaNcc", "MaNcc", phieunhaphang.MaNccPn);
            return View(phieunhaphang);
        }

        // POST: Phieunhaphangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaPn,NgayNhap,MaNccPn")] Phieunhaphang phieunhaphang)
        {
            if (id != phieunhaphang.MaPn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phieunhaphang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhieunhaphangExists(phieunhaphang.MaPn))
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
            ViewData["MaNccPn"] = new SelectList(_context.Nccs, "MaNcc", "MaNcc", phieunhaphang.MaNccPn);
            return View(phieunhaphang);
        }

        // GET: Phieunhaphangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Phieunhaphangs == null)
            {
                return NotFound();
            }

            var phieunhaphang = await _context.Phieunhaphangs
                .Include(p => p.MaNccPnNavigation)
                .FirstOrDefaultAsync(m => m.MaPn == id);
            if (phieunhaphang == null)
            {
                return NotFound();
            }

            return View(phieunhaphang);
        }

        // POST: Phieunhaphangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Phieunhaphangs == null)
            {
                return Problem("Entity set 'WebBanSachSqlContext.Phieunhaphangs'  is null.");
            }
            var phieunhaphang = await _context.Phieunhaphangs.FindAsync(id);
            if (phieunhaphang != null)
            {
                _context.Phieunhaphangs.Remove(phieunhaphang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhieunhaphangExists(int id)
        {
          return (_context.Phieunhaphangs?.Any(e => e.MaPn == id)).GetValueOrDefault();
        }
    }
}
