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
    public class DonhangsController : Controller
    {
        private readonly WebBanSachSqlContext _context;

        public DonhangsController(WebBanSachSqlContext context)
        {
            _context = context;
        }

        // GET: Donhangs
        public async Task<IActionResult> Index()
        {
            var webBanSachSqlContext = _context.Donhangs.Include(d => d.MaKhDhNavigation);
            return View(await webBanSachSqlContext.ToListAsync());
        }

        // GET: Donhangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Donhangs == null)
            {
                return NotFound();
            }

            var donhang = await _context.Donhangs
                .Include(d => d.MaKhDhNavigation)
                .FirstOrDefaultAsync(m => m.MaDh == id);
            if (donhang == null)
            {
                return NotFound();
            }

            return View(donhang);
        }

        // GET: Donhangs/Create
        public IActionResult Create()
        {
            ViewData["MaKhDh"] = new SelectList(_context.Khachhangs, "MaKh", "MaKh");
            return View();
        }

        // POST: Donhangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDh,NgayDat,NgayGiao,TinhTrang,DiaChiGiaoHang,MaKhDh")] Donhang donhang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donhang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKhDh"] = new SelectList(_context.Khachhangs, "MaKh", "MaKh", donhang.MaKhDh);
            return View(donhang);
        }

        // GET: Donhangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Donhangs == null)
            {
                return NotFound();
            }

            var donhang = await _context.Donhangs.FindAsync(id);
            if (donhang == null)
            {
                return NotFound();
            }
            ViewData["MaKhDh"] = new SelectList(_context.Khachhangs, "MaKh", "MaKh", donhang.MaKhDh);
            return View(donhang);
        }

        // POST: Donhangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDh,NgayDat,NgayGiao,TinhTrang,DiaChiGiaoHang,MaKhDh")] Donhang donhang)
        {
            if (id != donhang.MaDh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donhang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonhangExists(donhang.MaDh))
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
            ViewData["MaKhDh"] = new SelectList(_context.Khachhangs, "MaKh", "MaKh", donhang.MaKhDh);
            return View(donhang);
        }

        // GET: Donhangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Donhangs == null)
            {
                return NotFound();
            }

            var donhang = await _context.Donhangs
                .Include(d => d.MaKhDhNavigation)
                .FirstOrDefaultAsync(m => m.MaDh == id);
            if (donhang == null)
            {
                return NotFound();
            }

            return View(donhang);
        }

        // POST: Donhangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Donhangs == null)
            {
                return Problem("Entity set 'WebBanSachSqlContext.Donhangs'  is null.");
            }
            var donhang = await _context.Donhangs.FindAsync(id);
            if (donhang != null)
            {
                _context.Donhangs.Remove(donhang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonhangExists(int id)
        {
          return (_context.Donhangs?.Any(e => e.MaDh == id)).GetValueOrDefault();
        }
    }
}
