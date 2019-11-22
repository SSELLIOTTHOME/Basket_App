using System;
using System.Collections.Generic;
using Basket_App;
using Basket_App.Basket_Item;
using Basket_App.Product;
using Basket_App.Voucher;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Basket_App_Unit_Tests
{


    //Not this is an integration test, not a Unit Test
    [TestClass]
    public class Offer_Voucher_Unit_Tests
    {
        [TestMethod]
        public void Test_Offer_Voucher_With_Min_Spend_Fails()
        {

            OfferVoucher offer = new OfferVoucher(null, "test", 50, "test", 1000);
            Product_Stock_Item item = new Product_Stock_Item("Thing", 10, null);
            var mockItem1 = new Mock<IBasket_Item>();
            mockItem1.SetupGet(o => o.Product).Returns(item);
            //Testing for 5 items at £10 each
            mockItem1.SetupGet(o => o.Price).Returns(10);
            mockItem1.SetupGet(o => o.Quantity).Returns(1);
            decimal runningTotal = -1;
            var res = offer.Calculate_Voucher_Discounts(ref runningTotal, new List<IBasket_Item>() { mockItem1.Object });            
            Assert.IsTrue(res.HasValue && res.Value.Reason == Voucher_Failure_Reason.Insufficent_Spend && res.Value.Difference == (decimal)990.01 );

        }

        [TestMethod]
        public void Test_Offer_Voucher_With_Min_Spend_Works()
        {

            OfferVoucher offer = new OfferVoucher(null, "test", 50, "test", 1000);
            Product_Stock_Item item = new Product_Stock_Item("Thing", 10, null);
            var mockItem1 = new Mock<IBasket_Item>();
            mockItem1.SetupGet(o => o.Product).Returns(item);
            //Testing for 5 items at £10 each
            mockItem1.SetupGet(o => o.Price).Returns(2000);
            mockItem1.SetupGet(o => o.Quantity).Returns(1);
            decimal runningTotal = -1;
            var res = offer.Calculate_Voucher_Discounts(ref runningTotal, new List<IBasket_Item>() { mockItem1.Object });
            Assert.IsTrue(!res.HasValue);

        }

        [TestMethod]
        public void Test_Offer_Voucher_With_Category_Fails()
        {
            OfferVoucher offer = new OfferVoucher(new List<Product_Category>() { Product_Category.Shoes }, "test", 50, "test", 1000);
            Product_Stock_Item item = new Product_Stock_Item("Thing", 10, new List<Product_Category>() { Product_Category.Head_Gear });
            var mockItem1 = new Mock<IBasket_Item>();
            mockItem1.SetupGet(o => o.Product).Returns(item);
            //Testing for 5 items at £10 each
            mockItem1.SetupGet(o => o.Price).Returns(10);
            mockItem1.SetupGet(o => o.Quantity).Returns(1);
            decimal runningTotal = -1;
            var res = offer.Calculate_Voucher_Discounts(ref runningTotal, new List<IBasket_Item>() { mockItem1.Object });
            Assert.IsTrue(res.HasValue && res.Value.Reason == Voucher_Failure_Reason.Incorrect_Category);

        }

        [TestMethod]
        public void Test_Offer_Voucher_With_Category_Works()
        {
            OfferVoucher offer = new OfferVoucher(new List<Product_Category>() { Product_Category.Head_Gear }, "test", 50, "test", null);
            Product_Stock_Item item = new Product_Stock_Item("Thing", 10, new List<Product_Category>() { Product_Category.Head_Gear });
            var mockItem1 = new Mock<IBasket_Item>();
            mockItem1.SetupGet(o => o.Product).Returns(item);
            //Testing for 5 items at £10 each
            mockItem1.SetupGet(o => o.Price).Returns(10);
            mockItem1.SetupGet(o => o.Quantity).Returns(1);
            decimal runningTotal = -1;
            var res = offer.Calculate_Voucher_Discounts(ref runningTotal, new List<IBasket_Item>() { mockItem1.Object });
            Assert.IsTrue(!res.HasValue);

        }

        
     
    }
}
