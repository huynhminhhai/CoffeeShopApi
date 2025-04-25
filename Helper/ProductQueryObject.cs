using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopApi.Helper
{
    public class ProductQueryObject
    {
        public string? Name { get; set; } = null;
        public decimal? MinPrice { get; set; } = null;
        public decimal? MaxPrice { get; set; } = null;
        public int? CategoryId { get; set; } = null;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}