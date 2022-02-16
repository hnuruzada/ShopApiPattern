namespace Shop.Api.Apps.AdminApi.DTOs.ProductDtos
{
    public class ProductListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public string Image { get; set; }
        public CategoryInProductListItemDto Category { get; set; }
    }
    public class CategoryInProductListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
