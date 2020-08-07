using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Models
{
   public class ApiProductResponce
    {
        public int condition { get; set; }
        public string ageLimit { get; set; }
        public int couponType { get; set; }
        public List<object> s_Imp { get; set; }
        public double s_Rating { get; set; }
        public int id { get; set; }
        public DateTime createdAt { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string productType { get; set; }
        public string status { get; set; }
        public string icon1 { get; set; }
        public string icon2 { get; set; }
        public int workerId { get; set; }
        public string imageUrl { get; set; }
    }
}
