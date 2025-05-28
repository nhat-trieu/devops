using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_BanSach.Models;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Project_BanSach.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WebBanSachSqlContext _webBanSachSqlContext;

        public HomeController(ILogger<HomeController> logger, WebBanSachSqlContext webBanSachSqlContext)
        {
            _logger = logger;
            _webBanSachSqlContext = webBanSachSqlContext;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            //var khachhang = _webBanSachSqlContext.Khachhangs.ToList();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult ListCat(int parentId) {
            List<Category> cats = new List<Category>();
            cats = _webBanSachSqlContext
                    .Categories
                    .Where(cat => cat.ParentId == (parentId == 0 ? null : parentId))
                    .ToList();
            var jsonString = JsonConvert.SerializeObject(cats);
            var json = new
            {
                success = true,
                data = jsonString
            };
            return Json(json);
        }
        public IActionResult ListProduct(int maCatSach)
        {
            //Request.Headers.TryGetValue("sdt", out var sdt);
            List<Sach> sach = new List<Sach>();
            sach = _webBanSachSqlContext
                    .Saches
                    .Where(book => book.MaCateSach == maCatSach)
                    .ToList();
            var jsonString = JsonConvert.SerializeObject(sach);
            var json = new
            {
                success = true,
                data = jsonString
            };
            return Json(json);
        }
        
        [HttpPost]
        
        public IActionResult muaNgay(int maSach)
        {
            Console.Clear();
            Request.Headers.TryGetValue("sdt", out var sdt);
            Request.Headers.TryGetValue("matkhau", out var matkhau);
            Console.WriteLine(sdt);
            Console.WriteLine(matkhau);

            SqlConnection conString = new SqlConnection("Data Source=TUONGVIB09C;Database=WebBanSach_SQL;User ID=" + sdt.ToString() + ";Password=" + matkhau.ToString() + ";Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            if (conString.State == System.Data.ConnectionState.Closed)
            {
                conString.Open();
            }
            string select = $"SELECT MaGH FROM GIOHANG JOIN KHACHHANG ON MaKH_GH=MaKH WHERE Sdt='{sdt}'";
            SqlCommand cmd = new SqlCommand(select, conString);
            SqlDataReader rd = cmd.ExecuteReader();
            rd.Read();
            int maGH = Convert.ToInt32(rd["MaGH"]);
            rd.Close();
            try
            {
                string insert = $"INSERT INTO CHITIET_GIOHANG VALUES ({maGH}, {maSach}, 1)";
                cmd = new SqlCommand(insert, conString);
                cmd.ExecuteNonQuery();
            } catch (Exception ex)
            {
                string selectSl = $"SELECT SoLuong FROM CHITIET_GIOHANG WHERE MaGH_CTGH={maGH} AND MaSach_CTGH={maSach} ";
                SqlCommand com = new SqlCommand(selectSl, conString);
                SqlDataReader reader = com.ExecuteReader();
                reader.Read();
                int soluong = Convert.ToInt32(reader["SoLuong"]);
                soluong += 1;
                reader.Close();
                string update = $"UPDATE CHITIET_GIOHANG SET SoLuong={soluong} WHERE MaGH_CTGH={maGH} AND MaSach_CTGH={maSach} ";
                com = new SqlCommand(update, conString);
                com.ExecuteNonQuery();
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
        [HttpGet]
        public IActionResult timKiem(string timKiem)
        {

            SqlConnection conString = new SqlConnection("Data Source=TUONGVIB09C;Initial Catalog=WebBanSach_SQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            if (conString.State == System.Data.ConnectionState.Closed)
            {
                conString.Open();
            }
            string select = $"SELECT * FROM SACH WHERE TenSach LIKE '%{timKiem}%'";
            SqlCommand cmd = new SqlCommand(select, conString);
            SqlDataReader rd = cmd.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(rd);
           
            if (conString.State == System.Data.ConnectionState.Open)
            {
                conString.Close();
            }
            string jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(dataTable);
            var json = new
            {
                success = true,
                data = jsonString
            };
            return Json(json);
        }
        public IActionResult chiTietSach(int maSach)
        {
            SqlConnection conString = new SqlConnection("Data Source=TUONGVIB09C;Initial Catalog=WebBanSach_SQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            if (conString.State == System.Data.ConnectionState.Closed)
            {
                conString.Open();
            }
            string select = $"SELECT MaSach, TenSach, HinhAnh, MoTa, GiaBan, TenTacGia, TenNXB, TenNCC, TenLoaiSach, MaCate_SACH FROM SACH JOIN TACGIA ON MaTG_SACH = MaTG JOIN NXB ON MaNXB_SACH=MaNXB JOIN NCC ON MaNCC_SACH=MaNCC JOIN LOAISACH ON MaLoaiSach_SACH=MaLoaiSach WHERE MaSach={maSach}";
            SqlCommand cmd = new SqlCommand(select, conString);
            SqlDataReader rd = cmd.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(rd);

            if (conString.State == System.Data.ConnectionState.Open)
            {
                conString.Close();
            }
            string jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(dataTable);
            var json = new
            {
                success = true,
                data = jsonString
            };
            return Json(json);
        }

    }
}