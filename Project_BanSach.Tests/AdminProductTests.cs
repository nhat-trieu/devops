using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Project_BanSach.Tests
{
    public class AdminProductTests
    {
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public decimal Price { get; set; }
        }

        [Fact]
        public void ThemSanPham_ThanhCong()
        {
            var list = new List<Product>();
            var newProduct = new Product { Id = 1, Name = "Sách Giáo Khoa", Price = 50000 };

            list.Add(newProduct);

            Assert.Single(list);
            Assert.Equal("Sách Giáo Khoa", list[0].Name);
        }

        [Fact]
        public void XoaSanPham_ThanhCong()
        {
            var list = new List<Product>
            {
                new Product { Id = 1, Name = "Sách Cũ" },
                new Product { Id = 2, Name = "Sách Mới" }
            };

            list.RemoveAll(p => p.Id == 1);

            Assert.Single(list);
            Assert.Equal(2, list[0].Id);
        }

        [Fact]
        public void CapNhatSanPham_ThanhCong()
        {
            var product = new Product { Id = 1, Name = "Sách Văn", Price = 30000 };

            // Act: cập nhật tên và giá
            product.Name = "Sách Văn Mới";
            product.Price = 40000;

            // Assert
            Assert.Equal("Sách Văn Mới", product.Name);
            Assert.Equal(40000, product.Price);
        }
    }
}
