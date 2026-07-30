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
    public partial class LoginForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DA9BHPM\SQLEXPRESS;Initial Catalog=SmartStudyPlanner;Integrated Security=True");

        public static string LoggedInUser = "";
        public static int LoggedInUserID = 0;
       
        public LoginForm()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
        
            if (txtUser.Text == "" || txtPass.Text == "")
            {
                MessageBox.Show("Please enter username and password.");
                return;
            }

            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username=@username AND Password=@password", con);

                cmd.Parameters.AddWithValue("@username", txtUser.Text);
                cmd.Parameters.AddWithValue("@password", txtPass.Text);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    LoggedInUser = txtUser.Text;
                    LoggedInUserID = Convert.ToInt32(dr["UserID"]);
                    dr.Close();
                    con.Close();

                    frmDashboard f = new frmDashboard();
                    f.Show();
                    this.Hide();
                }
                else
                {
                    dr.Close();
                    con.Close();

                    MessageBox.Show("Invalid Username or Password!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                MessageBox.Show("Database Connected Successfully!");
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        
          
        {
            frmRegister f = new frmRegister();
            f.Show();
            this.Hide();
        }
    }
    }

