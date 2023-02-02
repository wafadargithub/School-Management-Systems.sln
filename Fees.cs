using System;
using System.Collections.Generic;//provides the type safety without derivation from a basic collection
using System.ComponentModel;//provides classes that are used to implement the run-time and design-time behavior of components and controls
using System.Data;//Represents a set of SQL commands and a database connection 
using System.Data.SqlClient;// contains the provider-specific ADO.NET objects and execute a command, and transfer information to and from a DataSet 
using System.Drawing;// provides methods for drawing to the display device
using System.Linq;// uniform query syntax in C# and VB.NET to retrieve data from different sources and formats
using System.Text;//contains classes representing various character encoding and conversions
using System.Threading.Tasks;//Provides types that simplify the work of writing concurrent and asynchronous code
using System.Windows.Forms;//Contains classes for creating Windows-based applications 
//The namespace keyword is used to declare a scope that contains a set of related objects
namespace School_Management_System
{
    public partial class Fees : Form
    {
        public Fees()
        {
            InitializeComponent();
            DisplayFees();
            FillStudId();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\SchoolDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayFees()
        {
            Con.Open();
            string Query = "Select * from FeesTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            FeesDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void FillStudId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select StId from StudentTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("StId", typeof(int));
            dt.Load(rdr);
            StdIdCb.ValueMember = "StId";
            StdIdCb.DataSource = dt;
            Con.Close();
        }
        private void GetStudName()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand(" select *from StudentTbl where StId = @SID", Con);
            cmd.Parameters.AddWithValue("@SID", StdIdCb.SelectedValue.ToString());
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                StNameTb.Text = dr["StName"].ToString();
            }
            Con.Close();
        }
        private void Reset()
        {
            AmountTb.Text = "";
            StNameTb.Text = "";
            StdIdCb.SelectedIndex = -1;
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (StNameTb.Text == "" || AmountTb.Text == "")
            {
                MessageBox.Show("Please Select A Record");
            }
            else
            {
               String paymentperiode;
                paymentperiode = PeriodDate.Value.Month.ToString() + "/"+PeriodDate.Value.Year.ToString();
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select COUNT (*) from Feestbl where StId ='"+StdIdCb.SelectedValue.ToString()+"'and Month ='"+paymentperiode.ToString()+"'",Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if(dt.Rows[0][0].ToString() == "1")
                {
                    MessageBox.Show("There is No Due For this Month");
                }
                else
                {
                    // Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into FeesTbl(StId,StName,Month,Amt) values (@SId,@SName,@SMonth,@SAmt)" , Con);
                    cmd.Parameters.AddWithValue("@SId", StdIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@SName", StNameTb.Text);
                    cmd.Parameters.AddWithValue("@SMonth", paymentperiode);
                    cmd.Parameters.AddWithValue("@SAmt", AmountTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Fees Successfully Paid");
                    
                }
                Con.Close();
                DisplayFees();
                Reset();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StdIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetStudName();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            MainMenu Obj = new MainMenu();
            Obj.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AmountTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
