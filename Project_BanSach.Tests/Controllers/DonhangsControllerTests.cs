using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_BanSach.Controllers;
using Project_BanSach.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Project_BanSach.Tests.Controllers
{
    public class DonhangsControllerTests
    {
        private DbContextOptions<WebBanSachSqlContext> GetDbOptions() =>
            new DbContextOptionsBuilder<WebBanSachSqlContext>()
                .UseInMemoryDatabase("TestDb_Donhangs").Options;

        [Fact]
        public async Task Index_ReturnsViewWithDonhangs()
        {
            var options = GetDbOptions();

            using (var context = new WebBanSachSqlContext(options))
            {
                context.Donhangs.Add(new Donhang
                {
                    MaDh = 1,
                    TinhTrang = true,
                    NgayDat = DateTime.Now,
                    DiaChiGiaoHang = "Hà Nội"
                });
                await context.SaveChangesAsync();
            }

            using (var context = new WebBanSachSqlContext(options))
            {
                var controller = new DonhangsController(context);
                var result = await controller.Index();

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<List<Donhang>>(viewResult.Model);
                Assert.Single(model);
            }
        }
    }
}
