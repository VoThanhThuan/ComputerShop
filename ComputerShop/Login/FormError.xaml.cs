using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DesignLogin
{
    /// <summary>
    /// Lógica de interacción para FormError.xaml
    /// </summary>
    public partial class FormError : Window
    {
        public FormError()
        {
            InitializeComponent();
        }


        private void btn_Close_Checked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
