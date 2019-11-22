using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket_App.Products
{
    public abstract class Product : IProduct
    {
        public string Description { get; set; }

        public decimal Price { get; set; }


        public Product(string description, decimal price) : base()
        {

            this.Description = description;
            this.Price = price;
            
    }

     
    }


 



}
