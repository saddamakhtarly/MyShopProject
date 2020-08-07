using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Models
{
    public class CartData
    {
        [PrimaryKey]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
