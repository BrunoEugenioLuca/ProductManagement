using NUnit.Framework;
using ProductManagement.Dal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Tests.Dal
{
    public class ProductManagementContextTests
    {
        private ProductManagementContext _sut;

        public ProductManagementContextTests()
        {
            _sut = new ProductManagementContext();
        }

        [Test]
        public void Get_Product_By_Specific_Id()
        {
            var expected = _sut.Product.Where(x => x.Id == 1).ToList();
            Assert.IsTrue(expected.Any());
            Assert.IsTrue(string.Equals(expected?.First().ProductName,"oppo find x5"));
            Assert.IsTrue(expected?.First().Price == 700);
        }
    }
}
