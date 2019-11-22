using Basket_App.Basket_Items;
using Basket_App.Products;
using System.Collections.Generic;

namespace Basket_App.Baskets
{
    public interface IBasket
    {

        bool Add_To_Basket(IProduct product, decimal Quantity);

        bool Apply_Voucher(IVoucher v);
    }

}