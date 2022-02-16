using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Core.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
