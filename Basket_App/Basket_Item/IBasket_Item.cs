namespace Basket_App.Basket_Item
{
    public interface IBasket_Item
    {
        
        decimal Price { get; }

        decimal Quantity { get; set; }
        IProduct Product { get;  }
    }
}