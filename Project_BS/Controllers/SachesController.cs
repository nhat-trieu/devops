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
    public class SachesController : Controller
    {
        private readonly WebBanSachSqlContext _context;

        public SachesController(WebBanSachSqlContext context)
        {
            _context = context;
        }

        // GET: Saches
        public async Task<IActionResult> Index()
        {
            var webBanSachSqlContext = _context.Saches.Include(s => s.MaCateSachNavigation).Include(s => s.MaDiscSachNavigation).Include(s => s.MaLoaiSachSachNavigation).Include(s => s.MaNccSachNavigation).Include(s => s.MaNxbSachNavigation).Include(s => s.MaTgSachNavigation);
            return View(await webBanSachSqlContext.ToListAsync());
        }

        // GET: Saches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Saches == null)
            {
                return NotFound();
            }

            var sach = await _context.Saches
                .Include(s => s.MaCateSachNavigation)
                .Include(s => s.MaDiscSachNavigation)
                .Include(s => s.MaLoaiSachSachNavigation)
                .Include(s => s.MaNccSachNavigation)
                .Include(s => s.MaNxbSachNavigation)
                .Include(s => s.MaTgSachNavigation)
                .FirstOrDefaultAsync(m => m.MaSach == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // GET: Saches/Create
        public IActionResult Create()
        {
            ViewData["MaCateSach"] = new SelectList(_context.Categories, "MaCate", "MaCate");
            ViewData["MaDiscSach"] = new SelectList(_context.Discounts, "MaDisc", "MaDisc");
            ViewData["MaLoaiSachSach"] = new SelectList(_context.Loaisaches, "MaLoaiSach", "MaLoaiSach");
            ViewData["MaNccSach"] = new SelectList(_context.Nccs, "MaNcc", "MaNcc");
            ViewData["MaNxbSach"] = new SelectList(_context.Nxbs, "MaNxb", "MaNxb");
            ViewData["MaTgSach"] = new SelectList(_context.Tacgia, "MaTg", "MaTg");
            return View();
        }

        // POST: Saches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSach,TenSach,HinhAnh,GiaBan,TrangThai,MoTa,MaNxbSach,MaTgSach,MaLoaiSachSach,MaNccSach,MaCateSach,MaDiscSach")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaCateSach"] = new SelectList(_context.Categories, "MaCate", "MaCate", sach.MaCateSach);
            ViewData["MaDiscSach"] = new SelectList(_context.Discounts, "MaDisc", "MaDisc", sach.MaDiscSach);
            ViewData["MaLoaiSachSach"] = new SelectList(_context.Loaisaches, "MaLoaiSach", "MaLoaiSach", sach.MaLoaiSachSach);
            ViewData["MaNccSach"] = new SelectList(_context.Nccs, "MaNcc", "MaNcc", sach.MaNccSach);
            ViewData["MaNxbSach"] = new SelectList(_context.Nxbs, "MaNxb", "MaNxb", sach.MaNxbSach);
            ViewData["MaTgSach"] = new SelectList(_context.Tacgia, "MaTg", "MaTg", sach.MaTgSach);
            return View(sach);
        }

        // GET: Saches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Saches == null)
            {
                return NotFound();
            }

            var sach = await _context.Saches.FindAsync(id);
            if (sach == null)
            {
                return NotFound();
            }
            ViewData["MaCateSach"] = new SelectList(_context.Categories, "MaCate", "MaCate", sach.MaCateSach);
            ViewData["MaDiscSach"] = new SelectList(_context.Discounts, "MaDisc", "MaDisc", sach.MaDiscSach);
            ViewData["MaLoaiSachSach"] = new SelectList(_context.Loaisaches, "MaLoaiSach", "MaLoaiSach", sach.MaLoaiSachSach);
            ViewData["MaNccSach"] = new SelectList(_context.Nccs, "MaNcc", "MaNcc", sach.MaNccSach);
            ViewData["MaNxbSach"] = new SelectList(_context.Nxbs, "MaNxb", "MaNxb", sach.MaNxbSach);
            ViewData["MaTgSach"] = new SelectList(_context.Tacgia, "MaTg", "MaTg", sach.MaTgSach);
            return View(sach);
        }

        // POST: Saches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSach,TenSach,HinhAnh,GiaBan,TrangThai,MoTa,MaNxbSach,MaTgSach,MaLoaiSachSach,MaNccSach,MaCateSach,MaDiscSach")] Sach sach)
        {
            if (id != sach.MaSach)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SachExists(sach.MaSach))
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
            ViewData["MaCateSach"] = new SelectList(_context.Categories, "MaCate", "MaCate", sach.MaCateSach);
            ViewData["MaDiscSach"] = new SelectList(_context.Discounts, "MaDisc", "MaDisc", sach.MaDiscSach);
            ViewData["MaLoaiSachSach"] = new SelectList(_context.Loaisaches, "MaLoaiSach", "MaLoaiSach", sach.MaLoaiSachSach);
            ViewData["MaNccSach"] = new SelectList(_context.Nccs, "MaNcc", "MaNcc", sach.MaNccSach);
            ViewData["MaNxbSach"] = new SelectList(_context.Nxbs, "MaNxb", "MaNxb", sach.MaNxbSach);
            ViewData["MaTgSach"] = new SelectList(_context.Tacgia, "MaTg", "MaTg", sach.MaTgSach);
            return View(sach);
        }

        // GET: Saches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Saches == null)
            {
                return NotFound();
            }

            var sach = await _context.Saches
                .Include(s => s.MaCateSachNavigation)
                .Include(s => s.MaDiscSachNavigation)
                .Include(s => s.MaLoaiSachSachNavigation)
                .Include(s => s.MaNccSachNavigation)
                .Include(s => s.MaNxbSachNavigation)
                .Include(s => s.MaTgSachNavigation)
                .FirstOrDefaultAsync(m => m.MaSach == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // POST: Saches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Saches == null)
            {
                return Problem("Entity set 'WebBanSachSqlContext.Saches'  is null.");
            }
            var sach = await _context.Saches.FindAsync(id);
            if (sach != null)
            {
                _context.Saches.Remove(sach);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SachExists(int id)
        {
          return (_context.Saches?.Any(e => e.MaSach == id)).GetValueOrDefault();
        }
    }
}
