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
    public partial class Events : Form
    {
        public Events()
        {
            InitializeComponent();
            DiplayEvents();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\SchoolDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DiplayEvents()
        {
            Con.Open(); 
            string Query = "Select * from EventsTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query , Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EventsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            EdurationTb.Text = "";
            EDescTb.Text = "";
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (EDescTb.Text == "" || EdurationTb.Text == "" )
            {
                MessageBox.Show("Please Insert Records");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EventsTbl(EDesc,EDate,EDuration) values (@EvDesc,@EvDate,@EvDur)", Con);
                    cmd.Parameters.AddWithValue("@EvDesc", EDescTb.Text);
                    cmd.Parameters.AddWithValue("@EvDate",EDate.Value.Date);
                    cmd.Parameters.AddWithValue("@EvDur", EdurationTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Inserted Successfully");
                    Con.Close();
                    DiplayEvents();
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

        private void BackBtn_Click(object sender, EventArgs e)
        {
            MainMenu Obj = new MainMenu();
            Obj.Show();
            this.Hide();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Please Select A Event record");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from EventsTbl where EId = @EKey", Con);
                    cmd.Parameters.AddWithValue("@EKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" Event Deleted Sucessfully");
                    Con.Close();
                    DiplayEvents();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);

                }

            }
        }
        int key = 0;
        private void EventsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EDescTb.Text = EventsDGV.SelectedRows[0].Cells[1].Value.ToString();
            EDate.Text = EventsDGV.SelectedRows[0].Cells[2].Value.ToString();
            EdurationTb.Text = EventsDGV.SelectedRows[0].Cells[3].Value.ToString();
            
            if (EDescTb.Text == " ")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(EventsDGV .SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (EDescTb.Text == "" || EdurationTb.Text == "" )
            {
                MessageBox.Show("Please Select A Records");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update EventsTbl set EDesc = @EvDesc,EDate = @EvDate,EDuration = @EvDuration where EId = @EvID", Con);
                    cmd.Parameters.AddWithValue("@EvDesc",EDescTb.Text);
                    cmd.Parameters.AddWithValue("@EvDate",EDate.Value.Date);
                    cmd.Parameters.AddWithValue("@EvDuration",EdurationTb.Text);
                    cmd.Parameters.AddWithValue("@EvID",key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" Events Record Updated Successfully");
                    Con.Close();
                    DiplayEvents();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }
    }
}
