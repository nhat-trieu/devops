using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Project_BanSach.Tests
{
    public class CartTests
    {
        public class CartItem
        {
            public decimal Gia { get; set; }
            public int SoLuong { get; set; }
        }

        [Fact]
        public void TinhTongTienGioHang_HopLe()
        {
            // Arrange
            var gioHang = new List<CartItem>
            {
                new CartItem { Gia = 50000, SoLuong = 2 },
                new CartItem { Gia = 30000, SoLuong = 1 }
            };

            // Act
            var tongTien = gioHang.Sum(item => item.Gia * item.SoLuong);

            // Assert
            Assert.Equal(130000, tongTien);
        }
    }
}
