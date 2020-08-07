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

namespace MyShopAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        public UserController()
        {

        }

        public SqlConnection GetConnection()
        {
            string conString = ConfigurationManager.ConnectionStrings["getConnection"].ToString();
            return new SqlConnection(conString);
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("RegisterUser")]
        public RegisterResponse RegisterUser([FromBody] tblUser user)
        {
            RegisterResponse response = new RegisterResponse();
            try
            {
                if (string.IsNullOrEmpty(user.Email))
                {
                    response.Message = "Email is mandatory";
                }
                else if (string.IsNullOrEmpty(user.FullName))
                {
                    response.Message = "Full Name is mandatory";
                }
                else if (string.IsNullOrEmpty(user.Mobile))
                {
                    response.Message = "Mobile is mandatory";
                }
                else if (string.IsNullOrEmpty(user.Password))
                {
                    response.Message = "Password is mandatory";
                }
                else
                {
                    // save
                    using (var conn = GetConnection())
                    {
                        SqlCommand com = new SqlCommand("Sp_RegisterUser", conn);
                        com.CommandType = CommandType.StoredProcedure;

                        com.Parameters.AddWithValue("@Id", user.Id);
                        com.Parameters.AddWithValue("@FullName", user.FullName);
                        com.Parameters.AddWithValue("@Email", user.Email);
                        com.Parameters.AddWithValue("@Mobile", user.Mobile);
                        com.Parameters.AddWithValue("@Password", user.Password);
                        com.Parameters.Add("@ReturnParamater", SqlDbType.Int, 4);
                        com.Parameters["@ReturnParamater"].Direction = ParameterDirection.Output;

                        com.Parameters.Add("@Message", SqlDbType.NVarChar, 100);
                        com.Parameters["@Message"].Direction = ParameterDirection.Output;

                        conn.Open();
                        int resp = com.ExecuteNonQuery();
                        if (resp > 0)
                        {
                            response.Message = "User Registration Sucessfully";
                            response.IsValid = true;
                        }
                        else
                        {
                            response.Message = "User Registration Failed";
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

        [HttpPost]
        [System.Web.Http.Route("ValidateUser")]
        public LoginResponse ValidateUser([FromBody] LoginRequest rqst)
        {
            LoginResponse response = new LoginResponse();
            try
            {
                if (string.IsNullOrEmpty(rqst.Username))
                {
                    response.Message = "Username is mandatory";
                }
                else if (string.IsNullOrEmpty(rqst.Password))
                {
                    response.Message = "Password is mandatory";
                }
                else
                {
                    // save
                    using (var conn = GetConnection())
                    {
                        SqlCommand com = new SqlCommand("Sp_ValidateUser", conn);
                        com.CommandType = CommandType.StoredProcedure;

                        com.Parameters.AddWithValue("@Username", rqst.Username);
                        com.Parameters.AddWithValue("@Password", rqst.Password);

                        conn.Open();
                        SqlDataReader reader = com.ExecuteReader();
                        while (reader.Read())
                        {
                            response.Id = Convert.ToInt32(reader["Id"]);
                            response.FullName = Convert.ToString(reader["FullName"]);
                            response.Email = Convert.ToString(reader["Email"]);
                            response.Mobile = Convert.ToString(reader["Mobile"]);
                            response.Role = Convert.ToInt32(reader["Role"]);
                        }
                        if (response.Id > 0)
                        {
                            response.Message = "Login Success";
                            response.IsValid = true;
                        }
                        else
                        {
                            response.Message = "Login Failed,Invalid Credentials";
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
    }
}