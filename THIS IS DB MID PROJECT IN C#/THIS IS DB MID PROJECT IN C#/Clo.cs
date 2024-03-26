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
    public partial class Clo : Form
    {
        SqlConnection connection = new SqlConnection("Data Source = DESKTOP-IJI0N4G\\SQLEXPRESS;Initial Catalog = NorthWind; Integrated Security = True; Encrypt = False;");
        TabControl tabcontrol2;
        int id;
        public Clo()
        {
            InitializeComponent();
            printCLO();
        }
      
        private void button1_Click(object sender, EventArgs e)
        {

            
          
                string constr = "Data Source= DESKTOP-IJI0N4G\\SQLEXPRESS;Initial Catalog=ProjectB;Integrated Security=True";
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into CLo values(@CLOName, GetDate(), GetDate())", con);
                cmd.Parameters.AddWithValue("@CLOName", textBox1.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully Inserted!");
                printCLO();
            

          

        }
        private void printCLO()
        {
            string connectionString = "Data Source=DESKTOP-IJI0N4G\\SQLEXPRESS;Initial Catalog=ProjectB;Integrated Security=True";
            string query = "SELECT * FROM CLo";

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
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home f3 = new Home();
            f3.TopLevel = false;
            
            panel1.Controls.Clear();
            panel1.Controls.Add(f3);
            f3.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            printCLO();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            string connectionString = "Data Source=DESKTOP-IJI0N4G\\SQLEXPRESS;Initial Catalog=ProjectB;Integrated Security=True";
            string deleteQuery = "DELETE FROM CLo WHERE id = @CLoId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@CloId", rowIndex);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        MessageBox.Show("Successfully Deleted!");
                        printCLO();

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
            int CLoID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value); // gets the id of selected row
            DateTime Date = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[2].Value); // gets the date from selected row

            string constr = "Data Source=DESKTOP-IJI0N4G\\SQLEXPRESS;Initial Catalog=ProjectB;Integrated Security=True";
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Update CLo SET Name=@Name,DateCreated = @dateCreated ,DateUpdated = GetDate() Where ID=@ID", con);
            cmd.Parameters.AddWithValue("@Name", textBox1.Text);
            cmd.Parameters.AddWithValue("@dateCreated", Date);
            cmd.Parameters.AddWithValue("@ID", CLoID);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("CLO updated successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            printCLO();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
