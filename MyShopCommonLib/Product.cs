using System;
using System.Collections.Generic;
using System.Text;

namespace MyShopCommonLib
{
    public class Product : Response
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal MRP { get; set; }
        public decimal SalePrice { get; set; }
        public string Description { get; set; }
        public string ThumbnailURL { get; set; }
    }
    public class ProductImage : Response
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageURL { get; set; }
    }
}
