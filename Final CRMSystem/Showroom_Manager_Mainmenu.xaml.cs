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

namespace Interfaces_new
{
    /// <summary>
    /// Interaction logic for Showroom_Manager_Dashboard.xaml
    /// </summary>
    public partial class Showroom_Manager_Mainmenu : Window
    {
        public Showroom_Manager_Mainmenu()
        {
            InitializeComponent();
        }

        private void btn_cusComplaint_Click_1(object sender, RoutedEventArgs e)
        {
            Customer_Complaint cc = new Customer_Complaint();
            cc.Show();
        }
    }
}
