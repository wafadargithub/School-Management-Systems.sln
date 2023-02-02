using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School_Management_System
{
    public partial class Attendance : Form
    {
        public Attendance()
        {
            InitializeComponent();
            DisplayAttendance();
            FillStudId();
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
            StIdCb.ValueMember = "StId";
            StIdCb.DataSource = dt;
            Con.Close();
        }
        private void GetStudName()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand(" select *from StudentTbl where StId = @SID", Con);
            cmd.Parameters.AddWithValue("@SID", StIdCb.SelectedValue.ToString());
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                StNameTb.Text = dr["StName"].ToString();
                StSectionTb.Text = dr["StSection"].ToString();
                ClassCb.SelectedItem = dr["StClass"].ToString();
            }
            Con.Close();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\SchoolDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayAttendance()
        {
            Con.Open();
            string Query = "Select * from AttendaceTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            var ds = new DataSet();
            sda.Fill(ds);
            AttendanceDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            AttStatusCb.SelectedIndex = -1;
            StNameTb.Text = "";
            StIdCb.SelectedIndex = -1;
            AttnTb.Text = "";
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
             if (AttnTb.Text=="" ||  AttStatusCb.SelectedIndex == -1)
            {
                MessageBox.Show("Please Insert Records");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into AttendaceTbl(AttStId,AttStName,AttStClass,AttStSection,AttName,AttDate,AttStatus) values (@StId,@StName,@SClass,@SSection,@AttName,@AttDate,@Status)", Con);
                    cmd.Parameters.AddWithValue("@StId", StIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@StName", StNameTb.Text);
                    cmd.Parameters.AddWithValue("@SClass", ClassCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SSection", StSectionTb.Text);
                    cmd.Parameters.AddWithValue("@AttName", AttnTb.Text);
                    cmd.Parameters.AddWithValue("@AttDate", AttDatePicker.Value.Date);
                    cmd.Parameters.AddWithValue("@Status", AttStatusCb.SelectedItem.ToString());

                    cmd.ExecuteNonQuery(); 
                    MessageBox.Show("Attendance Taken");
                    Con.Close();
                    DisplayAttendance();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetStudName();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }
        int key = 0;
        private void AttendanceDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            StIdCb.SelectedValue = AttendanceDGV.SelectedRows[0].Cells[1].Value.ToString();
            StNameTb.Text = AttendanceDGV.SelectedRows[0].Cells[2].Value.ToString();
            ClassCb.SelectedItem = AttendanceDGV.SelectedRows[0].Cells[3].Value.ToString();
            StSectionTb.Text = AttendanceDGV.SelectedRows[0].Cells[4].Value.ToString();
            AttnTb.Text = AttendanceDGV.SelectedRows[0].Cells[5].Value.ToString();
            AttDatePicker.Text = AttendanceDGV.SelectedRows[0].Cells[6].Value.ToString();
            AttStatusCb.SelectedItem = AttendanceDGV.SelectedRows[0].Cells[7].Value.ToString();

            if (StNameTb.Text == " ")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(AttendanceDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {

            if (AttnTb.Text == "" &&  AttStatusCb.SelectedIndex == -1 )
            {
                MessageBox.Show("Please Insert Records");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update AttendaceTbl set AttStId=@StId,AttStName=@StName,AttStClass=@SClass,AttStSection=@SSection,AttName=@AttName,AttDate =@ADate,AttStatus = @AStatus where AttNum=@ANum", Con);
                    cmd.Parameters.AddWithValue("@StId", StIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@StName", StNameTb.Text);
                    cmd.Parameters.AddWithValue("@SClass", ClassCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SSection", StSectionTb.Text);
                    cmd.Parameters.AddWithValue("@AttName", AttnTb.Text);
                    cmd.Parameters.AddWithValue("@ADate", AttDatePicker.Value.Date);
                    cmd.Parameters.AddWithValue("@AStatus", AttStatusCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@ANum", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" Attendance Record Updated Successfully");
                    Con.Close();
                    DisplayAttendance();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            MainMenu Obj = new MainMenu();
            Obj.Show();
            this.Hide();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Please Select A Student record");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from AttendaceTbl where AttNum=@ANum", Con);
                    cmd.Parameters.AddWithValue("@ANum", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" Student Record Deleted Sucessfully");
                    Con.Close();
                    DisplayAttendance();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);

                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void StIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
