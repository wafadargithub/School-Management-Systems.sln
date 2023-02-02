using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School_Management_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text =="" || PasswordTb.Text == "") 
            {
                MessageBox.Show("Please Enter UserName and Password");
            }
            else if(UnameTb.Text=="admin"&&PasswordTb.Text=="password")
            {
                MainMenu Obj = new MainMenu();
                Obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Enter Correct UserName and Password");
                UnameTb.Text = "";
                PasswordTb.Text = "";
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            UnameTb.Text = "";
            PasswordTb.Text = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UnameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void PasswordTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
