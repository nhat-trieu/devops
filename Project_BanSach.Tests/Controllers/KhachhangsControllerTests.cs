using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_BanSach.Controllers;
using Project_BanSach.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Project_BanSach.Tests.Controllers
{
    public class KhachhangsControllerTests
    {
        private DbContextOptions<WebBanSachSqlContext> GetDbOptions() =>
            new DbContextOptionsBuilder<WebBanSachSqlContext>()
                .UseInMemoryDatabase("TestDb_Khachhangs").Options;

        [Fact]
        public async Task Index_ReturnsViewWithKhachhangs()
        {
            var options = GetDbOptions();

            using (var context = new WebBanSachSqlContext(options))
            {
                context.Khachhangs.Add(new Khachhang
                {
                    MaKh = 1,
                    TenKh = "Nguyễn Văn A",
                    Sdt = "0123456789",
                    Email = "a@gmail.com"
                });
                await context.SaveChangesAsync();
            }

            using (var context = new WebBanSachSqlContext(options))
            {
                var controller = new KhachhangsController(context);
                var result = await controller.Index();

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<List<Khachhang>>(viewResult.Model);
                Assert.Single(model);
            }
        }
    }
}
