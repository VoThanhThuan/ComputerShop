using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dashboard;
using Dashboard.Common;
using Dashboard.Data.Entities;
using Dashboard.Login;
using MaterialDesignThemes.Wpf;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.AdminWindow

{
    /// <summary>
    /// Interaction logic for UserController.xaml
    /// </summary>
    public partial class UserController : UserControl
    {
        public UserController()
        {
            InitializeComponent();
            LoadData();
        }

        private St _dialog = St.None;

        private void LoadData()
        {
            Db.Context.AppUsers.Load();
            var user = Db.Context.AppUsers.Local.ToObservableCollection();
            //var user = from u in Db.Context.AppUsers.Local
            //    select new {u.LastName, u.FirstName, u.UserName, u.Dob, u.Email, u.PhoneNumber};
            dtg_User.ItemsSource = user;
        }

        private Result<string> AddUser()
        {
            var cUser = Db.Context.AppUsers.FirstOrDefault(x => x.Username == tbx_Username.Text);
            if (cUser != null)
                return new ResultError<string>("Tài khoản đã tồn tại");
            if (tbx_Password.Text != tbx_ConfirmPassword.Text)
                return new ResultError<string>("Mật khẩu xác thực không đúng");
            var user = new AppUser
            {
                Email = tbx_Email.Text,
                Dob = dp_DoB.SelectedDate ?? DateTime.Today,
                FirstName = tbx_FirstName.Text,
                LastName = tbx_LastName.Text,
                Username = tbx_Username.Text,
                PhoneNumber = tbx_PhoneNumber.Text,
                PasswordHash = UserService.PasswordHash(tbx_Password.Text)
            };
            Db.Context.AppUsers.Add(user);
            return new ResultSuccess<string>();
        }

        private void SetValueUser()
        {
            var info = (AppUser)dtg_User.SelectedValue;
            tbx_LastName.Text = info.LastName;
            tbx_FirstName.Text = info.FirstName;
            dp_DoB.Text = $"{info.Dob}";
            tbx_Email.Text = info.Email;
            tbx_PhoneNumber.Text = info.PhoneNumber;
            tbx_Username.Text = info.Username;

            var role = Db.Context.AppUserRoles.FirstOrDefault(x => x.UserID == info.ID);

            tbtn_IsAdmin.IsChecked = role != null;

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
            user.PasswordHash = UserService.PasswordHash(tbx_Password.Text);

            Db.Context.SaveChanges();
            return new ResultSuccess<string>();
        }

        private Result<string> RemoveUser()
        {
            var id = ((AppUser)dtg_User.SelectedValue).ID;
            var user = Db.Context.AppUsers.Find(id);
            if (user == null)
            {
                return new ResultError<string>("Tài khoản không tồn tại");
            }
            Db.Context.AppUsers.Remove(user);
            return new ResultSuccess<string>("Xóa thành công");;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btn_AddUser_Click(object sender, RoutedEventArgs e)
        {
            dlh_Loading.IsOpen = true;
            _dialog = St.Add;
            btn_Do.Content = "Thêm";
        }


        private void Btn_EditUser_OnClick(object sender, RoutedEventArgs e)
        {
            if(dtg_User.SelectedIndex < 0)
                return;
            _dialog = St.Edit;
            
            SetValueUser();
            
            dlh_Loading.IsOpen = true;
            btn_Do.Content = "Sửa";
        }

        private void Btn_AddRemove_OnClick(object sender, RoutedEventArgs e)
        {
            var result = RemoveUser();
            if (!result.IsSuccessed)
                MessageBox.Show(result.Message);
            else
            {
                Db.Context.SaveChanges();
                MessageBox.Show(result.Message);
                LoadData();
            }
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
                        MessageBox.Show(add.Message);
                    break;
                case St.Edit:
                    var edit = EditUser();
                    if (edit.IsSuccessed == false)
                        MessageBox.Show(edit.Message);
                    break;
                default:
                    return;
            }
            _dialog = St.None;
            btn_Do.Content = "Do";
            dlh_Loading.IsOpen = false;
            LoadData();
        }


        private void btn_OpenCalendar_Click(object sender, RoutedEventArgs e)
        {
            dp_DoB.IsDropDownOpen = true;

        }
    }

    enum St
    {
        Add,
        Edit,
        None
    }
}
