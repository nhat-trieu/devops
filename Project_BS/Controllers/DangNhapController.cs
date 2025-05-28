using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Project_BanSach.Models;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;


namespace Project_BanSach.Controllers
{
    
    public class DangNhapController : Controller
    {
        // GET: DangNhapController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DangNhapController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DangNhapController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DangNhapController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DangNhapController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DangNhapController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DangNhapController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DangNhapController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult DangKy (string sdt, string matkhau)
        {
            Console.Clear();
            Console.WriteLine(matkhau);
            Console.WriteLine(sdt);
            SqlConnection conString = new SqlConnection("Data Source=TUONGVIB09C;Initial Catalog=WebBanSach_SQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            if (conString.State == System.Data.ConnectionState.Closed)
            {
                conString.Open();
            }

            SqlCommand cmd = new SqlCommand("SP_CreateKhachHang", conString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TenLogin", sdt);
            cmd.Parameters.AddWithValue("@SdtKH", sdt);
            cmd.Parameters.AddWithValue("@PW", matkhau);

            SqlParameter RuturnValue = new SqlParameter("@result", SqlDbType.Int);
            RuturnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(RuturnValue);
            cmd.ExecuteNonQuery();
            int result = (int)cmd.Parameters["@result"].Value;
            Console.WriteLine(result);
            if (result != 0)
            {
                string insert = $"INSERT INTO KHACHHANG VALUES (null, '{sdt}', null)";
                cmd = new SqlCommand(insert, conString);
                cmd.ExecuteNonQuery();
                string select = $"SELECT MaKH FROM KHACHHANG WHERE Sdt = '{sdt}'";
                cmd = new SqlCommand(select, conString);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                int maKH = Convert.ToInt32(rd["MaKH"]);
                rd.Close();
                insert = $"INSERT INTO GIOHANG VALUES ("+maKH.ToString()+")";
                cmd = new SqlCommand (insert, conString);
                cmd.ExecuteNonQuery ();

            }
            
            if (conString.State == System.Data.ConnectionState.Open)
            {
                conString.Close();
            }
            var jsonString = JsonConvert.SerializeObject(result);
            var json = new
            {
                success = true,
                data = result
            };
            
            //return Json(json);
            return Json(json);
        }
        public IActionResult DangNhap(string sdt, string matkhau)
        {
           
           
            var check = 0;
            try
            {
                Console.Clear();
                SqlConnection connsql = new SqlConnection("Data Source=TUONGVIB09C;Database=WebBanSach_SQL;User ID=" + sdt + ";Password=" + matkhau +";Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
                if (connsql.State == ConnectionState.Closed)
                {
                    connsql.Open();
                }
                if (connsql.State == ConnectionState.Open)
                {
                    
                    Console.WriteLine(matkhau);
                    Console.WriteLine(sdt);
                    check = 1;
                }
                else
                {
                    Console.WriteLine("123");
                }
               
                if (connsql.State == ConnectionState.Open)
                {
                    connsql.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("1234");
            }
            var jsonString = JsonConvert.SerializeObject(check);
            var json = new
            {
                success = true,
                data = check
            };
            return Json(json);
        }

    }
}
