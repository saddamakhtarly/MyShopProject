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
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string CityName { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string PinCode { get; set; }
        public string HouseNo { get; set; }
        public string StreetNo { get; set; }
        public string Area { get; set; }
        public string Landmark { get; set; }
       
        
        public bool IsDefault { get; set; }

    }
    public class GetAddressResponce : Response
    {
        public List<Address> Address { get; set; } = new List<Address>();
    }
    public class SaveAddressResponce:Response
    {
        public int AddressId { get; set; }
    }
}
