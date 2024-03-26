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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            printAssessment();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string constr = "Data Source= DESKTOP-IJI0N4G\\SQLEXPRESS;Initial Catalog=ProjectB;Integrated Security=True";
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Assessment values(@CLOName, GetDate(),@TotalMarks,@TotalWeightage)", con);
            cmd.Parameters.AddWithValue("@CLOName", textBox1.Text);
            cmd.Parameters.AddWithValue("@TotalMarks", textBox2.Text);
            cmd.Parameters.AddWithValue("@TotalWeightage", textBox3.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully Inserted!");
            printAssessment();
        }
        private void printAssessment()
        {
            string connectionString = "Data Source=DESKTOP-IJI0N4G\\SQLEXPRESS;Initial Catalog=ProjectB;Integrated Security=True";
            string query = "SELECT * FROM Assessment";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                dataGridView1.DataSource = dataTable;
                reader.Close();
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            string connectionString = "Data Source=DESKTOP-IJI0N4G\\SQLEXPRESS;Initial Catalog=ProjectB;Integrated Security=True";
            string deleteQuery = "DELETE FROM Assessment WHERE id = @AssessmentId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@AssessmentId", rowIndex);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        MessageBox.Show("Successfully Deleted!");
                        printAssessment();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime Date = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[2].Value); // gets the date from selected row
            int StudentID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value); // gets the id of selected row

            string constr = "Data Source=DESKTOP-IJI0N4G\\SQLEXPRESS;Initial Catalog=ProjectB;Integrated Security=True";
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Update Assessment SET Title=@Name,DateCreated = @dateCreated ,TotalMarks = @TotalMarks,TotalWeightage = @TotalWeightage Where ID=@ID", con);
            cmd.Parameters.AddWithValue("@Name", textBox1.Text);
            cmd.Parameters.AddWithValue("@dateCreated", Date);
            cmd.Parameters.AddWithValue("@TotalMarks", textBox2.Text);
            cmd.Parameters.AddWithValue("@TotalWeightage", textBox3.Text);
            cmd.Parameters.AddWithValue("@ID", StudentID);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("CLO updated successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            printAssessment();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            AssessmentComponent f3 = new AssessmentComponent();
            f3.TopLevel = false;
            f3.FormBorderStyle = FormBorderStyle.None;
            f3.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(f3);
            f3.Show();
        }

        private void button5_Click(object sender, EventArgs e)
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
