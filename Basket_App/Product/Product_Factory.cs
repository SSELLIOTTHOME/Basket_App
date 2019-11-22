using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket_App.Product
{
    public class Product_Factory : IProduct_Factory
    {
        public IProduct Create_Product_Stock_Item(string description, decimal price, List<Product_Category> categories)
        {
            if (categories == null) categories = new List<Product_Category>();
            return new Product_Stock_Item(description, price, categories);

        }


        public IProduct Create_Product_Voucher(string description, decimal price)
        {
          
            return new Product_Voucher(description, price);


        }
    }
}
