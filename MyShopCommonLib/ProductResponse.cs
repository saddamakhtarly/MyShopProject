using System;
using System.Collections.Generic;
using System.Text;

namespace MyShopCommonLib
{
    public class ProductResponse : Response
    {
        public int ProductId { get; set; }
    }
    public class ProductImageSaveResponse : Response
    {

    }
    public class GetProductResponse : Response
    {
        public GetProductResponse()
        {
            Products = new List<Product>();
        }
        public List<Product> Products { get; set; }
    }
    public class GetProductImageResponse : Response
    {
        
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }
}
