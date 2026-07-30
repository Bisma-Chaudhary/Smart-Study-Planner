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
    public partial class frmSubjects : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DA9BHPM\SQLEXPRESS;Initial Catalog=SmartStudyPlanner;Integrated Security=True");
        int SubjectID = 0;
        public frmSubjects()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO Subjects (SubjectName, UserID) VALUES (@SubjectName,@UserID)", con);

            cmd.Parameters.AddWithValue("@SubjectName", txtSubject.Text);
            cmd.Parameters.AddWithValue("@UserID", LoginForm.LoggedInUserID);

            cmd.ExecuteNonQuery();

            con.Close();

            txtSubject.Clear();

            LoadSubjects();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Subjects SET SubjectName=@SubjectName  WHERE SubjectID=@SubjectID AND UserID=@UserID", con);

            cmd.Parameters.AddWithValue("@UserID", LoginForm.LoggedInUserID);
            cmd.Parameters.AddWithValue("@SubjectName", txtSubject.Text);
            cmd.Parameters.AddWithValue("@SubjectID", SubjectID);

            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Subject Updated Successfully!");

            txtSubject.Clear();

            LoadSubjects();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM Subjects WHERE SubjectID=@SubjectID AND UserID=@UserID", con);

            cmd.Parameters.AddWithValue("@UserID", LoginForm.LoggedInUserID);
            cmd.Parameters.AddWithValue("@SubjectID", SubjectID);

            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Subject Deleted Successfully!");

            txtSubject.Clear();

            LoadSubjects();
        }

        private void dgvSubjects_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSubject.Text = dgvSubjects.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            frmDashboard d = new frmDashboard();
            d.Show();
            this.Hide();
        }

        private void txtSubject_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadSubjects()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Subjects WHERE UserID = @UserID", con);
            da.SelectCommand.Parameters.AddWithValue("@UserID", LoginForm.LoggedInUserID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvSubjects.DataSource = dt;
            dgvSubjects.Columns["SubjectID"].Visible = false;
            dgvSubjects.Columns["UserID"].Visible = false;
        }

        private void frmSubjects_Load(object sender, EventArgs e)
        {
            LoadSubjects();
        }

        private void dgvSubjects_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SubjectID = Convert.ToInt32(dgvSubjects.Rows[e.RowIndex].Cells["SubjectID"].Value);
                txtSubject.Text = dgvSubjects.Rows[e.RowIndex].Cells["SubjectName"].Value.ToString();
            }
        }
    }
}
