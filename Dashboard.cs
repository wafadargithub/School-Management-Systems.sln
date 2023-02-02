using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace School_Management_System
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\SchoolDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MainMenu Obj = new MainMenu();
            Obj.Show();
            this.Hide();
        }
        private void CountStudent()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from StudentTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            StLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountTeachers()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from TeacherTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            TLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountEvents()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from EventsTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ELbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void SumFees()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Sum(Amt) from FeesTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            FeesLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {
            CountStudent();
            CountTeachers();
            CountEvents();
            SumFees();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
