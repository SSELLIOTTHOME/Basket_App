using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket_App.Product
{
    public class Product_Stock_Item : Product
    {
        public Product_Stock_Item(string description, decimal price, List<Product_Category> categories) : base(description, price)
        {
            this.Categories = categories;
        }
                    
         public List<Product_Category> Categories { get; set; }


    }

}
