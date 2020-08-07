using System;
using System.Collections.Generic;
using System.Text;

namespace MyShopCommonLib
{
    public class Order
    {
        public Order()
        {
            ShipmentAddress = new ShippingAddress();
            Items = new List<CartItem>();
        }
        public ShippingAddress ShipmentAddress { get; set; }
        public List<CartItem> Items { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal ShippingCharge { get; set; }
        public int UserId { get; set; }

        public int PaymentType { get; set; }//0=COD,1=Pay Now,2=Paypal
    }
    public class CreateOrderresponse : Response
    {
        public CreateOrderresponse()
        {
            OrderNo = "";
        }
        public string OrderNo { get; set; }
        public int OrderId { get; set; }
    }
    public class ShippingAddress
    {
        public int Id { get; set; }
        public int Country { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string PinCode { get; set; }
        public string HouseNo { get; set; }
        public string StreetNo { get; set; }
        public string Area { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        public int State { get; set; }
        public string Description { get; set; }

    }

    public class GetOrderResponse : Response
    {
        public List<OrderHistory> Orders { get; set; } = new List<OrderHistory>();
    }

    public class OrderHistory
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public DateTime Date { get; set; }
        public decimal OrderAmount { get; set; }
    }
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal MRP { get; set; }
        public decimal SellPrice { get; set; }
        public string ThumbnailURL { get; set; }
    }
    public class GetOrderDetailsResponse : Response
    {
        // order details
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public DateTime Date { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal ShippingCharge { get; set; }
        public int PaymentType { get; set; }
        //=== order items
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        //== shippingAddress
        public int Country { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string PinCode { get; set; }
        public string HouseNo { get; set; }
        public string StreetNo { get; set; }
        public string Area { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        public int State { get; set; }
        public string Description { get; set; }
    }

}
