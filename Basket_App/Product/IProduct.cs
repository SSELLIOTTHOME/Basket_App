using System.Collections.Generic;

namespace Basket_App.Products
{
    public interface IProduct
    {
        string Description { get; set; }
        decimal Price { get; set; }

        //Assumes that a Product could exist in more than one category
        

       
    }
}