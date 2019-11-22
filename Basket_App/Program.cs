
using Basket_App.Product;
using Basket_App.Voucher;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket_App
{
    class Program
    {


        private static void Basket1()
        {
            Console.WriteLine("Basket 1");
            Console.WriteLine("");
            IProduct_Factory product_Factory = new Product_Factory();
            IProduct Hat = product_Factory.Create_Product_Stock_Item(description: "Hat", price:  (decimal)10.50, categories: null);
            IProduct Jumper = product_Factory.Create_Product_Stock_Item(description: "Jumper", price: (decimal)54.65, categories: null);
            IVoucher_Factory factory = new Voucher_Factory();
            IVoucher off = factory.Create_Gift_Voucher(discount:5, code:  "XXX-XXX",  description: "");
            IBasket Basket = new Basket();
            Basket.Add_To_Basket(Hat, 1);            
            Basket.Add_To_Basket(Jumper, 1); 
            Basket.Apply_Voucher(off);
            Console.WriteLine(Basket.ToString());
        }



        private static void Basket2()
        {
            Console.WriteLine("Basket 2");
            Console.WriteLine("");
            IProduct_Factory product_Factory = new Product_Factory();
            IProduct Hat = product_Factory.Create_Product_Stock_Item(description: "Hat",price:  (decimal)25, categories: null);
            IProduct Jumper = product_Factory.Create_Product_Stock_Item(description: "Jumper", price: (decimal)26, categories: null);
            IVoucher_Factory factory = new Voucher_Factory();
            IVoucher off = factory.Create_Offer_Voucher(Applies_To: new List<Product_Category>() { Product_Category.Head_Gear }, discount:5, code: "YYY-YYY", description: "£5.00 off Head Gear in baskets over £50.00", minimumSpend: 50);
            IBasket Basket = new Basket();
            Basket.Add_To_Basket(Hat, 1);
            Basket.Add_To_Basket(Jumper, 1);
            Basket.Apply_Voucher(off);
            Console.WriteLine(Basket.ToString());
        }

        private static void Basket3()
        {
            Console.WriteLine("Basket 3");
            Console.WriteLine("");
            Product_Factory Product_Factory = new Product_Factory();
            IProduct Hat = Product_Factory.Create_Product_Stock_Item(description: "Hat",price: (decimal)25, categories: null);
            IProduct Jumper = Product_Factory.Create_Product_Stock_Item(description: "Jumper", price: (decimal)26, categories: null);
            IProduct Head_Light = Product_Factory.Create_Product_Stock_Item(description: "Head Light", price: (decimal)3.50, categories: new List<Product_Category>() { Product_Category.Head_Gear });
            IVoucher_Factory Factory = new Voucher_Factory();
            IVoucher off = Factory.Create_Offer_Voucher(Applies_To: new List<Product_Category>() { Product_Category.Head_Gear }, discount: 5, code: "XXX-XXX", description: "£5.00 off Head Gear in baskets over £50.00", minimumSpend : null);
            IBasket basket = new Basket();
            basket.Add_To_Basket(Hat, 1);
            basket.Add_To_Basket(Jumper, 1);
            basket.Add_To_Basket(Head_Light, 1);
            basket.Apply_Voucher(off);
            Console.WriteLine(basket.ToString());
        }


        private static void Basket4()
        {
            Console.WriteLine("Basket 4");
            Console.WriteLine("");
            Product_Factory Product_Factory = new Product_Factory();
            IProduct Hat = Product_Factory.Create_Product_Stock_Item(description: "Hat", price: (decimal)25, categories: null);
            IProduct Jumper = Product_Factory.Create_Product_Stock_Item(description: "Jumper", price: (decimal)26, categories: null);

            IVoucher_Factory factory = new Voucher_Factory();
            IVoucher Gift_Voucher = factory.Create_Gift_Voucher(discount: 5, code: "YYY-YYY", description: "");
            IVoucher Offer_Voucher = factory.Create_Offer_Voucher(Applies_To: null , discount: 5, code: "XXX-XXX", description: "£5.00 off baskets over £50.00", minimumSpend: 50);

            IBasket Basket = new Basket();
            Basket.Add_To_Basket(product: Hat, Quantity: 1);
            Basket.Add_To_Basket(product: Jumper, Quantity: 1);
            
            Basket.Apply_Voucher(Offer_Voucher);
            Basket.Apply_Voucher(Gift_Voucher);
            Console.WriteLine(Basket.ToString());
        }


        private static void Basket5()
        {
            Console.WriteLine("Basket 5");
            Console.WriteLine("");
            Product_Factory product_Factory = new Product_Factory();
            IProduct Hat = product_Factory.Create_Product_Stock_Item(description: "Hat", price: (decimal)25, categories: null);
            IProduct Voucher = product_Factory.Create_Product_Voucher(description: "£30.00 Gift Voucher", price: (decimal)30);
            IVoucher_Factory factory = new Voucher_Factory();
            IVoucher off = factory.Create_Offer_Voucher(null, 5, "YYY-YYY", "£5.00 off baskets over £50.00", 50);
            IBasket Basket = new Basket();
            Basket.Add_To_Basket(product: Hat, Quantity: 1);
            Basket.Add_To_Basket(product: Voucher, Quantity: 1);
            Basket.Apply_Voucher(off);
            
            Console.WriteLine(Basket.ToString());
        }





        static void Main(string[] args)
        {
            Basket1();
            Basket2();
            Basket3();
            Basket4();
            Basket5();
            Console.ReadKey();
        }
    }
}
