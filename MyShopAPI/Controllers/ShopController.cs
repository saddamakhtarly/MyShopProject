using MyShopCommonLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MyShopAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/Shop")]
    public class ShopController : ApiController
    {
        public ShopController()
        {

        }
        public SqlConnection GetConnection()
        {
            string conString = ConfigurationManager.ConnectionStrings["getConnection"].ToString();
            return new SqlConnection(conString);
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAddress")]
        public GetAddressResponce GetAddress(string userId)
        {
            // www.abc.com/api/Saddam/GetAddress?userId=1&mobileNumber=526352415623
            GetAddressResponce response = new GetAddressResponce();
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
                        SqlCommand com = new SqlCommand("SpGetAddress", conn);
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@UserId", userId);

                        conn.Open();
                        SqlDataReader reader = com.ExecuteReader();
                        while (reader.Read())
                        {
                            response.Address.Add(new Address
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                CountryId = Convert.ToInt32(reader["CountryId"]),
                                StateId = Convert.ToInt32(reader["StateId"]),
                                CityName = Convert.ToString(reader["CityName"]),
                                FullName = Convert.ToString(reader["FullName"]),
                                MobileNumber = Convert.ToString(reader["MobileNumber"]),
                                PinCode = Convert.ToString(reader["PINCode"]),
                                HouseNo = Convert.ToString(reader["HouseNo"]),
                                StreetNo = Convert.ToString(reader["StreetNo"]),
                                Area = Convert.ToString(reader["Area"]),
                                Landmark = Convert.ToString(reader["Landmark"]),
                                IsDefault = Convert.ToBoolean(reader["IsDefault"])
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
        [System.Web.Http.Route("GetCategories")]
        public CategoryResponse GetCategories()
        {
            CategoryResponse response = new CategoryResponse();
            try
            {
                using (var conn = GetConnection())
                {
                    SqlCommand com = new SqlCommand("Sp_GetCategories", conn);
                    // SqlCommand com = new SqlCommand("SELECT * FROM tblCategory", conn);
                    // com.CommandType = CommandType.Text;
                    com.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        response.CategoryList.Add(new Category
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = Convert.ToString(reader["Name"])
                        });
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("SaveCategory")]
        public SaveCategoryResponse SaveCategory([FromBody] Category category)
        {
            // www.abc.com/api/Saddam/Saveaddress
            SaveCategoryResponse resp = new SaveCategoryResponse();
            try
            {
                if (string.IsNullOrEmpty(category.Name))
                {
                    resp.Message = "Category Name is mandatory";
                }
                else
                {
                    // save
                    using (var conn = GetConnection())
                    {
                        SqlCommand com = new SqlCommand("Sp_SaveCategory", conn);
                        com.CommandType = CommandType.StoredProcedure;

                        com.Parameters.AddWithValue("@Name", category.Name);

                        com.Parameters.Add("@ReturnParamater", SqlDbType.Int, 4);
                        com.Parameters["@ReturnParamater"].Direction = ParameterDirection.Output;

                        com.Parameters.Add("@Message", SqlDbType.NVarChar, 100);
                        com.Parameters["@Message"].Direction = ParameterDirection.Output;

                        conn.Open();
                        int res = com.ExecuteNonQuery();
                        if (res > 0)
                        {
                            resp.Message = com.Parameters["@Message"].Value.ToString();
                            resp.IsValid = true;
                        }
                        else
                        {
                            resp.Message = com.Parameters["@Message"].Value.ToString();
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
            }
            return resp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("SaveProduct")]
        public ProductResponse SaveProduct([FromBody] Product product)
        {
            ProductResponse response = new ProductResponse();
            try
            {
                if (string.IsNullOrEmpty(product.Name))
                {
                    response.Message = "Name is mandatory";
                }
                else if ((product.CategoryId) == 0)
                {
                    response.Message = "Category id is mandatory";
                }
                else if ((product.MRP) <= 0)
                {
                    response.Message = "MRP is mandatory";
                }
                else if ((product.SalePrice) <= 0)
                {
                    response.Message = "Saleprice is mandatory";
                }
                else
                {
                    // save
                    using (var conn = GetConnection())
                    {
                        SqlCommand com = new SqlCommand("Sp_SaveProduct", conn);
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@Id", product.Id);
                        com.Parameters.AddWithValue("@Name", product.Name);
                        com.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                        com.Parameters.AddWithValue("@MRP", product.MRP);
                        com.Parameters.AddWithValue("@SellPrice", product.SalePrice);
                        com.Parameters.AddWithValue("@Description", product.Description);
                        com.Parameters.Add("@ReturnParamater", SqlDbType.Int, 4);
                        com.Parameters["@ReturnParamater"].Direction = ParameterDirection.Output;

                        com.Parameters.Add("@Message", SqlDbType.NVarChar, 100);
                        com.Parameters["@Message"].Direction = ParameterDirection.Output;
                        conn.Open();
                        int resp = com.ExecuteNonQuery();
                        if (resp > 0)
                        {
                            response.Message = com.Parameters["@Message"].Value.ToString();
                            response.IsValid = true;
                            response.ProductId = Convert.ToInt32(com.Parameters["@ReturnParamater"].Value);
                        }
                        else
                        {
                            response.Message = com.Parameters["@Message"].Value.ToString();
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

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("SaveProductImages")]
        public ProductImageSaveResponse SaveProductImages(int productId)
        {
            ProductImageSaveResponse resp = new ProductImageSaveResponse();
            try
            {
                List<ProductImage> images = new List<ProductImage>();
                HttpRequest httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        HttpPostedFile postedFile = httpRequest.Files[file];
                        string reativePath = $"ProductImages/{postedFile.FileName}";
                        var filePath = HttpContext.Current.Server.MapPath($"~/{reativePath}");
                        postedFile.SaveAs(filePath);
                        images.Add(new ProductImage { ImageURL = reativePath, ProductId = productId });
                    }

                    if (images.Count > 0)
                    {
                        foreach (ProductImage image in images)
                        {
                            using (var conn = GetConnection())
                            {
                                SqlCommand com = new SqlCommand("Sp_SaveProductImage", conn);
                                com.CommandType = CommandType.StoredProcedure;
                                com.Parameters.AddWithValue("@Id", 0);
                                com.Parameters.AddWithValue("@ProductId", image.ProductId);
                                com.Parameters.AddWithValue("@ImageURL", image.ImageURL);

                                com.Parameters.Add("@ReturnParamater", SqlDbType.Int, 4);
                                com.Parameters["@ReturnParamater"].Direction = ParameterDirection.Output;

                                com.Parameters.Add("@Message", SqlDbType.NVarChar, 100);
                                com.Parameters["@Message"].Direction = ParameterDirection.Output;

                                conn.Open();
                                var retId = com.ExecuteNonQuery();
                                if (retId > 0)
                                {
                                    resp.Message += $"{image.ImageURL} : Image saved Success";
                                }
                                else
                                {
                                    resp.Message += $"{image.ImageURL} : Image saved Failed";
                                }
                                conn.Close();
                            }
                        }
                    }
                    resp.IsValid = true;
                }
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
            }
            return resp;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetProducts")]
        public GetProductResponse GetProducts(int categoryId = 0)
        {
            GetProductResponse response = new GetProductResponse();
            try
            {
                using (var conn = GetConnection())
                {
                    SqlCommand com = new SqlCommand("Sp_GetProducts", conn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@CategoryId", categoryId);

                    conn.Open();
                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        response.Products.Add(new Product
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = Convert.ToString(reader["Name"]),
                            CategoryId = Convert.ToInt32(reader["CategoryId"]),
                            Description = Convert.ToString(reader["Description"]),
                            MRP = Convert.ToDecimal(reader["MRP"]),
                            SalePrice = Convert.ToDecimal(reader["SellPrice"]),
                            ThumbnailURL = Convert.ToString(reader["ThumbnailURL"])
                        });
                    }

                    response.Message = $"{response.Products.Count} products found";
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetProductImage")]
        public GetProductImageResponse GetProductImage(int productId)
        {
            GetProductImageResponse response = new GetProductImageResponse();
            try
            {
                using (var conn = GetConnection())
                {
                    SqlCommand com = new SqlCommand("Sp_GetProductImage", conn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ProductId", productId);

                    conn.Open();
                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        response.ProductImages.Add(new ProductImage
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                             ProductId= Convert.ToInt32(reader["ProductId"]),
                              ImageURL= Convert.ToString(reader["ImageURL"])
                        });
                    }

                    response.Message = $"{response.ProductImages.Count} products found";
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        [Route("SearchProduct")]
        [HttpGet]
        public GetProductResponse SearchProduct(string keyword)
        {

            GetProductResponse response = new GetProductResponse();
            try
            {
                using (var conn = GetConnection())
                {
                    SqlCommand com = new SqlCommand("Sp_GetSearchProducts", conn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Keyword", keyword);

                    conn.Open();
                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        response.Products.Add(new Product
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = Convert.ToString(reader["Name"]),
                            CategoryId = Convert.ToInt32(reader["CategoryId"]),
                            Description = Convert.ToString(reader["Description"]),
                            MRP = Convert.ToDecimal(reader["MRP"]),
                            SalePrice = Convert.ToDecimal(reader["SellPrice"]),
                            ThumbnailURL = Convert.ToString(reader["ThumbnailURL"])

                        });
                    }

                    response.Message = $"{response.Products.Count} products found";
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("SaveCart")]
        public SaveCartResponse SaveCart(Cart cart)
        {
            SaveCartResponse response = new SaveCartResponse();
            try
            {
                if (cart.UserId == 0)
                {
                    response.Message = "User Id is mandatory";
                }
                else if (cart.ProductId == 0)
                {
                    response.Message = "Product id is mandatory";
                }
                else if (cart.Quantity == 0)
                {
                    response.Message = "Quantity should be grater than 0";
                }
                else
                {
                    // save
                    using (var conn = GetConnection())
                    {
                        SqlCommand com = new SqlCommand("Sp_SaveCart", conn);
                        com.CommandType = CommandType.StoredProcedure;

                        com.Parameters.AddWithValue("@Id", cart.Id);
                        com.Parameters.AddWithValue("@ProductId", cart.ProductId);
                        com.Parameters.AddWithValue("@UserId", cart.UserId);
                        com.Parameters.AddWithValue("@Quantity", cart.Quantity);
                        com.Parameters.Add("@ReturnParamater", SqlDbType.Int, 4);
                        com.Parameters["@ReturnParamater"].Direction = ParameterDirection.Output;

                        com.Parameters.Add("@Message", SqlDbType.NVarChar, 100);
                        com.Parameters["@Message"].Direction = ParameterDirection.Output;
                        conn.Open();
                        int resp = com.ExecuteNonQuery();
                        if (resp > 0)
                        {
                            response.Message = com.Parameters["@Message"].Value.ToString();
                            response.IsValid = true;
                            response.CartId = Convert.ToInt32(com.Parameters["@ReturnParamater"].Value);
                        }
                        else
                        {
                            response.Message = com.Parameters["@Message"].Value.ToString();
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
        [System.Web.Http.Route("GetCart")]
        public GetCartResponse GetCart(int UserId)
        {
            GetCartResponse response = new GetCartResponse();
            try
            {
                using (var conn = GetConnection())
                {
                    SqlCommand com = new SqlCommand("Sp_GetCart", conn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@UserId", UserId);

                    conn.Open();
                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        response.cartItems.Add(new CartItem
                        {
                            CartId = Convert.ToInt32(reader["CartId"]),
                            Name = Convert.ToString(reader["Name"]),
                            ProductId = Convert.ToInt32(reader["ProductId"]),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            MRP = Convert.ToDecimal(reader["MRP"]),
                            SalePrice = Convert.ToDecimal(reader["SellPrice"]),
                            ThumbnailURL = Convert.ToString(reader["ThumbnailURL"])
                        });
                    }

                    response.Message = $"{response.cartItems.Count} items found";
                    conn.Close();
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
