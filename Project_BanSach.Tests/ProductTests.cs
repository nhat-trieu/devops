using Xunit;

namespace Project_BanSach.Tests
{
    public class ProductTests
    {
        [Fact]
        public void TinhGiaSauKhiGiam_GiamPhanTramVaTien_ChoKetQuaDung()
        {
            // Arrange
            decimal giaGoc = 100000;
            decimal giamPhanTram = 10; // 10%
            decimal giamTien = 5000;

            // Act
            var giaSauPhanTram = giaGoc - (giaGoc * giamPhanTram / 100);
            var giaSauGiam = giaSauPhanTram - giamTien;

            // Assert
            Assert.Equal(85000, giaSauGiam);
        }

        [Fact]
        public void TimKiemSanPham_ChuaTuKhoa_ChoKetQua()
        {
            // Arrange
            var ds = new[] {
                new { Ten = "Ngữ Văn 12" },
                new { Ten = "Toán 11" },
                new { Ten = "Tiếng Anh 12" }
            };

            // Act
            var ketQua = ds.Where(sp => sp.Ten.Contains("12")).ToList();

            // Assert
            Assert.Equal(2, ketQua.Count);
        }
    }
}
