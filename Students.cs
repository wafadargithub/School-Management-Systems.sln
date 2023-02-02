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
    public partial class Students : Form
    {
        public Students()
        {
            InitializeComponent();
            DisplayStudent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\SchoolDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayStudent()
        {
            Con.Open();
            string Query = "Select * from StudentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            var ds = new DataSet();
            sda.Fill(ds);
            StudentsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(StNameTb.Text ==  " " || StGenCb.SelectedIndex == -1 || ClassCb.SelectedIndex == -1 ||  StSectionTb.Text == " " ||   FeesTb.Text == " " || AddressTb.Text == " ")
            {
                MessageBox.Show("Please Insert Records");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into StudentTbl(StName,StGen,StDOB,StClass,StSection,StFees,StAdd) values (@Sname,@SGen,@SDob,@SClass,@SSection,@SFees,@SAdd)", Con);
                    cmd.Parameters.AddWithValue("@Sname", StNameTb.Text);
                    cmd.Parameters.AddWithValue("@SGen", StGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SDob", DOBPicker.Value.Date);
                    cmd.Parameters.AddWithValue("@SClass", ClassCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SSection", StSectionTb.Text);
                    cmd.Parameters.AddWithValue("@SFees", FeesTb.Text);
                    cmd.Parameters.AddWithValue("@SAdd", AddressTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Inserted Successfully");
                    Con.Close();
                    DisplayStudent();
                    Reset();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
               
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Reset()
        {
            key = 0;
            StNameTb.Text = " ";
            FeesTb.Text = " ";
            AddressTb.Text = " ";
            StGenCb.SelectedIndex = 0;
            ClassCb.SelectedIndex = 0;

        }
        int key = 0;
        private void StudentsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            StNameTb.Text = StudentsDGV.SelectedRows[0].Cells[1].Value.ToString();
            StGenCb.SelectedItem = StudentsDGV.SelectedRows[0].Cells[2].Value.ToString();
            DOBPicker.Text = StudentsDGV.SelectedRows[0].Cells[3].Value.ToString();
           ClassCb.SelectedItem = StudentsDGV.SelectedRows[0].Cells[4].Value.ToString();
            StSectionTb.Text = StudentsDGV.SelectedRows[0].Cells[5].Value.ToString();
            FeesTb.Text = StudentsDGV.SelectedRows[0].Cells[6].Value.ToString();
            AddressTb.Text = StudentsDGV.SelectedRows[0].Cells[7].Value.ToString();
            if(StNameTb.Text == " ")
            {
                key = 0;
            }
            else {
                key = Convert.ToInt32(StudentsDGV.SelectedRows[0].Cells[0].Value.ToString());
                 }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if(key == 0)
            {
                MessageBox.Show("Please Select A Student record");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from studentTbl where StId = @StKey" ,Con);
                    cmd.Parameters.AddWithValue("@StKey", key );
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" Student Record Deleted Sucessfully");
                    Con.Close();
                     DisplayStudent();
                    Reset();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);

                }
                   
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {

            if (StNameTb.Text == " " || FeesTb.Text == " " || StSectionTb.Text == " " || AddressTb.Text == " " || StGenCb.SelectedIndex == -1 || ClassCb.SelectedIndex == -1)
            {
                MessageBox.Show("Please Insert Records");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update StudentTbl set StName = @Sname,StGen = @SGen,StDOB = @SDob,StClass = @SClass,StSection = @SSection ,StFees = @SFees,StAdd = @SAdd where StId = @SID", Con);
                    cmd.Parameters.AddWithValue("@Sname", StNameTb.Text);
                    cmd.Parameters.AddWithValue("@SGen", StGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SDob", DOBPicker.Value.Date);
                    cmd.Parameters.AddWithValue("@SClass", ClassCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SSection", StSectionTb.Text);
                    cmd.Parameters.AddWithValue("@SFees", FeesTb.Text);
                    cmd.Parameters.AddWithValue("@SAdd", AddressTb.Text);
                    cmd.Parameters.AddWithValue("@SID", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Updated Successfully");
                    Con.Close();
                    DisplayStudent();
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
            MainMenu obj = new MainMenu();
            obj.Show();
            this.Hide();
        }

        private void FeesTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void SectionTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void Students_Load(object sender, EventArgs e)
        {

        }
    }
}
