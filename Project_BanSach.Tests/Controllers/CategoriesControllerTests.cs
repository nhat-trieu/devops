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

        [Fact]
        public async Task Details_WithValidId_ReturnsCategory()
        {
            var options = GetDbOptions();

            using (var context = new WebBanSachSqlContext(options))
            {
                context.Categories.Add(new Category { MaCate = 5, TenDanhMuc = "Thể thao" });
                await context.SaveChangesAsync();
            }

            using (var context = new WebBanSachSqlContext(options))
            {
                var controller = new CategoriesController(context);
                var result = await controller.Details(5);

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<Category>(viewResult.Model);
                Assert.Equal(5, model.MaCate);
            }
        }
        [Fact]
public async Task Create_Post_ValidCategory_RedirectsToIndex()
{
    var options = GetDbOptions();

    using (var context = new WebBanSachSqlContext(options))
    {
        var controller = new CategoriesController(context);
        var category = new Category
        {
            MaCate = 9,
            TenDanhMuc = "Giáo trình",
            TrangThai = true,
            ParentId = null
        };

        controller.ModelState.Clear(); // để giả định ModelState.IsValid = true

        var result = await controller.Create(category);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
    }
}

        [Fact]
        public async Task Details_WithInvalidId_ReturnsNotFound()
        {
            var options = GetDbOptions();

            using (var context = new WebBanSachSqlContext(options))
            {
                var controller = new CategoriesController(context);
                var result = await controller.Details(999); // id không tồn tại

                Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}
