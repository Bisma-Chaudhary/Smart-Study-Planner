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

namespace Smart_Study_Planner
{
    public partial class frmRegister : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DA9BHPM\SQLEXPRESS;Initial Catalog=SmartStudyPlanner;Integrated Security=True");
        bool showPassword = false;
        public frmRegister()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnShowPass_Click(object sender, EventArgs e)
        {
           
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
         
        
            if (txtUser.Text == "" || txtPass.Text == "" || txtConfirmPass.Text == "")
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            if (txtPass.Text != txtConfirmPass.Text)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            con.Open();

            // Username check
            SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username=@Username", con);
            checkCmd.Parameters.AddWithValue("@Username", txtUser.Text);

            int count = (int)checkCmd.ExecuteScalar();

            if (count > 0)
            {
                MessageBox.Show("Username already exists.");

                con.Close();
                return;
            }

            // Insert User
            SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username, Password) VALUES (@Username,@Password)", con);

            cmd.Parameters.AddWithValue("@Username", txtUser.Text);
            cmd.Parameters.AddWithValue("@Password", txtPass.Text);

            cmd.ExecuteNonQuery();
            LoginForm.LoggedInUser = txtUser.Text;
            SqlCommand getID = new SqlCommand(
    "SELECT UserID FROM Users WHERE Username=@Username", con);

            getID.Parameters.AddWithValue("@Username", txtUser.Text);

            LoginForm.LoggedInUserID = Convert.ToInt32(getID.ExecuteScalar());
            con.Close();

            MessageBox.Show("Registration Successful!");

            frmDashboard f = new frmDashboard();
            f.Show();
            this.Hide();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUser.Clear();
            txtPass.Clear();
            txtConfirmPass.Clear();

            txtUser.Focus();
        }

        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm f = new LoginForm();
            f.Show();
            this.Hide();
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = true;
            txtConfirmPass.UseSystemPasswordChar = true;
        }
    }
}
