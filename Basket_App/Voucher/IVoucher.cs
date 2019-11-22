using Basket_App.Basket_Item;
using System.Collections.Generic;

namespace Basket_App
{
    public interface IVoucher
    {
        string Voucher_Code { get; set; }        

        decimal Discount { get; set; }

        string Description { get; set; }

        Voucher_Failure_Result? Calculate_Voucher_Discounts(ref decimal runningTotal, List<IBasket_Item> Basket_Items);
    }
}