﻿using MyShopCommonLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.ApplicationBlocks.Data;

namespace MyShopAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        public OrderController()
        {

        }
        public SqlConnection GetConnection()
        {
            string conString = ConfigurationManager.ConnectionStrings["getConnection"].ToString();
            return new SqlConnection(conString);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("CreateOrder")]
        public CreateOrderresponse Saveaddress([FromBody] Order order)
        {
            CreateOrderresponse resp = new CreateOrderresponse();
            try
            {
                if (order.AddressId<= 0)
                {
                    resp.Message = "Address is mandatory";
                }
                else if (order.Items.Count == 0)
                {
                    resp.Message = "order should have atleast 1 item";
                }
                else if (order.UserId <= 0)
                {
                    resp.Message = "user id is mandatory";
                }
                else if (order.PaymentType == 0)
                {
                    resp.Message = "payment type is mandatory";
                }
                else
                {
                    // save
                    using (var conn = GetConnection())
                    {
                        SqlCommand com = new SqlCommand("Sp_CreateOrder", conn);
                        com.CommandType = CommandType.StoredProcedure;
                        
                        com.Parameters.AddWithValue("@UserId", order.UserId);
                        com.Parameters.AddWithValue("@AddressId", order.AddressId);
                        com.Parameters.AddWithValue("@OrderNo", $"OD{GenerateRefNo()}");
                        com.Parameters.AddWithValue("@OrderAmount", order.OrderAmount);
                        com.Parameters.AddWithValue("@PaidAmount", order.PaidAmount);
                        com.Parameters.AddWithValue("@Discount", order.Discount);
                        com.Parameters.AddWithValue("@ShippingCharge", order.ShippingCharge);
                        com.Parameters.AddWithValue("@Date", DateTime.Now);
                        com.Parameters.AddWithValue("@Time", DateTime.Now.ToString("HH:mm"));
                        com.Parameters.AddWithValue("@PaymentType", order.PaymentType);

                        com.Parameters.Add("@ReturnParamater", SqlDbType.Int, 4);
                        com.Parameters["@ReturnParamater"].Direction = ParameterDirection.Output;
                        com.Parameters.Add("@ReturnOrderNo", SqlDbType.NChar, 20);
                        com.Parameters["@ReturnOrderNo"].Direction = ParameterDirection.Output;

                        conn.Open();
                        int orderId = com.ExecuteNonQuery();
                        if (orderId > 0)
                        {
                            resp.OrderNo = com.Parameters["@ReturnOrderNo"].Value.ToString();
                        }
                        else
                        {
                            resp.Message = com.Parameters["@Message"].Value.ToString();
                        }
                        conn.Close();

                        if(orderId > 0)
                        {
                            foreach (CartItem item in order.Items)
                            {
                                com = new SqlCommand("Sp_SaveOrderItem", conn);
                                com.CommandType = CommandType.StoredProcedure;

                                com.Parameters.AddWithValue("@OrderId", orderId);
                                com.Parameters.AddWithValue("@ProductId", item.ProductId);
                                com.Parameters.AddWithValue("@Quantity", item.Quantity);

                                com.Parameters.Add("@ReturnParamater", SqlDbType.Int, 4);
                                com.Parameters["@ReturnParamater"].Direction = ParameterDirection.Output;
                               
                                conn.Open();
                                int itemResp = com.ExecuteNonQuery();
                                if (itemResp > 0)
                                {

                                }
                                else
                                {
                                    resp.Message = com.Parameters["@Message"].Value.ToString();
                                }
                                conn.Close();
                            }

                            
                        }

                        resp.OrderId = orderId;
                    }
                }
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
            }
            return resp;
        }
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("SaveShippingAddress")]
        //public SaveShippingAddressResponce SaveShippingAddress([FromBody] ShippingAddress shippingAddress)
        //{
        //    SaveShippingAddressResponce resp = new SaveShippingAddressResponce();
        //    try
        //    {
        //        if (shippingAddress.CountryId <= 0)
        //        {
        //            resp.Message = "country is mandatory";
        //        }
        //        else if (string.IsNullOrEmpty(shippingAddress.FullName))
        //        {
        //            resp.Message = "Full Name is mandatory";
        //        }
        //        else if (string.IsNullOrEmpty(shippingAddress.MobileNumber))
        //        {
        //            resp.Message = "mobile no is mandatory";
        //        }
        //        else if (string.IsNullOrEmpty(shippingAddress.PinCode))
        //        {
        //            resp.Message = "pincode is mandatory";
        //        }
        //        else if (string.IsNullOrEmpty(shippingAddress.Landmark))
        //        {
        //            resp.Message = "landmark is mandatory";
        //        }
        //        else if (string.IsNullOrEmpty(shippingAddress.CityName))
        //        {
        //            resp.Message = "city is mandatory";
        //        }
        //        else if (shippingAddress.StateId <= 0)
        //        {
        //            resp.Message = "state is mandatory";
        //        }
       
        //        else
        //        {
        //            // save
        //            using (var conn = GetConnection())
        //            {
        //                SqlCommand com = new SqlCommand("Sp_SaveShippingAddress", conn);
        //                com.CommandType = CommandType.StoredProcedure;

        //                com.Parameters.AddWithValue("@UserId", shippingAddress.UserId);
        //                com.Parameters.AddWithValue("@OrderId", shippingAddress.OrderId);
        //                com.Parameters.AddWithValue("@CountryId", shippingAddress.CountryId);
        //                com.Parameters.AddWithValue("@StateId", shippingAddress.StateId);
        //                com.Parameters.AddWithValue("@CityName", shippingAddress.CityName);
        //                com.Parameters.AddWithValue("@FullName", shippingAddress.FullName);
        //                com.Parameters.AddWithValue("@MobileNumber", shippingAddress.MobileNumber);
        //                com.Parameters.AddWithValue("@PINCode", shippingAddress.PinCode);
        //                com.Parameters.AddWithValue("@HouseNo", shippingAddress.HouseNo);
        //                com.Parameters.AddWithValue("@StreetNo", shippingAddress.StreetNo);
        //                com.Parameters.AddWithValue("@Area", shippingAddress.Area);
        //                com.Parameters.AddWithValue("@Landmark", shippingAddress.Landmark);
        //                com.Parameters.AddWithValue("@Description", shippingAddress.Description);
        //                com.Parameters.Add("@ReturnParamater", SqlDbType.Int, 4);
        //                com.Parameters["@ReturnParamater"].Direction = ParameterDirection.Output;
        //                com.Parameters.Add("@Message", SqlDbType.NVarChar, 100);
        //                com.Parameters["@Message"].Direction = ParameterDirection.Output;
        //                conn.Open();
        //                int res = com.ExecuteNonQuery();
        //                if (res > 0)
        //                {
        //                    resp.Message = com.Parameters["@Message"].Value.ToString();
        //                    resp.IsValid = true;
        //                }
        //                else
        //                {
        //                    resp.Message = com.Parameters["@Message"].Value.ToString();
        //                }
        //                conn.Close();
        //            }



        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resp.Message = ex.Message;
        //    }
        //    return resp;
        //}
       




        public string GenerateRefNo()
        {
            string refNo = "";
            Random ran = new Random();
            String b = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int length = 6;
            String random = "";
            for (int i = 0; i < length; i++)
            {
                int a = ran.Next(26);
                random = random + b.ElementAt(a);
            }

            int d = ran.Next(100000, 999999);

            refNo = $"{random}{d}";
            return refNo;
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetOrders")]
        public GetOrderResponse GetOrders(string userId)
        {
            // www.abc.com/api/Saddam/GetAddress?userId=1&mobileNumber=526352415623
            GetOrderResponse response = new GetOrderResponse();
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    response.Message = "User Id is mandatory";
                }
                else
                {
                    // save
                    using (var conn = GetConnection())
                    {
                        SqlCommand com = new SqlCommand("Sp_GetOrders", conn);
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@UserId", userId);

                        conn.Open();
                        SqlDataReader reader = com.ExecuteReader();
                        while (reader.Read())
                        {
                            response.Orders.Add(new OrderHistory
                            {   
                                Id = Convert.ToInt32(reader["Id"]), 
                                OrderNo= Convert.ToString(reader["OrderNo"]), 
                                Date=Convert.ToDateTime(reader["Date"]),
                                 OrderAmount=Convert.ToDecimal(reader["OrderAmount"])
                            });
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
      
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetOrdersDetails")]
        public GetOrderDetailsResponse GetOrdersDetails(int OrderId)
        {
            GetOrderDetailsResponse response = new GetOrderDetailsResponse();
            try
            {
                if (OrderId <= 0)
                {
                    response.Message = "Order Id is mandatory";
                }
                else
                {
                    using (var conn = GetConnection())
                    {
                        var sqlpr = new SqlParameter[1];
                        sqlpr[0] = new SqlParameter("@OrderId", OrderId);

                        DataSet data = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "Sp_GetOrderDetails", sqlpr);
                        var orderDetails = data.Tables[0];
                        var orderItems = data.Tables[1];
                        var orderAddress = data.Tables[2];
                        if (orderDetails.Rows.Count > 0)
                        {
                            response.Id = Convert.ToInt32(orderDetails.Rows[0]["Id"]);
                            response.OrderNo = Convert.ToString(orderDetails.Rows[0]["OrderNo"]);
                            response.Date = Convert.ToDateTime(orderDetails.Rows[0]["Date"]);
                            response.OrderAmount = Convert.ToDecimal(orderDetails.Rows[0]["OrderAmount"]);
                            response.PaidAmount = Convert.ToDecimal(orderDetails.Rows[0]["PaidAmount"]);
                            response.Discount = Convert.ToDecimal(orderDetails.Rows[0]["Discount"]);
                            response.ShippingCharge = Convert.ToDecimal(orderDetails.Rows[0]["ShippingCharge"]);
                            response.PaymentType = Convert.ToInt32(orderDetails.Rows[0]["PaymentType"]);
                        }
                        if (orderItems.Rows.Count > 0)
                        {
                            for (int i = 0; i < orderItems.Rows.Count; i++)
                            {
                                response.OrderItems.Add(new OrderItem
                                {
                                    Id = Convert.ToInt32(orderItems.Rows[i]["Id"]),
                                    MRP = Convert.ToDecimal(orderItems.Rows[i]["MRP"]),
                                    Name = Convert.ToString(orderItems.Rows[i]["Name"]),
                                    ProductId = Convert.ToInt32(orderItems.Rows[i]["ProductId"]),
                                    Quantity = Convert.ToInt32(orderItems.Rows[i]["Quantity"]),
                                    SellPrice = Convert.ToDecimal(orderItems.Rows[i]["SellPrice"]),
                                    ThumbnailURL = Convert.ToString(orderItems.Rows[i]["ThumbnailURL"])
                                });
                            }
                        }
                        if (orderAddress.Rows.Count > 0)
                        {
                            response.CountryId = Convert.ToInt32(orderAddress.Rows[0]["CountryId"]);
                            response.FullName = Convert.ToString(orderAddress.Rows[0]["FullName"]);
                            response.MobileNumber = Convert.ToString(orderAddress.Rows[0]["MobileNumber"]);
                            response.PinCode = Convert.ToString(orderAddress.Rows[0]["PINCode"]);
                            response.HouseNo = Convert.ToString(orderAddress.Rows[0]["HouseNo"]);
                            response.StreetNo = Convert.ToString(orderAddress.Rows[0]["StreetNo"]);
                            response.Area = Convert.ToString(orderAddress.Rows[0]["Area"]);
                            response.Landmark = Convert.ToString(orderAddress.Rows[0]["Landmark"]);
                            response.CityName = Convert.ToString(orderAddress.Rows[0]["CityName"]);
                            response.StateId = Convert.ToInt32(orderAddress.Rows[0]["StateId"]);
                            response.Description = Convert.ToString(orderAddress.Rows[0]["Description"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
