using MyShop.Models;
using MyShopCommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyShop.DatabaseHandler
{
    public class DatabaseFunction
    {
        public int AddToCart(Product product, int qty = 1)
        {
            int resp = 0;
            string sql = $"SELECT * FROM CartData WHERE ProductId = {product.Id}";
            var existingProduct = GlobalVariables.conn.Query<CartData>(sql).FirstOrDefault();
            if (existingProduct == null)
            {
                CartData data = new CartData();
                data.ProductId = product.Id;
                data.Quantity = qty;

                resp = GlobalVariables.conn.Insert(data);
            }
            else
            {
                existingProduct.Quantity += qty;
                resp = GlobalVariables.conn.Update(existingProduct);
            }

            return resp;
        }

        public int UpdateCart(CartItem item)
        {
            int resp = 0;
            string sql = $"SELECT * FROM CartData WHERE ProductId = {item.ProductId}";
            var existingProduct = GlobalVariables.conn.Query<CartData>(sql).FirstOrDefault();
            if (item.Quantity <= 0)
            {
                // remove item from cart
                resp = GlobalVariables.conn.Delete(existingProduct);
            }
            else
            {
                CartData data = new CartData();
                data.ProductId = existingProduct.ProductId;
                data.Quantity = item.Quantity;
                resp = GlobalVariables.conn.Update(data);
            }
            return resp;
        }
    }
}
