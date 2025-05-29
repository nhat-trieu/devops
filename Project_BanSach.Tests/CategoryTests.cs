using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Project_BanSach.Tests
{
    public class CategoryTests
    {
        // Class giả lập Category để phục vụ kiểm thử
        public class FakeCategory
        {
            public int Id { get; set; }
            public int? ParentId { get; set; }
        }

        [Fact]
        public void LocDanhMucTheoCha_ChiLayConDung()
        {
            // Arrange: tạo dữ liệu test
            var allCategories = new List<FakeCategory>
            {
                new FakeCategory { Id = 1, ParentId = null },
                new FakeCategory { Id = 2, ParentId = 1 },
                new FakeCategory { Id = 3, ParentId = 1 },
                new FakeCategory { Id = 4, ParentId = 2 }
            };

            // Act: lấy các danh mục con của Id = 1
            var catCon = allCategories.Where(c => c.ParentId == 1).ToList();

            // Assert: kiểm tra đúng kết quả
            Assert.Equal(2, catCon.Count);
            Assert.All(catCon, c => Assert.Equal(1, c.ParentId));
        }
    }
}
