using System.Collections.Generic;

namespace Shop.Api.Apps.AdminApi.DTOs
{
    public class ListDto<TItem>
    {
        public int TotalCount { get; set; }
        public List<TItem> Items { get; set; }
    }
}
