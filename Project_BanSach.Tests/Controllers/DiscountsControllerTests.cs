using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_BanSach.Controllers;
using Project_BanSach.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Project_BanSach.Tests.Controllers
{
    public class DiscountsControllerTests
    {
        private DbContextOptions<WebBanSachSqlContext> GetInMemoryDbOptions()
        {
            return new DbContextOptionsBuilder<WebBanSachSqlContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_Discounts")
                .Options;
        }

        [Fact]
        public async Task Index_ReturnsViewWithDiscounts()
        {
            // Arrange
            var options = GetInMemoryDbOptions();

            using (var context = new WebBanSachSqlContext(options))
            {
                context.Discounts.Add(new Discount { MaDisc = 1, TenDisc = "Summer Sale", PhanTram = 10 });
                context.Discounts.Add(new Discount { MaDisc = 2, TenDisc = "Winter Sale", PhanTram = 20 });
                context.SaveChanges();
            }

            using (var context = new WebBanSachSqlContext(options))
            {
                var controller = new DiscountsController(context);

                // Act
                var result = await controller.Index();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<List<Discount>>(viewResult.Model);
                Assert.Equal(2, model.Count);
            }
        }
    }
}
