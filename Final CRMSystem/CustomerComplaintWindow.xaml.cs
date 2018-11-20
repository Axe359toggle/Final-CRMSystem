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

namespace Final_CRMSystem
{
    /// <summary>
    /// Interaction logic for CustomerComplaintWindow.xaml
    /// </summary>
    public partial class CustomerComplaintWindow : Window
    {
        public CustomerComplaintWindow()
        {
            InitializeComponent();
            setCompID();
        }

        private string compID;
        private string refID;
        private string cusID;
        private string compMethod;
        private string compType;

        private void setCompID()
        {
            Database db = new Database();
            string query = "select case when MAX(comp_id) is null then '10000000' else MAX(comp_id) END as comp_id from Complaint";
            compID = db.ReadData(query,"comp_id");
            compID_txt.Text = compID;
        }
    }
}
