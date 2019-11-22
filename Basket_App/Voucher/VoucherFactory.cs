using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket_App.Voucher
{
    public class Voucher_Factory : IVoucher_Factory
    {
        public virtual IVoucher Create_Offer_Voucher(List<Products.Product_Category> Applies_To, decimal discount, string code, string description, decimal? minimumSpend)
        {
            OfferVoucher offer_Voucher = new OfferVoucher(Applies_To, code, discount, description, minimumSpend);
            return offer_Voucher;
        }

        public virtual IVoucher Create_Gift_Voucher(decimal discount, string code, string description)
        {
            GiftVoucher Gift_Voucher = new GiftVoucher(code, discount, description);
            return Gift_Voucher;

        }

    }
}
