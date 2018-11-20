using System.Windows;

namespace Interfaces_new
{
    /// <summary>
    /// Interaction logic for HQ_insert_Dashboard.xaml
    /// </summary>
    public partial class HQ_insert_Dashboard : Window
    {
        public HQ_insert_Dashboard()
        {
            InitializeComponent();
        }

        private void btn_LoginDetails_Click(object sender, RoutedEventArgs e)
        {
            var ld = new Log_Details();
            ld.Show();
           
        }
    }
}

