using System;
using System.Collections.Generic;
using System.Text;

namespace MyShopCommonLib
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
    }
    public class SaveCartResponse:  Response
    {
        public int CartId { get; set; }
    }
    public class CartItem
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public decimal MRP { get; set; }
        public decimal SalePrice { get; set; }
        public string ThumbnailURL { get; set; }

    }
    public class GetCartResponse : Response
    {
        //public GetCartResponse()
        //{
        //    cartItems = new List<CartItem>();
        //}
        public List<CartItem> cartItems { get; set; } = new List<CartItem>();
    }
}
