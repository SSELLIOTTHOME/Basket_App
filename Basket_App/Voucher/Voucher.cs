using Basket_App.Basket_Item;
using Basket_App.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket_App
{
    //Acceptable for simple applicaion such as this, but in reality this would be a lookup table from a  database
    public enum Product_Category
    {
        Head_Gear = 0, 
        Trousers = 1,
        Shoes = 2


    }

    public class GiftVoucher : IVoucher
    {

            

        
        public string Voucher_Code { get; set; }

        public decimal Discount { get; set; }
        public string Description { get; set; }

        

        public bool ApplyVoucher(IBasket basket)
        {
            basket.Apply_Voucher(this);
            return true;
        }

        public Voucher_Failure_Result? Calculate_Voucher_Discounts(ref decimal runningTotal, List<IBasket_Item> basket_Items)
        {   
            runningTotal = runningTotal - this.Discount;
            return null;
        }

        public GiftVoucher(string voucher_Code, decimal discount, string description)
        {
            
            this.Voucher_Code = voucher_Code;
            this.Discount = discount;
            this.Description = description;
         
            
        }



    }


    public class OfferVoucher : IVoucher
    {
        private List<Product_Category> Applies_To { get; set; }

        public string Voucher_Code { get; set; }

        public decimal Discount { get; set; }

        public string Description { get; set; }
        
        private decimal? Required_Spend { get; set; }

        public OfferVoucher(List<Product_Category> applies_To, string voucher_Code, decimal discount, string description, decimal? required_Spend)
        {
            this.Applies_To = applies_To;
            this.Voucher_Code = voucher_Code;
            this.Description = description;
            this.Discount = discount;
            this.Required_Spend = required_Spend;
        }

        public Voucher_Failure_Result? Calculate_Voucher_Discounts(ref decimal runningTotal, List<IBasket_Item> Basket_Items )
        {

                var originalTotal = Basket_Items.Sum(o => o.Price);

                List<IBasket_Item> basketItemsThatApplyToVoucher = null;
                //If the voucher has no applies to 
                if (this.Applies_To == null)
                {
                    //then it is all items
                    basketItemsThatApplyToVoucher = Basket_Items;
                }
                else
                {
                    //otherwise just those items with matching categories
                    basketItemsThatApplyToVoucher = Basket_Items.Where(o => ((Product_Stock_Item)o.Product).Categories.Intersect(this.Applies_To).Any()).ToList();
                }

                //restrict to include non vouchers                
                basketItemsThatApplyToVoucher = basketItemsThatApplyToVoucher.Where(o => o.Product is Product_Stock_Item).ToList();

                //Sum all of those items
                var sumofItemsThatApplyToVoucher = basketItemsThatApplyToVoucher.Sum(o => o.Price * o.Quantity);

                //calculate the discount to apply by looking at the lowest value, the lowest total spend or the max discount
                var discountToApply = Math.Min(sumofItemsThatApplyToVoucher, this.Discount);

                //If there is no discount to apply
                if (discountToApply == 0)
                {
                    //Return error
                    return  new Voucher_Failure_Result() { Reason = Voucher_Failure_Reason.Incorrect_Category, Voucher = this };
                }
                else
                {
                    //if the minimum spend is not met
                    if (this.Required_Spend == null || sumofItemsThatApplyToVoucher > this.Required_Spend)
                    {
                        //remove from running total
                        runningTotal = runningTotal - discountToApply;
                    }
                    else
                    {
                        //Return Error & calculate missing spend
                        
                        return new Voucher_Failure_Result() { Reason = Voucher_Failure_Reason.Insufficent_Spend, Voucher = this, Difference = (this.Required_Spend - sumofItemsThatApplyToVoucher) +(decimal)0.01 };
                    }
                }
            return null;
            }


        }

    }

