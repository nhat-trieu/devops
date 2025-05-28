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
    public class ChitietPhieunhapsController : Controller
    {
        private readonly WebBanSachSqlContext _context;

        public ChitietPhieunhapsController(WebBanSachSqlContext context)
        {
            _context = context;
        }

        // GET: ChitietPhieunhaps
        public async Task<IActionResult> Index()
        {
            var webBanSachSqlContext = _context.ChitietPhieunhaps.Include(c => c.MaPnCtpnNavigation).Include(c => c.MaSachCtpnNavigation);
            return View(await webBanSachSqlContext.ToListAsync());
        }

        // GET: ChitietPhieunhaps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChitietPhieunhaps == null)
            {
                return NotFound();
            }

            var chitietPhieunhap = await _context.ChitietPhieunhaps
                .Include(c => c.MaPnCtpnNavigation)
                .Include(c => c.MaSachCtpnNavigation)
                .FirstOrDefaultAsync(m => m.MaPnCtpn == id);
            if (chitietPhieunhap == null)
            {
                return NotFound();
            }

            return View(chitietPhieunhap);
        }

        // GET: ChitietPhieunhaps/Create
        public IActionResult Create()
        {
            ViewData["MaPnCtpn"] = new SelectList(_context.Phieunhaphangs, "MaPn", "MaPn");
            ViewData["MaSachCtpn"] = new SelectList(_context.Saches, "MaSach", "MaSach");
            return View();
        }

        // POST: ChitietPhieunhaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaPnCtpn,MaSachCtpn,GiaNhap,SoLuong,ThanhTien")] ChitietPhieunhap chitietPhieunhap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chitietPhieunhap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaPnCtpn"] = new SelectList(_context.Phieunhaphangs, "MaPn", "MaPn", chitietPhieunhap.MaPnCtpn);
            ViewData["MaSachCtpn"] = new SelectList(_context.Saches, "MaSach", "MaSach", chitietPhieunhap.MaSachCtpn);
            return View(chitietPhieunhap);
        }

        // GET: ChitietPhieunhaps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChitietPhieunhaps == null)
            {
                return NotFound();
            }

            var chitietPhieunhap = await _context.ChitietPhieunhaps.FindAsync(id);
            if (chitietPhieunhap == null)
            {
                return NotFound();
            }
            ViewData["MaPnCtpn"] = new SelectList(_context.Phieunhaphangs, "MaPn", "MaPn", chitietPhieunhap.MaPnCtpn);
            ViewData["MaSachCtpn"] = new SelectList(_context.Saches, "MaSach", "MaSach", chitietPhieunhap.MaSachCtpn);
            return View(chitietPhieunhap);
        }

        // POST: ChitietPhieunhaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaPnCtpn,MaSachCtpn,GiaNhap,SoLuong,ThanhTien")] ChitietPhieunhap chitietPhieunhap)
        {
            if (id != chitietPhieunhap.MaPnCtpn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chitietPhieunhap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChitietPhieunhapExists(chitietPhieunhap.MaPnCtpn))
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
            ViewData["MaPnCtpn"] = new SelectList(_context.Phieunhaphangs, "MaPn", "MaPn", chitietPhieunhap.MaPnCtpn);
            ViewData["MaSachCtpn"] = new SelectList(_context.Saches, "MaSach", "MaSach", chitietPhieunhap.MaSachCtpn);
            return View(chitietPhieunhap);
        }

        // GET: ChitietPhieunhaps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChitietPhieunhaps == null)
            {
                return NotFound();
            }

            var chitietPhieunhap = await _context.ChitietPhieunhaps
                .Include(c => c.MaPnCtpnNavigation)
                .Include(c => c.MaSachCtpnNavigation)
                .FirstOrDefaultAsync(m => m.MaPnCtpn == id);
            if (chitietPhieunhap == null)
            {
                return NotFound();
            }

            return View(chitietPhieunhap);
        }

        // POST: ChitietPhieunhaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChitietPhieunhaps == null)
            {
                return Problem("Entity set 'WebBanSachSqlContext.ChitietPhieunhaps'  is null.");
            }
            var chitietPhieunhap = await _context.ChitietPhieunhaps.FindAsync(id);
            if (chitietPhieunhap != null)
            {
                _context.ChitietPhieunhaps.Remove(chitietPhieunhap);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChitietPhieunhapExists(int id)
        {
          return (_context.ChitietPhieunhaps?.Any(e => e.MaPnCtpn == id)).GetValueOrDefault();
        }
    }
}
