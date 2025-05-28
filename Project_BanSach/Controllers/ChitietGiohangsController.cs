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
    public class ChitietGiohangsController : Controller
    {
        private readonly WebBanSachSqlContext _context;

        public ChitietGiohangsController(WebBanSachSqlContext context)
        {
            _context = context;
        }

        // GET: ChitietGiohangs
        public async Task<IActionResult> Index()
        {
            var webBanSachSqlContext = _context.ChitietGiohangs.Include(c => c.MaGhCtghNavigation).Include(c => c.MaSachCtghNavigation);
            return View(await webBanSachSqlContext.ToListAsync());
        }

        // GET: ChitietGiohangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChitietGiohangs == null)
            {
                return NotFound();
            }

            var chitietGiohang = await _context.ChitietGiohangs
                .Include(c => c.MaGhCtghNavigation)
                .Include(c => c.MaSachCtghNavigation)
                .FirstOrDefaultAsync(m => m.MaGhCtgh == id);
            if (chitietGiohang == null)
            {
                return NotFound();
            }

            return View(chitietGiohang);
        }

        // GET: ChitietGiohangs/Create
        public IActionResult Create()
        {
            ViewData["MaGhCtgh"] = new SelectList(_context.Giohangs, "MaGh", "MaGh");
            ViewData["MaSachCtgh"] = new SelectList(_context.Saches, "MaSach", "MaSach");
            return View();
        }

        // POST: ChitietGiohangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaGhCtgh,MaSachCtgh,SoLuong")] ChitietGiohang chitietGiohang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chitietGiohang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaGhCtgh"] = new SelectList(_context.Giohangs, "MaGh", "MaGh", chitietGiohang.MaGhCtgh);
            ViewData["MaSachCtgh"] = new SelectList(_context.Saches, "MaSach", "MaSach", chitietGiohang.MaSachCtgh);
            return View(chitietGiohang);
        }

        // GET: ChitietGiohangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChitietGiohangs == null)
            {
                return NotFound();
            }

            var chitietGiohang = await _context.ChitietGiohangs.FindAsync(id);
            if (chitietGiohang == null)
            {
                return NotFound();
            }
            ViewData["MaGhCtgh"] = new SelectList(_context.Giohangs, "MaGh", "MaGh", chitietGiohang.MaGhCtgh);
            ViewData["MaSachCtgh"] = new SelectList(_context.Saches, "MaSach", "MaSach", chitietGiohang.MaSachCtgh);
            return View(chitietGiohang);
        }

        // POST: ChitietGiohangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaGhCtgh,MaSachCtgh,SoLuong")] ChitietGiohang chitietGiohang)
        {
            if (id != chitietGiohang.MaGhCtgh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chitietGiohang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChitietGiohangExists(chitietGiohang.MaGhCtgh))
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
            ViewData["MaGhCtgh"] = new SelectList(_context.Giohangs, "MaGh", "MaGh", chitietGiohang.MaGhCtgh);
            ViewData["MaSachCtgh"] = new SelectList(_context.Saches, "MaSach", "MaSach", chitietGiohang.MaSachCtgh);
            return View(chitietGiohang);
        }

        // GET: ChitietGiohangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChitietGiohangs == null)
            {
                return NotFound();
            }

            var chitietGiohang = await _context.ChitietGiohangs
                .Include(c => c.MaGhCtghNavigation)
                .Include(c => c.MaSachCtghNavigation)
                .FirstOrDefaultAsync(m => m.MaGhCtgh == id);
            if (chitietGiohang == null)
            {
                return NotFound();
            }

            return View(chitietGiohang);
        }

        // POST: ChitietGiohangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChitietGiohangs == null)
            {
                return Problem("Entity set 'WebBanSachSqlContext.ChitietGiohangs'  is null.");
            }
            var chitietGiohang = await _context.ChitietGiohangs.FindAsync(id);
            if (chitietGiohang != null)
            {
                _context.ChitietGiohangs.Remove(chitietGiohang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChitietGiohangExists(int id)
        {
          return (_context.ChitietGiohangs?.Any(e => e.MaGhCtgh == id)).GetValueOrDefault();
        }
    }
}
