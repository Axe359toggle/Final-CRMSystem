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

using System.Data.SqlClient;

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
        private string compType1 = "Customer";
        private string compType2;
        private string relShrmID;

        string query;

        private void setCompID()
        {
            try
            {
                Database db = new Database();
                string query = "select case when MAX(comp_id) is null then '10000000' else MAX(comp_id) END as comp_id from Complaint";
                compID = db.ReadData(query, "comp_id");
                compID_txt.Text = (Int16.Parse(compID)+1).ToString() ;
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

        private void next_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (validate())
                {
                    compID = compID_txt.Text;
                    cusID = cusID_txt.Text.Trim();

                    if (byCall_rbn.IsChecked == true) { compMethod = "By Call"; }
                    else if (inPerson_rbn.IsChecked == true) { compMethod = "In Person"; }

                    if (staffComp_rbn.IsChecked == true) { compType2 = "Staff"; }
                    else if (itemComp_rbn.IsChecked == true) { compType2 = "Item"; }


                    Database db = new Database();

                    if (refID_txt.Text.Trim().Length == 0)
                    {
                        try
                        {
                            
                            string query1 = "INSERT INTO Reference DEFAULT VALUES DECLARE @ID int = SCOPE_IDENTITY() SELECT @ID as ref_id";
                            refID_txt.Text = refID = db.ReadData(query1, "ref_id");
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
                    else
                    {
                        refID = refID_txt.Text.Trim();
                    }


                    if(relShrmID_txt.Text.Trim().Length==0)
                    {
                        query = "INSERT INTO Complaint (comp_id,comp_type,ref_id) values('" + compID + "','" + compType1 + "','" + refID + "') INSERT INTO CustomerComplaint (comp_id,cus_id,comp_method,cus_comp_type) values('" + compID + "','" + cusID + "','"+compMethod+"','" + compType2 + "')";
                    }
                    else
                    {
                        relShrmID = relShrmID_txt.Text.Trim();
                        query = "INSERT INTO Complaint (comp_id,comp_type,ref_id) values('" + compID + "','" + compType1 + "','" + refID + "') INSERT INTO CustomerComplaint (comp_id,cus_id,comp_method,cus_comp_type,related_showroom) values('" + compID + "','" + cusID + "','"+compMethod+"','" + compType2 + "','"+ relShrmID +"')";
                    }

                    int rows = db.Save_Del_Update(query);

                    if ( rows > 0)
                    {
                        MessageBox.Show("Data inserted Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        if (staffComp_rbn.IsChecked == true)
                        {

                        }
                        else if (itemComp_rbn.IsChecked == true)
                        {

                        }
                        //open next window
                    }
                    
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

        private bool validate()
        {
            bool check = true;

            if(compID_txt.Text.Length != CRMdbData.Complaint.comp_id.size)
            {
                check = false;
                compIDNotify.Source = compIDNotify.TryFindResource("notifyErrorImage") as BitmapImage;
            }
            else
            {
                compIDNotify.Source = compIDNotify.TryFindResource("notifyCorrectImage") as BitmapImage;
            }

            if (!(refID_txt.Text.Trim().Length >= CRMdbData.Reference.ref_id.minsize || refID_txt.Text.Trim().Length == 0 ))
            {
                check = false;
                refIDNotify.Source = refIDNotify.TryFindResource("notifyErrorImage") as BitmapImage;
            }
            else
            {
                refIDNotify.Source = refIDNotify.TryFindResource("notifyCorrectImage") as BitmapImage;
            }

            if (cusID_txt.Text.Trim().Length != CRMdbData.Customer.cus_id.size)
            {
                check = false;
                cusIDNotify.Source = cusIDNotify.TryFindResource("notifyErrorImage") as BitmapImage;
            }
            else
            {
                cusIDNotify.Source = cusIDNotify.TryFindResource("notifyCorrectImage") as BitmapImage;
            }

            if (!(byCall_rbn.IsChecked==true||inPerson_rbn.IsChecked==true))
            {
                check = false;
                compMethodNotify.Source = compMethodNotify.TryFindResource("notifyErrorImage") as BitmapImage;
            }
            else
            {
                compMethodNotify.Source = compMethodNotify.TryFindResource("notifyCorrectImage") as BitmapImage;
            }

            if (!(staffComp_rbn.IsChecked == true || itemComp_rbn.IsChecked == true))
            {
                check = false;
                compTypeNotify.Source = compTypeNotify.TryFindResource("notifyErrorImage") as BitmapImage;
            }
            else
            {
                compTypeNotify.Source = compTypeNotify.TryFindResource("notifyCorrectImage") as BitmapImage;
            }

            if (!(relShrmID_txt.Text.Trim().Length == CRMdbData.Location.location_id.size || relShrmID_txt.Text.Trim().Length == 0))
            {
                check = false;
                relShrmIDNotify.Source = relShrmIDNotify.TryFindResource("notifyErrorImage") as BitmapImage;
            }
            else
            {
                relShrmIDNotify.Source = relShrmIDNotify.TryFindResource("notifyCorrectImage") as BitmapImage;
            }
                

            return check;
        }
    }
}
