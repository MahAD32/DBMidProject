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

namespace THIS_IS_DB_MID_PROJECT_IN_C_
{
    public partial class Attendance : Form
    {
        string constr = "Data Source= DESKTOP-IJI0N4G\\SQLEXPRESS;Initial Catalog=ProjectB;Integrated Security=True";

        public Attendance()
        {
            InitializeComponent();
            fillComboRegistration();
            data();

        }
        private void fillComboRegistration()
        {

            string query = "SELECT * FROM Student";

            using (SqlConnection connection = new SqlConnection(constr))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string reg = reader.GetString(5);
                    comboBox1.Items.Add(reg);

                }


                reader.Close();
                connection.Close();
            }
        }

        private void Student_attendance_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Student;", con);
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            con.Close();
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "RegistrationNumber";
            comboBox1.ValueMember = "ID";
        }
        private int getStatusNumber()
        {
            int Id = -1;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select lookupid from lookup where name = @name", con);
            cmd.Parameters.AddWithValue("@name", comboBox3.Text);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Id = Convert.ToInt32(reader["lookupId"]);
            }

            con.Close();

            return Id;
        }
     
        private void data() {
            using (SqlConnection c = new SqlConnection(constr))
            {
                SqlCommand s = new SqlCommand("SELECT name FROM Lookup where lookupId <=4", c);
                c.Open();
                SqlDataReader r = s.ExecuteReader();
                while (r.Read())
                {
                    string name = r.GetString(r.GetOrdinal("Name"));
                    comboBox3.Items.Add(name);
                }
                r.Close();
                c.Close();
            }
 }
        private void SaveAttendanceDate()
        {
            ADDDate();
            int dateId = FindDateID();
            int studentId = FindStudentId();
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentAttendance (AttendanceId, StudentId, AttendanceStatus) VALUES (@attendanceId, @studentId, @status)", con);
                if (studentId != -1)
                {
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                }
                else
                {
                    MessageBox.Show("Please select a student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (dateId != -1)
                {
                    cmd.Parameters.AddWithValue("@attendanceId", dateId);
                }
                else
                {
                    MessageBox.Show("Please select a valid date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int number = giveNumber();
                cmd.Parameters.AddWithValue("@status", number);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            MessageBox.Show("Attendance added successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ADDDate()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            if (!CheckDateExist())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO ClassAttendance (AttendanceDate) VALUES (@date)", con);
                cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(dateTimePicker1.Value));
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private int FindDateID()
        {
            int dateId = -1;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select id from ClassAttendance where AttendanceDate = @date", con);
            cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(dateTimePicker1.Value));
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                dateId = Convert.ToInt32(reader["id"]);
            }
            con.Close();
            return dateId;
        }

        private bool CheckDateExist()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select count(*)from ClassAttendance where AttendanceDate = @date", con);
            cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(dateTimePicker1.Value));
            int count = (int)(cmd.ExecuteScalar());
            con.Close();
            return count > 0;
        }

        private int FindStudentId()
        {
            int studentId = -1;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select id from Student where RegistrationNumber = @reg", con);
            cmd.Parameters.AddWithValue("@reg", comboBox1.Text);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                studentId = Convert.ToInt32(reader["id"]);
            }
            con.Close();
            return studentId;
        }

        private int giveNumber()
        {
            if (comboBox3.Text == "Present")
            {
                return 1;
            }
            else if (comboBox3.Text == "Absent")
            {
                return 2;
            }
            else if (comboBox3.Text == "Leave")
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }

        private void resetData()
        {
            comboBox1.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Please select attendance status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveAttendanceDate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home f3 = new Home();
            f3.TopLevel = false;
            f3.FormBorderStyle = FormBorderStyle.None;
            f3.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(f3);
            f3.Show();
        }
    }
}
