using System.Collections.Generic;

namespace Basket_App.Voucher
{
    public interface IVoucher_Factory
    {
        IVoucher Create_Gift_Voucher(decimal discount, string code, string description);
        IVoucher Create_Offer_Voucher(List<Product_Category> Applies_To, decimal discount, string code, string description, decimal? minimumSpend);
    }
}