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
    /// Interaction logic for Batch_Item_Complaint.xaml
    /// </summary>
    public partial class Batch_Item_Complaint : Window
    {
        public Batch_Item_Complaint()
        {
            InitializeComponent();
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            Login.b1.goBack(this);
        }
    }
}
