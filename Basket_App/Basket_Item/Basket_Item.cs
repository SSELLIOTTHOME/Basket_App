namespace Basket_App.Basket_Item
{
    //Represents an Item in the Order Basket
    public class Basket_Item : IBasket_Item
    {
        //Ordered Product
        public IProduct Product { get; }

        //Better to use IVoucher here rather than Offer Voucher - in future there may be other 
        //vouchers that require product specific functionality
        

        //Calculated Field - easier than calculating and maintaining
        
        public Basket_Item(IProduct product, decimal quantity)
        {
            this.Product = product; Quantity = quantity;
        }

        public decimal Quantity { get; set; }


        public decimal Price { get { return Product.Price * Quantity; } }
    }
}