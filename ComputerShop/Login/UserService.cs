using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Dashboard.Common;
using Dashboard.Data.EF;
using Dashboard.Data.Entities;

namespace Dashboard.Login
{
    public class UserService
    {

        public Result<string> Authenticate(LoginRequest request)
        {
            var userName = Controller._context.AppUsers.FirstOrDefault(x => x.Username == request.Username);
            if (userName == null) return new ResultError<string>("Tài khoản không tồn tại!");
            var password = PasswordHash(request.Password);
            if (password != userName.PasswordHash) return new ResultError<string>("Password không đúng");
            Controller.StartDashboard();
            return new ResultSuccess<string>();
        }

        public static string PasswordHash(string password)
        {
            var inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
            var hashBytes = MD5.Create().ComputeHash(inputBytes);

            // Encode the byte array using Base64 encoding
            return Convert.ToBase64String(hashBytes);
        }

    }
}
