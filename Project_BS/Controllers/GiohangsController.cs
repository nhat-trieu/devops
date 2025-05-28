using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project_BanSach.Models;

namespace Project_BanSach.Controllers
{
    public class GiohangsController : Controller
    {
        private readonly WebBanSachSqlContext _context;

        public GiohangsController(WebBanSachSqlContext context)
        {
            _context = context;
        }

        // GET: Giohangs
        public ActionResult GioHang()
        {
            return View();
        }
        [HttpGet]
        public IActionResult getGioHang()
        {
            Request.Headers.TryGetValue("sdt", out var sdt);
            Request.Headers.TryGetValue("matkhau", out var matkhau);
            SqlConnection conString = new SqlConnection("Data Source=TUONGVIB09C;Database=WebBanSach_SQL;User ID=" + sdt.ToString() + ";Password=" + matkhau.ToString() + ";Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            if (conString.State == System.Data.ConnectionState.Closed)
            {
                conString.Open();
            }
            
            string select = $"SELECT MaGH, MaKH, MaSach, SoLuong, GiaBan, TenSach, HinhAnh FROM KHACHHANG JOIN GIOHANG ON MaKH=MaKH_GH JOIN CHITIET_GIOHANG ON MaGH=MaGH_CTGH JOIN SACH ON MaSach_CTGH=MaSach WHERE Sdt='{sdt}'";
            SqlCommand cmd = new SqlCommand(select, conString);
            SqlDataReader rd = cmd.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(rd);
            string jsonString  = string.Empty;
            jsonString = JsonConvert.SerializeObject(dataTable);
           


            if (conString.State == System.Data.ConnectionState.Open)
            {
                conString.Close();
            }
            var json = new
            {
                success = true,
                data = jsonString
            };
            return Json(json);

        }
        
        [HttpPost]
        public IActionResult thanhToan(int maSach, int soLuong, int tongTien, int maGH)
        {
            Console.Clear();
            
            

            Request.Headers.TryGetValue("sdt", out var sdt);
            Request.Headers.TryGetValue("matkhau", out var matkhau);
            Console.WriteLine(maSach);
            SqlConnection conString = new SqlConnection("Data Source=TUONGVIB09C;Database=WebBanSach_SQL;User ID=" + sdt.ToString() + ";Password=" + matkhau.ToString() + ";Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            if (conString.State == System.Data.ConnectionState.Closed)
            {
                conString.Open();
            }
            int maKH = 0;
            try
            {
                Console.WriteLine(soLuong);
                string select = $"SELECT MaKH FROM KHACHHANG WHERE Sdt='{sdt}'";
                SqlCommand cmd = new SqlCommand(select, conString);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                maKH = Convert.ToInt32(rd["MaKH"]);
                rd.Close();
                string insert = $"INSERT INTO DONHANG VALUES(null, null,1, null, {maKH})";
                cmd = new SqlCommand(insert, conString);
                cmd.ExecuteNonQuery();
                insert = $"INSERT INTO CHITIET_DONHANG VALUES(IDENT_CURRENT ('DONHANG'), {maSach}, {soLuong}, {tongTien})";
                cmd = new SqlCommand(insert, conString);
                cmd.ExecuteNonQuery();
                string delete = $"DELETE FROM CHITIET_GIOHANG  WHERE MaSach_CTGH = {maSach}  AND MaGH_CTGH={maGH}";
                cmd = new SqlCommand(delete, conString);
                cmd.ExecuteNonQuery();
            } catch (Exception ex)
            {
                Console.WriteLine(tongTien);
                string insert = $"INSERT INTO CHITIET_DONHANG VALUES(IDENT_CURRENT ('DONHANG'), {maSach}, {soLuong}, {tongTien}, 1)";
                SqlCommand cmd = new SqlCommand(insert, conString);
                cmd.ExecuteNonQuery();
                string delete = $"DELETE FROM CHITIET_GIOHANG  WHERE MaSach_CTGH = {maSach}  AND MaGH_CTGH={maGH} ";
                cmd = new SqlCommand(delete, conString);
                cmd.ExecuteNonQuery();
            }
            if (conString.State == System.Data.ConnectionState.Open)
            {
                conString.Close();
            }
            var json = new
            {
                success = true
            };
            return Json(json);

        } 
        public async Task<IActionResult> Index()
        {
            var webBanSachSqlContext = _context.Giohangs.Include(g => g.MaKhGhNavigation);
            return View(await webBanSachSqlContext.ToListAsync());
        }

        // GET: Giohangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Giohangs == null)
            {
                return NotFound();
            }

            var giohang = await _context.Giohangs
                .Include(g => g.MaKhGhNavigation)
                .FirstOrDefaultAsync(m => m.MaGh == id);
            if (giohang == null)
            {
                return NotFound();
            }

            return View(giohang);
        }

        // GET: Giohangs/Create
        public IActionResult Create()
        {
            ViewData["MaKhGh"] = new SelectList(_context.Khachhangs, "MaKh", "MaKh");
            return View();
        }

        // POST: Giohangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaGh,MaKhGh")] Giohang giohang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giohang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKhGh"] = new SelectList(_context.Khachhangs, "MaKh", "MaKh", giohang.MaKhGh);
            return View(giohang);
        }

        // GET: Giohangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Giohangs == null)
            {
                return NotFound();
            }

            var giohang = await _context.Giohangs.FindAsync(id);
            if (giohang == null)
            {
                return NotFound();
            }
            ViewData["MaKhGh"] = new SelectList(_context.Khachhangs, "MaKh", "MaKh", giohang.MaKhGh);
            return View(giohang);
        }

        // POST: Giohangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaGh,MaKhGh")] Giohang giohang)
        {
            if (id != giohang.MaGh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giohang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiohangExists(giohang.MaGh))
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
            ViewData["MaKhGh"] = new SelectList(_context.Khachhangs, "MaKh", "MaKh", giohang.MaKhGh);
            return View(giohang);
        }

        // GET: Giohangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Giohangs == null)
            {
                return NotFound();
            }

            var giohang = await _context.Giohangs
                .Include(g => g.MaKhGhNavigation)
                .FirstOrDefaultAsync(m => m.MaGh == id);
            if (giohang == null)
            {
                return NotFound();
            }

            return View(giohang);
        }

        // POST: Giohangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Giohangs == null)
            {
                return Problem("Entity set 'WebBanSachSqlContext.Giohangs'  is null.");
            }
            var giohang = await _context.Giohangs.FindAsync(id);
            if (giohang != null)
            {
                _context.Giohangs.Remove(giohang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiohangExists(int id)
        {
          return (_context.Giohangs?.Any(e => e.MaGh == id)).GetValueOrDefault();
        }

    }
}
