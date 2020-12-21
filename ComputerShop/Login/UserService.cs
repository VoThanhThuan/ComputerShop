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

        public Result<InfoLogin> Authenticate(LoginRequest request)
        {
            var info = new InfoLogin();
            var userName = Controller._context.AppUsers.FirstOrDefault(x => x.Username == request.Username);
            if (userName == null) return new ResultError<InfoLogin>("Tài khoản không tồn tại!");
            var password = PasswordHash(request.Password);
            if (password != userName.PasswordHash) return new ResultError<InfoLogin>("Password không đúng");
            var role = Db.Context.AppUserRoles.FirstOrDefault(x => x.UserID == userName.ID);
            if(role == null) return new ResultError<InfoLogin>("không tồn tại quyền cho tài khoản này!");
            info = new InfoLogin(){RoleID = role.RoleID, NameStaff = $"{userName.LastName} {userName.FirstName}", ImagePath = userName.Avatar};
            return new ResultSuccess<InfoLogin>(info, "OK");
        }

        public static string PasswordHash(string password)
        {
            var inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
            var hashBytes = MD5.Create().ComputeHash(inputBytes);

            // Encode the byte array using Base64 encoding
            return Convert.ToBase64String(hashBytes);
        }

    }

    public class InfoLogin
    {
        public string RoleID { get; set; }
        public string NameStaff { get; set; }
        public string ImagePath { get; set; }
    }
}
