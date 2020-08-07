﻿using MyShopCommonLib;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MyShop.Models
{
    public class GlobalVariables
    {
        public static SQLiteConnection conn;
        public static LoginResponse user;
        public static string serviceURL = "http://192.168.43.117:8104/";
        public static string username = "";
        public static int user_role = 0;
        public static int user_id = 0;
    }
}
