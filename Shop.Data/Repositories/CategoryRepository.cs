using Shop.Core.Entities;
using Shop.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Data.Repositories
{
    public class CategoryRepository:Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ShopDbContext context):base(context)
        {

        }

    }
}
