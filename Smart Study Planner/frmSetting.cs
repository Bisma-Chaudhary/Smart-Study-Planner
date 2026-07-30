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
    public partial class frmSetting : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DA9BHPM\SQLEXPRESS;Initial Catalog=SmartStudyPlanner;Integrated Security=True");
        public frmSetting()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        
            if (txtCurrentPassword.Text == "" || txtNewPassword.Text == "" || txtConfirmPassword.Text == "")
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("New Password and Confirm Password do not match.");
                return;
            }

            con.Open();

            SqlCommand checkCmd = new SqlCommand(
                "SELECT COUNT(*) FROM Users WHERE UserID=@UserID AND Password=@Password", con);

            checkCmd.Parameters.AddWithValue("@UserID", LoginForm.LoggedInUserID);
            checkCmd.Parameters.AddWithValue("@Password", txtCurrentPassword.Text);

            int count = (int)checkCmd.ExecuteScalar();

            if (count == 0)
            {
                MessageBox.Show("Current Password is incorrect.");
                con.Close();
                return;
            }

            SqlCommand cmd = new SqlCommand(
                "UPDATE Users SET Password=@NewPassword WHERE UserID=@UserID", con);

            cmd.Parameters.AddWithValue("@NewPassword", txtNewPassword.Text);
            cmd.Parameters.AddWithValue("@UserID", LoginForm.LoggedInUserID);

            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Password changed successfully!");

            txtCurrentPassword.Clear();
            txtNewPassword.Clear();
            txtConfirmPassword.Clear();
        }
        

        private void btnReset_Click(object sender, EventArgs e)
        {
         
        
            txtCurrentPassword.Clear();
            txtNewPassword.Clear();
            txtConfirmPassword.Clear();

            txtCurrentPassword.Focus();
        }
        

        private void frmSetting_Load(object sender, EventArgs e)
        {
            txtUsername.Text = LoginForm.LoggedInUser;
            txtUsername.ReadOnly = true;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            frmDashboard d = new frmDashboard();
            d.Show();
            this.Hide();
        }
    }
}
