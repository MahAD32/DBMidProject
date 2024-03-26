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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace THIS_IS_DB_MID_PROJECT_IN_C_
{
    public partial class RubericsLevel : Form
    {
        string constr = "Data Source= DESKTOP-IJI0N4G\\SQLEXPRESS;Initial Catalog=ProjectB;Integrated Security=True";

        public RubericsLevel()
        {

            InitializeComponent();
            printRubricLevel();
            fillComboRId();
        }
        private void printRubricLevel()
        {

            string query = "SELECT * FROM rubriclevel";

            using (SqlConnection connection = new SqlConnection(constr))
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
        private void RubricLevel_Load(object sender, EventArgs e)
        {
            printRubricLevel();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                if (!validateDetails(textBox2.Text))
                { return; }
                if (!validateRIdCombo())
                { return; }
                if (!validateLevelCombo())
                { return; }

                addRubricLevel();


            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            string deleteQuery = "DELETE FROM rUBRIClEVEL WHERE id = @Id";
            using (SqlConnection connection = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", rowIndex);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Successfully Deleted!");

                        printRubricLevel();

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
            if (!validateDetails(textBox2.Text))
            { return; }
            if (!validateRIdCombo())
            { return; }
            if (!validateLevelCombo())
            { return; }

            modifyRubricLevel();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private bool validateRIdCombo()
        {
            if (comboBox2.SelectedItem == null)
            {
                
                errorProvider1.SetError(comboBox2, "It cannot be empty.");
                return false;
            }
            else
            {
                errorProvider1.SetError(comboBox2, string.Empty);

            }
            return true;
        }

        private bool validateLevelCombo()
        {
            if (comboBox1.SelectedItem == null)
            {
                errorProvider3.SetError(comboBox1, "It cannot be empty.");
                return false;
            }
            else
            {
                errorProvider3.SetError(comboBox1, string.Empty);

            }
            return true;
        }

        private void reset()
        {
            textBox2.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

        }


        private void addRubricLevel()
        {
            int number = giveNumber();

            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO rubricLevel (RubricId, Details, MeasurementLevel) VALUES (@id, @Details, @level)", con);
                cmd.Parameters.AddWithValue("@id", comboBox2.Text);
                cmd.Parameters.AddWithValue("@Details", textBox2.Text);
                cmd.Parameters.AddWithValue("@level", number);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            MessageBox.Show("Successfully Inserted!");
            printRubricLevel();
            reset();
        }

        private void modifyRubricLevel()
        {
            int number = giveNumber();
            int rID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            if (rID != 0)
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE rubricLevel SET RubricId = @rid , Details = @Details, MeasurementLevel = @level WHERE Id = @id", con);
                    cmd.Parameters.AddWithValue("@rid", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@Details", textBox2.Text);
                    cmd.Parameters.AddWithValue("@level", number);
                    cmd.Parameters.AddWithValue("@id", rID);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                MessageBox.Show("Successfully Updated!");

                printRubricLevel();
                reset();
            }
            else
            {
                return;
            }
        }

        private int giveNumber()
        {
            if (comboBox1.Text == "Unsatisfactory")
            {
                return 1;
            }
            else if (comboBox1.Text == "Fair")
            {
                return 2;
            }
            else if (comboBox1.Text == "Good")
            {
                return 3;
            }
            else { return 4; }
        }
        
        private bool validateDetails(string Details)
        {
            if (string.IsNullOrEmpty(Details.Trim()))
            {
                errorProvider2.SetError(textBox2, "Details are required");
                return false;
            }
            else
            {
                errorProvider2.SetError(textBox2, string.Empty);
            }

            return true;
        }
        private void fillComboRId()
        {

            string query = "SELECT * FROM Rubric";

            using (SqlConnection connection = new SqlConnection(constr))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    comboBox2.Items.Add(id);

                }


                reader.Close();
                connection.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home f3 = new Home();
            f3.TopLevel = false;
            f3.FormBorderStyle = FormBorderStyle.None;
            f3.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(f3);
            f3.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
