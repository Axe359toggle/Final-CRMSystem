using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Final_CRMSystem
{
    public class BackButton
    {


        static string[] previousWindows;
        static int noOfWindows = 0;

        public BackButton()
        {
            previousWindows = new string[10];
        }

        public BackButton(int maxNoOfWindows)
        {
            previousWindows = new string[maxNoOfWindows];
        }

        public int getNoOfWindows()
        {
            return noOfWindows;
        }

        public string getPreviousWindow()
        {
            string value;
            if (noOfWindows > 0)
            {
                value = previousWindows[noOfWindows - 1];
            }
            else
            {
                value = "Error";
            }
            return value;
        }

        public void goBack(Window Window1)
        {

            Window currentWindow = Window1;
            if (noOfWindows > 0)
            {
                Window Window2 = (Window)Activator.CreateInstance(Type.GetType(previousWindows[noOfWindows - 1]));
                previousWindows[noOfWindows] = null;
                noOfWindows--;
                Window2.Show();
                currentWindow.Hide();
            }
            else
            {
                MessageBox.Show("No available previous Windows");
            }

        }

        public void goBack()
        {
            if (noOfWindows > 0)
            {
                Window Window2 = (Window)Activator.CreateInstance(Type.GetType(previousWindows[noOfWindows - 1]));
                previousWindows[noOfWindows] = null;
                noOfWindows--;
                Window2.Show();

            }
            else
            {
                MessageBox.Show("No available Windows");
            }

        }

        public void addCurrentWindow(Window Window1)
        {
            previousWindows[noOfWindows] = Window1.GetType().FullName;
            noOfWindows++;
        }

        public void addWindowAndOpenNextWindow(Window Window1, Window Window2)
        {
            addCurrentWindow(Window1);
            Window2.Show();
            Window1.Hide();

        }
        ~BackButton() { }
    }


    class Person
    {
        string id;
        string fname;
        string nic;

        public Person(string x, string y, string z)
        {
            id = x;
            fname = y;
            nic = z;
        }

        public string getID()
        {
            return id;
        }
        public string getFname()
        {
            return fname;
        }
        public string getNic()
        {
            return nic;
        }
        ~Person() { }
    }
    class Database
    {
        private SqlConnection con;
        private SqlCommand cmd;
        public Database()
        {
            con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-99OKMBM;Initial Catalog=CRMdb;Integrated Security=True";
        }

        public void openCon()
        {
            con.Open();
        }
        public void closeCon()
        {
            con.Close();
        }

        public int Save_Del_Update(string query)
        {
            int rows;
            try
            {
                openCon();
            }
            catch (SqlException)
            {
                MessageBox.Show("Check the Database Connection", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cmd = new SqlCommand(query, con);
            rows = cmd.ExecuteNonQuery();
            cmd.Dispose();
            closeCon();
            return rows;
        }
        public DataTable GetData(string query)
        {
            try
            {
                openCon();
            }
            catch (SqlException)
            {
                MessageBox.Show("Check the Database Connection", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            closeCon();
            return dt;
        }

        public string ReadData(string query, string column)
        {
            string tb = "555";
            try
            {
                openCon();
            }
            catch (SqlException)
            {
                MessageBox.Show("Check the Database Connection", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                tb = Convert.ToString(dr[column]);
            }
            else
            {
                MessageBox.Show("Either returns multiple values or does not return a value");
            }
            closeCon();

            return tb;

        }

        public Person ReadData1(string query)
        {
            string x = "";
            string y = "";
            string z = "";

            try
            {
                openCon();
            }
            catch (SqlException)
            {
                MessageBox.Show("Check the Database Connection", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                x = Convert.ToString(dr["emp_id"]);
                y = Convert.ToString(dr["password"]);
                z = Convert.ToString(dr["desID"]);
            }
            else
            {
                MessageBox.Show("Either returns multiple values or does not return a value");
            }
            closeCon();
            Person p = new Person(x, y, z);
            return p;

        }
        ~Database() { }
    }

    static class Notifications
    {
        internal static void setNotification()
        {
            throw new NotImplementedException();
        }

        internal static void setNotification(string value, string type1, string type2, string des)
        {
            try
            {
                string query = " insert into Notifications (des_id,notification_type) values('" + des + "','" + type1 + "') declare @ID int = SCOPE_IDENTITY() insert into " + type1 + "_Notification values(@ID,'" + value + "','" + type2 + "') ";

                Database db = new Database();
                int rows = db.Save_Del_Update(query);
                MessageBox.Show(rows + " rows inserted");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Exception \n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

    }

    static class CRMdbData
    {
        internal static class Location
        {
            static class location_id
            {
                internal static string size = "5";
            }
            static class location_addr
            {
                internal static string size = "200";
            }
            static class location_type
            {
                internal static string size = "12";
            }
        }
        internal static class Complaint
        {
            static class comp_id
            {
                internal static string size = "8";
            }
            static class comp_type
            {
                internal static string size = "8";
            }
        }
        internal static class Customer
        {
            static class cus_id
            {
                internal static string size = "7";
            }
            static class cus_name
            {
                internal static string size = "50";
            }
            static class cus_tp
            {
                internal static string size = "12";
            }
            static class cus_email
            {
                internal static string size = "200";
            }
        }
        internal static class CustomerComplaint
        {
            static class comp_method
            {
                internal static string size = "9";
            }
            static class cus_comp_type
            {
                internal static string size = "5";
            }
        }
        internal static class Reference
        {
            static class ref_id
            {
                internal static string minsize = "8";
            }
        }

    }

}
