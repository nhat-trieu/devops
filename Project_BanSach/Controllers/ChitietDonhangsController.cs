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
    public class ChitietDonhangsController : Controller
    {
        private readonly WebBanSachSqlContext _context;

        public ChitietDonhangsController(WebBanSachSqlContext context)
        {
            _context = context;
        }

        // GET: ChitietDonhangs
        public async Task<IActionResult> Index()
        {
            var webBanSachSqlContext = _context.ChitietDonhangs.Include(c => c.MaDhCtdhNavigation).Include(c => c.MaSachCtdhNavigation);
            return View(await webBanSachSqlContext.ToListAsync());
        }

        // GET: ChitietDonhangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChitietDonhangs == null)
            {
                return NotFound();
            }

            var chitietDonhang = await _context.ChitietDonhangs
                .Include(c => c.MaDhCtdhNavigation)
                .Include(c => c.MaSachCtdhNavigation)
                .FirstOrDefaultAsync(m => m.MaDhCtdh == id);
            if (chitietDonhang == null)
            {
                return NotFound();
            }

            return View(chitietDonhang);
        }

        // GET: ChitietDonhangs/Create
        public IActionResult Create()
        {
            ViewData["MaDhCtdh"] = new SelectList(_context.Donhangs, "MaDh", "MaDh");
            ViewData["MaSachCtdh"] = new SelectList(_context.Saches, "MaSach", "MaSach");
            return View();
        }

        // POST: ChitietDonhangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDhCtdh,MaSachCtdh,SoLuong,ThanhTien,TinhTrangThanhToan")] ChitietDonhang chitietDonhang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chitietDonhang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDhCtdh"] = new SelectList(_context.Donhangs, "MaDh", "MaDh", chitietDonhang.MaDhCtdh);
            ViewData["MaSachCtdh"] = new SelectList(_context.Saches, "MaSach", "MaSach", chitietDonhang.MaSachCtdh);
            return View(chitietDonhang);
        }

        // GET: ChitietDonhangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChitietDonhangs == null)
            {
                return NotFound();
            }

            var chitietDonhang = await _context.ChitietDonhangs.FindAsync(id);
            if (chitietDonhang == null)
            {
                return NotFound();
            }
            ViewData["MaDhCtdh"] = new SelectList(_context.Donhangs, "MaDh", "MaDh", chitietDonhang.MaDhCtdh);
            ViewData["MaSachCtdh"] = new SelectList(_context.Saches, "MaSach", "MaSach", chitietDonhang.MaSachCtdh);
            return View(chitietDonhang);
        }

        // POST: ChitietDonhangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDhCtdh,MaSachCtdh,SoLuong,ThanhTien,TinhTrangThanhToan")] ChitietDonhang chitietDonhang)
        {
            if (id != chitietDonhang.MaDhCtdh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chitietDonhang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChitietDonhangExists(chitietDonhang.MaDhCtdh))
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
            ViewData["MaDhCtdh"] = new SelectList(_context.Donhangs, "MaDh", "MaDh", chitietDonhang.MaDhCtdh);
            ViewData["MaSachCtdh"] = new SelectList(_context.Saches, "MaSach", "MaSach", chitietDonhang.MaSachCtdh);
            return View(chitietDonhang);
        }

        // GET: ChitietDonhangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChitietDonhangs == null)
            {
                return NotFound();
            }

            var chitietDonhang = await _context.ChitietDonhangs
                .Include(c => c.MaDhCtdhNavigation)
                .Include(c => c.MaSachCtdhNavigation)
                .FirstOrDefaultAsync(m => m.MaDhCtdh == id);
            if (chitietDonhang == null)
            {
                return NotFound();
            }

            return View(chitietDonhang);
        }

        // POST: ChitietDonhangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChitietDonhangs == null)
            {
                return Problem("Entity set 'WebBanSachSqlContext.ChitietDonhangs'  is null.");
            }
            var chitietDonhang = await _context.ChitietDonhangs.FindAsync(id);
            if (chitietDonhang != null)
            {
                _context.ChitietDonhangs.Remove(chitietDonhang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChitietDonhangExists(int id)
        {
          return (_context.ChitietDonhangs?.Any(e => e.MaDhCtdh == id)).GetValueOrDefault();
        }
    }
}
