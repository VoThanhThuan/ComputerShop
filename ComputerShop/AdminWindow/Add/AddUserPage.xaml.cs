using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Dashboard.Common;
using Dashboard.Common.ViewModel;
using Dashboard.Data.Entities;
using Dashboard.Login;
using Microsoft.Win32;
using Path = System.IO.Path;

namespace Dashboard.AdminWindow.Add
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>

    public partial class AddUserPage : Window
    {
        public AddUserPage(DataGrid dg, St status)
        {
            InitializeComponent();
            dtg_User = dg;
            if(status == St.Edit)
                SetValueUser();
            _dialog = status;
        }
        public enum St
        {
            Add,
            Edit,
            None
        }

        private DataGrid dtg_User;
        private St _dialog = St.None;
        private string _pathImage = null;

        private string SaveFile(string file)
        {
            var originalFileName = file;
            var fileName = $@"imageProduct/{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            if (!Directory.Exists("imageProduct"))
                Directory.CreateDirectory("imageProduct");
            File.Copy(_pathImage, fileName);
            return fileName;
        }

        private Result<string> AddUser()
        {
            var cID = Db.Context.AppUsers.FirstOrDefault(x => x.ID == tbx_ID.Text);
            if (cID != null)
                return new ResultError<string>("Trùng ID");
            var cUser = Db.Context.AppUsers.FirstOrDefault(x => x.Username == tbx_Username.Text);
            if (cUser != null)
                return new ResultError<string>("Tài khoản đã tồn tại");
            if (tbx_Password.Text != tbx_ConfirmPassword.Text)
                return new ResultError<string>("Mật khẩu xác thực không đúng");
            var user = new AppUser
            {
                ID = tbx_ID.Text,
                Email = tbx_Email.Text,
                Dob = dp_DoB.SelectedDate ?? DateTime.Today,
                FirstName = tbx_FirstName.Text,
                LastName = tbx_LastName.Text,
                Username = tbx_Username.Text,
                PhoneNumber = tbx_PhoneNumber.Text,
                Avatar = _pathImage != null ? this.SaveFile(_pathImage) : "",

            };
            if (tbx_Password.Text != tbx_ConfirmPassword.Text) return new ResultError<string>("xác thực password không chính xác");
            user.PasswordHash = UserService.PasswordHash(tbx_Password.Text);

            Db.Context.AppUsers.Add(user);

            var userRole = new AppUserRole();
            if (tbtn_IsAdmin.IsChecked == true)
            {
                userRole.UserID = tbx_ID.Text;
                userRole.RoleID = "admin";
            }
            else
            {
                userRole.UserID = tbx_ID.Text;
                userRole.RoleID = "staff";
            }
            Db.Context.AppUserRoles.Add(userRole);

            Db.Context.SaveChanges();

            return new ResultSuccess<string>();
        }

        private void SetValueUser()
        {
            var info = (AppUser)dtg_User.SelectedValue;
            tbx_ID.Text = info.ID;
            tbx_LastName.Text = info.LastName;
            tbx_FirstName.Text = info.FirstName;
            dp_DoB.Text = $"{info.Dob}";
            tbx_Email.Text = info.Email;
            tbx_PhoneNumber.Text = info.PhoneNumber;
            tbx_Username.Text = info.Username;

            var role = Db.Context.AppUserRoles.FirstOrDefault(x => x.UserID == info.ID);

            tbtn_IsAdmin.IsChecked = role != null && (role.RoleID == "admin" ? true : false);

        }

        private Result<string> EditUser()
        {
            var user = Db.Context.AppUsers.FirstOrDefault(x => x.Username == tbx_Username.Text);
            if (user == null)
                return new ResultError<string>("Không có tài khoản này");
            user.Email = tbx_Email.Text;
            user.Dob = dp_DoB.SelectedDate ?? DateTime.Today;
            user.FirstName = tbx_FirstName.Text;
            user.LastName = tbx_LastName.Text;
            user.Username = tbx_Username.Text;
            user.PhoneNumber = tbx_PhoneNumber.Text;
            if (!string.IsNullOrEmpty(tbx_Password.Text))
            {
                if (tbx_Password.Text != tbx_ConfirmPassword.Text) return new ResultError<string>("xác thực password không chính xác");
                user.PasswordHash = UserService.PasswordHash(tbx_Password.Text);

            }
            user.Avatar = _pathImage != null ? this.SaveFile(_pathImage) : _pathImage;

            var userRole = Db.Context.AppUserRoles.FirstOrDefault(x => x.UserID == user.ID);
            if (userRole != null)
            {
                if (tbtn_IsAdmin.IsChecked == true)
                {
                    userRole.UserID = tbx_ID.Text;
                    userRole.RoleID = "admin";
                }
                else
                {
                    userRole.UserID = tbx_ID.Text;
                    userRole.RoleID = "staff";
                }
            }

            Db.Context.SaveChanges();
            return new ResultSuccess<string>();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void btn_Do_Click(object sender, RoutedEventArgs e)
        {
            switch (_dialog)
            {
                case St.None:
                    return;
                case St.Add:
                    var add = AddUser();
                    if (add.IsSuccessed == false)
                    {
                        var mess = new MessageDialog()
                        {
                            tbl_Title = { Text = "LỖI" }
                            ,
                            tbl_Message = { Text = add.Message }
                        };
                        mess.ShowDialog();
                    }

                    break;
                case St.Edit:
                    var edit = EditUser();
                    if (edit.IsSuccessed == false)
                    {
                        if (edit.IsSuccessed == false)
                        {
                            var mess = new MessageDialog()
                            {
                                tbl_Title = { Text = "LỖI" }
                                ,
                                tbl_Message = { Text = edit.Message }
                            };
                            mess.ShowDialog();
                        }
                    }
                    break;
                default:
                    return;
            }

            this.DialogResult = true;
            _dialog = St.None;
            btn_Do.Content = "Do";
        }


        private void btn_OpenCalendar_Click(object sender, RoutedEventArgs e)
        {
            dp_DoB.IsDropDownOpen = true;

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GroupBox_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            // Note that you can have more than one file.
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            _pathImage = files[0];
            img_Product.Source = new BitmapImage(new Uri(files[0]));
        }

        private void GroupBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var opf = new OpenFileDialog();
            if (opf.ShowDialog() != true) return;
            _pathImage = opf.FileName;
            img_Product.Source = new BitmapImage(new Uri(_pathImage));
        }

    }

}
