using Basket_App.Basket_Item;
using System.Collections.Generic;

namespace Basket_App
{
    public interface IBasket
    {

        bool Add_To_Basket(IProduct product, decimal Quantity);

        bool Apply_Voucher(IVoucher v);
    }

}