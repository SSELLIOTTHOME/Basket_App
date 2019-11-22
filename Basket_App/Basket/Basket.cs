using Basket_App.Basket_Item;
using Basket_App.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Basket_App
{

    public enum Voucher_Failure_Reason
    {
        Insufficent_Spend = 0, 
        Incorrect_Category = 1

    }


    public struct Voucher_Failure_Result
    {
        public IVoucher Voucher { get; set; }
        public Voucher_Failure_Reason Reason { get; set; }

        public decimal? Difference { get; set; }
    }        


    public class Basket : IBasket
    {
        public List<IBasket_Item> Basket_Items { get; set; }

        public List<IVoucher> Vouchers { get; set; }

        private OfferVoucher Offer_Voucher { get; set; }

        private decimal? Missing_Spend { get; set; }

        public Voucher_Failure_Result? Failed_Voucher { get; set; }

        public decimal Total { get; set; }

        private void CalculateBasket()
        {
            //Reset Message Variables

            Failed_Voucher = null;
            Missing_Spend = null;

            //First part of calculation is to sum all of the items and subtract general gift vouchers
            var runningTotal = Basket_Items.Where(o => o.Product is Product_Stock_Item).Sum(o => o.Product.Price );
            var originalTotal = runningTotal;

            Vouchers.Where<IVoucher>(o => o is GiftVoucher).ToList().ForEach(o => o.Calculate_Voucher_Discounts(ref runningTotal, this.Basket_Items));

            //Apply Offer Vouchers Discount
            if (this.Offer_Voucher != null)
            {
                Failed_Voucher = this.Offer_Voucher.Calculate_Voucher_Discounts(ref runningTotal, this.Basket_Items);
            }

            //Apply Offer Vouchers
            

            //apply gift vouchers at the end
            runningTotal = runningTotal + Basket_Items.Where(o => o.Product is Product_Voucher).Sum(o => o.Product.Price);
            
            //Deal with any negative values
            if (runningTotal < 0) runningTotal = 0;

            Total = runningTotal;
        }

        
       

        public Basket()
        {
            Basket_Items = new List<IBasket_Item>();
            Vouchers = new List<IVoucher>();
        }

        public bool Add_To_Basket(IProduct product, decimal Quantity)
        {

            Basket_Items.Add(new Basket_Item.Basket_Item(product, Quantity));

            return true;
        }


        public bool Apply_Voucher(IVoucher v)
        {
            if (Check_For_Duplicate_Voucher(v)) return false;
            this.Vouchers.Add(v);
            if (v is OfferVoucher) Offer_Voucher = (OfferVoucher)v;
            return true;
        }

        private bool Check_For_Duplicate_Voucher(IVoucher c)
        {
            if (Vouchers.Count(o => o.Voucher_Code == c.Voucher_Code) > 0)
            {                
                return true;

            }
            return false;

        }

        public List<String> Messages { get; set; }


        const string Total_String = "Total:{0}";
        
        const string Item_String = "{0} {1} @ {2} each";
        const string No_Voucher_String = "There are no products in your basket applicable to voucher Voucher {0}";
        const string Threashold_Message = "You have not reached the spend threshold for voucher {0}. Spend another {1} to receive {2} discount from your basket total";
        const string Offer_Voucher_Applied = "{0} x {1} Offer Voucher {2} {3} applied";
        const string Gift_Voucher_Applied =  "{0} x {1} Gift Voucher {2} Applied";




        public override string ToString()
        {
            //Refresh the calculation
            CalculateBasket();
            StringBuilder stringBuilder = new StringBuilder();

            //List Basket Items
            foreach (var item in this.Basket_Items)
            {
                stringBuilder.AppendLine(String.Format(Item_String, item.Quantity, item.Product.Description, item.Price.ToString("C")));
            }
            stringBuilder.AppendLine("-------");

            //List Vouchers Submitted
            Vouchers.ToList().ForEach(voucher =>
            {
                if (voucher is OfferVoucher)
                {

                    stringBuilder.AppendLine(String.Format(Offer_Voucher_Applied, "1", voucher.Discount.ToString("C"), voucher.Description, voucher.Voucher_Code));
                }
                else
                {
                    stringBuilder.AppendLine(String.Format(Gift_Voucher_Applied, "1", voucher.Discount.ToString("C"),   voucher.Voucher_Code));
                }
            }
            );

            stringBuilder.AppendLine("-------");

            stringBuilder.AppendLine(String.Format(Total_String, Total));
            stringBuilder.AppendLine("-------");

            if (Failed_Voucher != null)
            {
                var FailedVoucher = this.Failed_Voucher.Value;
                if (FailedVoucher.Reason == Voucher_Failure_Reason.Incorrect_Category) stringBuilder.AppendLine(String.Format(No_Voucher_String, FailedVoucher.Voucher.Voucher_Code));
                if (FailedVoucher.Reason == Voucher_Failure_Reason.Insufficent_Spend) stringBuilder.AppendLine(String.Format(Threashold_Message, FailedVoucher.Voucher.Voucher_Code, (FailedVoucher.Difference ?? 0).ToString("C"), FailedVoucher.Voucher.Discount.ToString("C")));

            };

            return stringBuilder.ToString();

        }


    }

}
