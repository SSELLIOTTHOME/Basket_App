using Basket_App.Voucher;
using System.Collections.Generic;

namespace Basket_App.Products
{
    public interface IProduct_Factory
    {
        IProduct Create_Product_Stock_Item(string description, decimal price, List<Product_Category> categories);

        IProduct Create_Product_Voucher(string description, decimal price);
        
    }
}