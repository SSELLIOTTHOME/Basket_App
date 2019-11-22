using Basket_App.Products;

namespace Basket_App.Basket_Items
{
    public interface IBasket_Item
    {
        
        decimal Price { get; }

        decimal Quantity { get; set; }
        IProduct Product { get;  }
    }
}