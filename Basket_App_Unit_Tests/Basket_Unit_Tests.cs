using System;
using System.Collections.Generic;
using Basket_App;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Basket_App.Unit_Tests
{
    [TestClass]
    public class BasketUnitTests
    {


        /* Ensure adding a valid voucher adds to the voucher list */
        [TestMethod]
        public void Test_Inserting_Voucher_Adds_To_Voucher_List()
        {
            Basket b = new Basket();
            var mock = new Mock<IVoucher>();
            mock.SetupSet(o => o.Voucher_Code = "xxxx-xxxx");
            b.Apply_Voucher(mock.Object);
            Assert.IsTrue(b.Vouchers.Count > 0);
            
        }

        /* Ensure adding a Valid Product to the basket works*/
        [TestMethod]
        public void Test_Inserting_Product_Adds_To_Basket()
        {
            Basket b = new Basket();
            var mock = new Mock<IProduct>();
            mock.SetupGet(o => o.Description).Returns("Trousers");
            mock.SetupSet(o => o.Price = (decimal)4.99);
            b.Add_To_Basket(mock.Object,1);
            Assert.IsTrue(b.Basket_Items.Count > 0);
            
        }

        /* Ensure the same voucher cannot be used more than once */
        [TestMethod]
        public void Test_No_Duplicate_Codes_Allowed()
        {
            Basket b = new Basket();
            var mock = new Mock<IVoucher>();
            mock.SetupGet(o => o.Voucher_Code).Returns("xxxx-xxxx");
            var result = b.Apply_Voucher(mock.Object);
            string expectedResult = String.Format("Duplicate Voucher - {0}", "xxxx-xxxx");

             result = b.Apply_Voucher(mock.Object);
            Assert.IsTrue(!result);
            


        }

    }


  

}
