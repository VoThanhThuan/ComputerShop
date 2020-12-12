using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dashboard.Common.ViewModel
{
    /// <summary>
    /// Interaction logic for Prompt.xaml
    /// </summary>
    public partial class PromptDialog : Window
    {
        public PromptDialog()
        {
            InitializeComponent();
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_OK_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
