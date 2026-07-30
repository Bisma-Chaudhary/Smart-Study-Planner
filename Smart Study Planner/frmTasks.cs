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
    public partial class frmTasks : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DA9BHPM\SQLEXPRESS;Initial Catalog=SmartStudyPlanner;Integrated Security=True");

        int TaskID = 0;
        public frmTasks()
        {
            InitializeComponent();

        }
        private void LoadSubjects()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT SubjectID, SubjectName FROM Subjects WHERE UserID=@UserID", con);
            da.SelectCommand.Parameters.AddWithValue("@UserID", LoginForm.LoggedInUserID);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cmbSubject.DataSource = dt;
            cmbSubject.DisplayMember = "SubjectName";
            cmbSubject.ValueMember = "SubjectID";
        }
        private void LoadTasks()
        {
            SqlDataAdapter da = new SqlDataAdapter(
        @"SELECT Tasks.TaskID,
                 Tasks.TaskName,
                 Subjects.SubjectName,
                 Tasks.Priority,
                 Tasks.Deadline,
                 Tasks.Status
          FROM Tasks
          INNER JOIN Subjects
              ON Tasks.SubjectID = Subjects.SubjectID
          WHERE Tasks.UserID = @UserID", con);

            da.SelectCommand.Parameters.AddWithValue("@UserID", LoginForm.LoggedInUserID);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvTasks.DataSource = dt;
            dgvTasks.Columns["TaskID"].Visible = false;
        }
        private void frmTasks_Load(object sender, EventArgs e)
        {
            cmbPriority.Items.Add("High");
            cmbPriority.Items.Add("Medium");
            cmbPriority.Items.Add("Low");
            cmbStatus.Items.Add("Pending");
            cmbStatus.Items.Add("Completed");
            LoadSubjects();
            LoadTasks();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtTask.Text.Trim() == "")
            {
                MessageBox.Show("Enter Task.");
                return;
            }

            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO Tasks(TaskName,SubjectID,Priority,Deadline, Status, UserID) VALUES(@TaskName,@SubjectID,@Priority,@Deadline, @Status, @UserID)", con);

            cmd.Parameters.AddWithValue("@TaskName", txtTask.Text);
            cmd.Parameters.AddWithValue("@SubjectID", cmbSubject.SelectedValue);
            cmd.Parameters.AddWithValue("@Priority", cmbPriority.Text);
            cmd.Parameters.AddWithValue("@Deadline", dtpDeadline.Value.Date);
            cmd.Parameters.AddWithValue("@Status", cmbStatus.Text);

            cmd.Parameters.AddWithValue("@UserID", LoginForm.LoggedInUserID);
            cmd.ExecuteNonQuery();

            con.Close();

            txtTask.Clear();
            cmbPriority.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;

            LoadTasks();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Tasks SET TaskName=@TaskName, SubjectID=@SubjectID, Priority=@Priority, Deadline=@Deadline, Status=@Status WHERE TaskID=@TaskID AND UserID=@UserID", con);

          
            cmd.Parameters.AddWithValue("@TaskName", txtTask.Text);
            cmd.Parameters.AddWithValue("@SubjectID", cmbSubject.SelectedValue);
            cmd.Parameters.AddWithValue("@Priority", cmbPriority.Text);
            cmd.Parameters.AddWithValue("@Deadline", dtpDeadline.Value.Date);
            cmd.Parameters.AddWithValue("@TaskID", TaskID);
            cmd.Parameters.AddWithValue("@UserID", LoginForm.LoggedInUserID);
            cmd.Parameters.AddWithValue("@Status", cmbStatus.Text);
            cmd.ExecuteNonQuery();

            con.Close();

            txtTask.Clear();
            cmbPriority.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
            TaskID = 0;

            LoadTasks();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (TaskID == 0)
            {
                MessageBox.Show("Please select a task first.");
                return;
            }

            con.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM Tasks WHERE TaskID=@TaskID AND UserID=@UserID", con);

            cmd.Parameters.AddWithValue("@TaskID", TaskID);
            cmd.Parameters.AddWithValue("@UserID", LoginForm.LoggedInUserID);
            cmd.ExecuteNonQuery();

            con.Close();

            txtTask.Clear();
            cmbPriority.SelectedIndex = -1;
            TaskID = 0;

            LoadTasks();
        }

        private void dgvTasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTask.Text = dgvTasks.CurrentRow.Cells[0].Value.ToString();
            cmbSubject.Text = dgvTasks.CurrentRow.Cells[1].Value.ToString();
            cmbPriority.Text = dgvTasks.CurrentRow.Cells[2].Value.ToString();
            dtpDeadline.Text = dgvTasks.CurrentRow.Cells[3].Value.ToString();
            
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            frmDashboard d = new frmDashboard();
            d.Show();
            this.Hide();
        }

        private void txtTask_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvTasks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                TaskID = Convert.ToInt32(dgvTasks.Rows[e.RowIndex].Cells["TaskID"].Value);

                txtTask.Text = dgvTasks.Rows[e.RowIndex].Cells["TaskName"].Value.ToString();

                cmbSubject.Text = dgvTasks.Rows[e.RowIndex].Cells["SubjectName"].Value.ToString();

                cmbPriority.Text = dgvTasks.Rows[e.RowIndex].Cells["Priority"].Value.ToString();

                dtpDeadline.Value = Convert.ToDateTime(dgvTasks.Rows[e.RowIndex].Cells["Deadline"].Value);

                cmbStatus.Text = dgvTasks.Rows[e.RowIndex].Cells["Status"].Value.ToString();
            }
        }
    }
    
}
