using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Project_BanSach.Tests
{
    public class OrderTests
    {
        public class OrderItem
        {
            public int ProductId { get; set; }
            public int SoLuong { get; set; }
            public decimal DonGia { get; set; }
        }

        public class Order
        {
            public int UserId { get; set; }
            public List<OrderItem> Items { get; set; } = new();
        }

        [Fact]
        public void MuaNhanh_TaoDonHang_ThanhCong()
        {
            // Arrange
            int userId = 1;
            int productId = 100;
            decimal donGia = 50000;

            var order = new Order
            {
                UserId = userId,
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductId = productId, SoLuong = 1, DonGia = donGia }
                }
            };

            // Assert
            Assert.Equal(1, order.UserId);
            Assert.Single(order.Items);
            Assert.Equal(productId, order.Items[0].ProductId);
        }

        [Fact]
        public void TinhTongTienDonHang_DungKetQua()
        {
            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductId = 1, SoLuong = 2, DonGia = 50000 },
                    new OrderItem { ProductId = 2, SoLuong = 1, DonGia = 30000 }
                }
            };

            var tongTien = order.Items.Sum(i => i.SoLuong * i.DonGia);

            Assert.Equal(130000, tongTien);
        }
    }
}
