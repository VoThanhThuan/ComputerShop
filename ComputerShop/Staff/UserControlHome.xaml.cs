using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Dashboard.Staff
{
    /// <summary>
    /// Interaction logic for UserControlHome.xaml
    /// </summary>
    public partial class UserControlHome : UserControl
    {
        public UserControlHome()
        {
            InitializeComponent();
            var liveTime = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0)
            };
            liveTime.Tick += timer_Tick;
            liveTime.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            LiveTimeLabel.Content = $"{DateTime.Now:🕤hh:mm:ss tt}";
        }
    }
}
