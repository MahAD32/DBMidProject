using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;

namespace THIS_IS_DB_MID_PROJECT_IN_C_
{
    public partial class Students : Form
    {
        SqlConnection connection = new SqlConnection("Data Source = DESKTOP-IJI0N4G\\SQLEXPRESS;Initial Catalog = ProjectB; Integrated Security = True; Encrypt = False;");
        TabControl tabcontrol1;
        int id;
        public Students()
        {
            InitializeComponent();
            tabcontrol1 = new TabControl();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void GetStudentsRecord()
        {

            string connectionString = "Data Source=DESKTOP-IJI0N4G\\SQLEXPRESS;Initial Catalog=ProjectB;Integrated Security=True";
            string query = "SELECT * FROM student";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                StudentRecord.DataSource = dataTable;
                reader.Close();
                connection.Close();
            }
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            tabcontrol1.SelectedIndex = 0;
            if (isInfoValid())
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
            }
            if (!isInfoValid())
            {
                MessageBox.Show("Request Failed", " Please Enter Credentials Prperly", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (connection.State == System.Data.ConnectionState.Open)
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO Student VALUES(@Name,@Lastname,@Contactno,@Email,@Registrationno,@status)", connection))
                {
                    
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("@Name", StudentNametext.Text);
                    command.Parameters.AddWithValue("@Lastname", textBox1.Text);
                    command.Parameters.AddWithValue("@Contactno", Contactnotext.Text);
                    command.Parameters.AddWithValue("@Registrationno", Registrationnotext.Text);
                    command.Parameters.AddWithValue("@Email", emailtext.Text);
                    if(comboBox2.Text == "Active")
                    {
                        command.Parameters.AddWithValue("@status", 5);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@status", 6);
                    }
                    command.ExecuteNonQuery();
                    MessageBox.Show("Student integrated Successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetStudentsRecord();
                }
            }

            else
            {
                MessageBox.Show("Failed to open database connection.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
       
        }

       



        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
        
        private bool isInfoValid()
        {
            if (StudentNametext.Text == string.Empty || (!IsAlphabetOnly(StudentNametext.Text)))
            {
                return false;
            }
            if(Contactnotext.Text == string.Empty || (!IsNumberOnly(Contactnotext.Text)))
            {
                return false;
            }
            return true;
        }
        private bool IsNumberOnly(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        private bool IsAlphabetOnly(string input)
        {
            foreach(char c in input)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void panel1_Paint()
        {

        }
        private void StudentDataR(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(StudentRecord.SelectedRows[0].Cells[0].Value);
            StudentNametext.Text = StudentRecord.SelectedRows[0].Cells[1].Value.ToString();
            Registrationnotext.Text = StudentRecord.SelectedRows[0].Cells[2].Value.ToString();
            emailtext.Text = StudentRecord.SelectedRows[0].Cells[3].Value.ToString();
            Contactnotext.Text = StudentRecord.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int StudentID = Convert.ToInt32(StudentRecord.SelectedRows[0].Cells[0].Value); // gets the id of selected row

            string constr = "Data Source=DESKTOP-IJI0N4G\\SQLEXPRESS;Initial Catalog=ProjectB;Integrated Security=True";
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Update Student SET FirstName=@FirstName,LastName = @LastName , Contact =@Contact, Email = @Email, RegistrationNumber = @RegistrationNumber, Status = @Status Where ID=@ID", con);
            cmd.Parameters.AddWithValue("@FirstName", StudentNametext.Text);
            cmd.Parameters.AddWithValue("@LastName", textBox1.Text);
            cmd.Parameters.AddWithValue("@Contact", Contactnotext.Text);
            cmd.Parameters.AddWithValue("@Email", emailtext.Text);
            cmd.Parameters.AddWithValue("@RegistrationNumber", Registrationnotext.Text);
            if (comboBox2.Text == "Active")
            {
                cmd.Parameters.AddWithValue("@Status", 5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Status", 6);
            }
            cmd.Parameters.AddWithValue("@ID", StudentID);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Student updated successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            GetStudentsRecord();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Attendance f3 = new Attendance();
            f3.TopLevel = false;
            f3.FormBorderStyle = FormBorderStyle.None;
            f3.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(f3);
            f3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(StudentRecord.SelectedRows[0].Cells[0].Value);
            string connectionString = "Data Source=DESKTOP-IJI0N4G\\SQLEXPRESS;Initial Catalog=ProjectB;Integrated Security=True";
            string deleteQuery = "DELETE FROM student WHERE id = @StudentId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", rowIndex);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        MessageBox.Show("Successfully Deleted!");
                        GetStudentsRecord();
                        connection.Close();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }

        private void StudentNametext_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            GetStudentsRecord();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home f3 = new Home();
            f3.TopLevel = false;
            f3.FormBorderStyle = FormBorderStyle.None;
            f3.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(f3);
            f3.Show();
        }
        
        private void Contactnotext_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
   