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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data.SqlClient;

namespace Final_CRMSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        internal static BackButton b1;

        public Login()
        {
            InitializeComponent();
        }

        private void login_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string uname = uname_txt.Text;
                string upass = Password.sha256(upass_txt.Password);
                string desID;

                Database db = new Database();
                string query = "SELECT des_id from Login where emp_username = '" + uname + "' and emp_pass='" + upass + "' ";
                desID = db.ReadData(query, "des_id");
                

                if (desID.Equals("H"))
                {
                    MessageBox.Show("Login Successful");
                    b1 = new BackButton();
                    b1.addWindowAndOpenNextWindow(this, new HQ_Manager_Dashboard());
                }
                else if (desID.Equals("S"))
                {
                    MessageBox.Show("Login Successful");
                    b1 = new BackButton();
                    b1.addWindowAndOpenNextWindow(this, new Showroom_Manager_Mainmenu());
                }
                else
                {
                    MessageBox.Show("Login Failed");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString(), "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

        }
    }
}
