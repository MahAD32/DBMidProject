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
    public partial class Ruberics : Form
    {
        string constr = "Data Source= DESKTOP-IJI0N4G\\SQLEXPRESS;Initial Catalog=ProjectB;Integrated Security=True";


        public Ruberics()
        {
            InitializeComponent();
            fillComboCLO();
            printRubric();

        }
        private void Rubric_Load(object sender, EventArgs e)
        {
            printRubric();
        }
        private void fillComboCLO()
        {

            string query = "SELECT * FROM CLO";

            using (SqlConnection connection = new SqlConnection(constr))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string reg = reader.GetString(1);
                    comboBox1.Items.Add(reg);

                }


                reader.Close();
                connection.Close();
            }
        }
        private int getCLOid()
        {
            int CLOId = -1;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select id from Clo where name = @name", con);
            cmd.Parameters.AddWithValue("@name", comboBox1.Text);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                CLOId = Convert.ToInt32(reader["id"]);
            }

            con.Close();

            return CLOId;
        }

        private void AddRubric()
        {
            int number = getCLOid();

            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO rubric (Id, Details, CLoId) VALUES (@id, @Details, @CLoId)", con);
                cmd.Parameters.AddWithValue("@id", textBox1.Text);
                cmd.Parameters.AddWithValue("@Details", textBox2.Text);
                if (number != -1)
                {
                    cmd.Parameters.AddWithValue("@CLoId", number);
                }
                else
                {
                    return;
                }
                if (!CheckIDExist())
                {
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully Inserted!");
                    printRubric();
                    reset();
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Rubric Id already Exist");
                    reset();
                }

            }


        }

        private void modifyRubic()
        {
            int rID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            int number = getCLOid();

            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE rubric SET  id = @id ,Details = @Details, CLoId = @CLoId WHERE Id = @id", con);
                cmd.Parameters.AddWithValue("@id", rID);
                cmd.Parameters.AddWithValue("@Details", textBox2.Text);
                if (number != -1)
                {
                    cmd.Parameters.AddWithValue("@CLoId", number);
                }
                else
                {
                    return;
                }
                cmd.ExecuteNonQuery();
                con.Close();
            }
            MessageBox.Show("Successfully Updated!");
            printRubric();
            reset();
        }
        private void printRubric()
        {

            string query = "SELECT * FROM rubric";

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

        private bool validateCLOCombo()
        {
            if (comboBox1.SelectedItem == null)
            {
                errorProvider1.SetError(comboBox1, "It cannot be empty.");
                return false;
            }
            else
            {
                errorProvider1.SetError(comboBox1, string.Empty);

            }
            return true;
        }
        private void reset()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private bool Empty(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                errorProvider3.SetError(textBox1, string.Empty);
                return true;
            }
            errorProvider6.SetError(textBox1, "The id box should be empty while Modifying.");
            return false;

        }


        private bool ValidateId(string total)
        {
            int number;

            if (string.IsNullOrWhiteSpace(total) || !int.TryParse(total, out number))
            {
                errorProvider3.SetError(textBox1, "It is required and it should be an integer");
                return false;
            }
            else
            {
                errorProvider3.SetError(textBox1, string.Empty);
                return true;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!validateCLOCombo())
            { return; }

            if (!validateDetails(textBox2.Text))
            {
                return;
            }
            if (!ValidateId(textBox1.Text))
            {
                return;
            }

            AddRubric();

        }
        private bool CheckIDExist()
        {

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM rubric where id = @id", con);
            cmd.Parameters.AddWithValue("@id",textBox1.Text);
            int count = (int)(cmd.ExecuteScalar());

            con.Close();

            // If count is greater than 0, it means the date exists
            return count > 0;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

            string deleteQuery = "DELETE FROM rubric WHERE id = @RId";
            using (SqlConnection connection = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@RId", rowIndex);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Successfully Deleted!");
                        printRubric();

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
            if (!validateCLOCombo())
            { return; }

            if (!validateDetails(textBox2.Text))
            {
                return;
            }
            if (!Empty(textBox1.Text))
            {
                return;
            }


            modifyRubic();
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
