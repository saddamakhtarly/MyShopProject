using System;

namespace MyShopCommonLib
{
    public class tblUser
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
    public class Response
    {
        public bool IsValid { get; set; }
        public string Message { get; set; } = "";
    }

    public class RegisterResponse : Response
    {

    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class LoginResponse : Response
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Mobile { get; set; } = "";
        public string Email { get; set; } = "";
        public int Role { get; set; } = 0;
    }
}
