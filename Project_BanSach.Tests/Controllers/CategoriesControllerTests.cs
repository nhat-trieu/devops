using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_BanSach.Controllers;
using Project_BanSach.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Project_BanSach.Tests.Controllers
{
    public class CategoriesControllerTests
    {
        private DbContextOptions<WebBanSachSqlContext> GetDbOptions() =>
            new DbContextOptionsBuilder<WebBanSachSqlContext>()
                .UseInMemoryDatabase("TestDb_Categories").Options;

        [Fact]
        public async Task Index_ReturnsViewWithCategories()
        {
            var options = GetDbOptions();

            using (var context = new WebBanSachSqlContext(options))
            {
                context.Categories.Add(new Category { MaCate = 1, TenDanhMuc = "Demo" });
                await context.SaveChangesAsync();
            }

            using (var context = new WebBanSachSqlContext(options))
            {
                var controller = new CategoriesController(context);
                var result = await controller.Index();

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<List<Category>>(viewResult.Model);
                Assert.Single(model);
            }
        }
    }
}
