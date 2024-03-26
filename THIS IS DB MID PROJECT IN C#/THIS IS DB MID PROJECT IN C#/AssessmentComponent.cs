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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace THIS_IS_DB_MID_PROJECT_IN_C_
{
    public partial class AssessmentComponent : Form
    {
            string constr = "Data Source= DESKTOP-IJI0N4G\\SQLEXPRESS;Initial Catalog=ProjectB;Integrated Security=True";
        public AssessmentComponent()
        {
            InitializeComponent();
            fillAssessmentCombo();
            fillRubricCombo();
            printAssessmentComponent();


        }
        private void ModifyAssessmentComp()
        {
            int ACID = Convert.ToInt32(ACGrid.SelectedRows[0].Cells[0].Value); // gets the id of selected row
            DateTime Date = Convert.ToDateTime(ACGrid.SelectedRows[0].Cells[4].Value); // gets the date from selected row
            int aID = Convert.ToInt32(ACGrid.SelectedRows[0].Cells[6].Value);
            int rID = Convert.ToInt32(ACGrid.SelectedRows[0].Cells[2].Value);





            if (aID != null && rID != null && ACID != null && Date != null)
            {
                using (SqlConnection con = new SqlConnection(constr))
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE assessmentComponent SET Name = @name, RubricId = @rId, TotalMarks = @totalmarks,DateCreated = @date,  DateUpdated = GetDate(), AssessmentId = @aId WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@rId", rID);
                    cmd.Parameters.AddWithValue("@totalmarks", textBox2.Text);
                    cmd.Parameters.AddWithValue("@date", Date);
                    cmd.Parameters.AddWithValue("@aId", aID);
                    cmd.Parameters.AddWithValue("@id", ACID);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                MessageBox.Show("Successfully Updated!");
                printAssessmentComponent();
                reset();
            }
            else
            {
                return;
            }


        }
        private void fillRubricCombo()
        {

            string query = "SELECT * FROM rubric";

            using (SqlConnection connection = new SqlConnection(constr))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string reg = reader.GetString(1);
                    comboBox2.Items.Add(reg);

                }


                reader.Close();
                connection.Close();
            }
        }
        private void fillAssessmentCombo()
        {

            string query = "SELECT * FROM Assessment";

            using (SqlConnection connection = new SqlConnection(constr))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string reg = reader.GetString(1);
                    assmBox.Items.Add(reg);

                }


                reader.Close();
                connection.Close();
            }
        }

        private bool validateMarks(string marks)
        {
            int number;
            if (string.IsNullOrWhiteSpace(marks) || !int.TryParse(marks, out number))
            {
                errorProvider2.SetError(textBox2, "It is required and it should be an integer");
                return false;
            }
            else
            {
                errorProvider2.SetError(textBox2, string.Empty);
                return true;

            }
        }
        private bool validateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                errorProvider1.SetError(textBox1, "This name is required.");
                return false;
            }
            else
            {
                errorProvider1.SetError(textBox1, string.Empty);
                return true;
            }

        }
        private bool validateRubricCombo()
        {
            if (comboBox2.SelectedItem == null)
            {
                errorProvider4.SetError(comboBox2, "It cannot be empty.");
                return false;
            }
            else
            {
                errorProvider4.SetError(comboBox2, string.Empty);

            }
            return true;
        }
        private bool validateAssessmentCombo()
        {
            if (assmBox.SelectedItem == null)
            {
                errorProvider3.SetError(assmBox, "It cannot be empty.");
                return false;
            }
            else
            {
                errorProvider3.SetError(assmBox, string.Empty);

            }
            return true;
        }

        private int getRubricId()
        {
            int Id = -1;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select id from rubric where details = @details", con);
            cmd.Parameters.AddWithValue("@details", comboBox2.Text);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Id = Convert.ToInt32(reader["id"]);
            }

            con.Close();

            return Id;
        }

        private int getAsssessmentId()
        {
            int Id = -1;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select id from assessment where title = @title", con);
            cmd.Parameters.AddWithValue("@title", assmBox.Text);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Id = Convert.ToInt32(reader["id"]);
            }

            con.Close();

            return Id;
        }
        private void reset()
        {
            textBox1.Clear();
            textBox2.Clear();
            assmBox.Items.Clear();
            comboBox2.Items.Clear();

        }
        private void addAssessmentComp()
        {
            int aID = getAsssessmentId();
            int rID = getRubricId();

            if (aID != -1 && rID != -1)
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO assessmentComponent (Name, RubricId, TotalMarks, DateCreated, DateUpdated, AssessmentId) VALUES (@name, @rid, @totalmarks, GetDate(), GetDate(), @aId)", con);
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@rid", rID); // Corrected parameter name to @rid
                    cmd.Parameters.AddWithValue("@totalmarks", textBox2.Text);
                    cmd.Parameters.AddWithValue("@aId", aID);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Successfully Inserted!");
                printAssessmentComponent();
            }
            else
            {
                MessageBox.Show("Failed to insert assessment component. Please make sure assessment and rubric IDs are valid.");
                return;
            }
        }



        private void AssessmentComponent_Load(object sender, EventArgs e)
        {
            printAssessmentComponent();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (!validateName(textBox1.Text))
            {
                return;
            }
            if (!validateMarks(textBox2.Text))
            {
                return;
            }
            if (!validateAssessmentCombo())
            {
                return;
            }
            if (!validateRubricCombo())
            {
                return;
            }

            addAssessmentComp();

        }

        private void printAssessmentComponent()
        {

            string query = "SELECT * FROM assessmentcomponent";

            using (SqlConnection connection = new SqlConnection(constr))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                ACGrid.DataSource = dataTable;
                reader.Close();
                connection.Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!validateName(textBox1.Text))
            {
                return;
            }
            if (!validateMarks(textBox2.Text))
            {
                return;
            }
            if (!validateAssessmentCombo())
            {
                return;
            }
            if (!validateRubricCombo())
            {
                return;
            }

            addAssessmentComp();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void printAssessment()
        {
            string connectionString = "Data Source=DESKTOP-IJI0N4G\\SQLEXPRESS;Initial Catalog=ProjectB;Integrated Security=True";
            string query = "SELECT * FROM AssessmentComponent";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                ACGrid.DataSource = dataTable;
                reader.Close();
                connection.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!validateName(textBox1.Text))
            {
                return;
            }
            if (!validateMarks(textBox2.Text))
            {
                return;
            }
            if (!validateAssessmentCombo())
            {
                return;
            }
            if (!validateRubricCombo())
            {
                return;
            }

            ModifyAssessmentComp();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(ACGrid.SelectedRows[0].Cells[0].Value);

            string deleteQuery = "DELETE FROM AssessmentComponent WHERE id = @Id";
            using (SqlConnection connection = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", rowIndex);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        MessageBox.Show("Successfully Deleted!");
                        connection.Close();
                        printAssessmentComponent();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
    }
}
