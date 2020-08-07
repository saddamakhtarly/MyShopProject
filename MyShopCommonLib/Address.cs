using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShopCommonLib
{
    public class Address
    {
        public int Id { get; set; }
        public int UserId { get; set; }
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
        public bool IsDefault { get; set; }

    }
    public class GetAddressResponce : Response
    {
        public List<Address> Addresses { get; set; } = new List<Address>();
    }
    public class SaveAddressResponce:Response
    {
        public int AddressId { get; set; }
        public object Data { get; set; }
    }
}
