using Shop.Core.Entities;
using Shop.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Data.Repositories
{
    public class ProductRepository:Repository<Product>, IProductRepository
    {
        public ProductRepository(ShopDbContext context):base(context)
        {

        }
    }
}
