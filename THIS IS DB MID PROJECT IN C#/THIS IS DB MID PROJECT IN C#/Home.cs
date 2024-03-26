using System;
using System.Data.SqlClient;
using System.Drawing.Text;
using System.Windows.Forms;


namespace THIS_IS_DB_MID_PROJECT_IN_C_
{
    public partial class Home : Form
    {
        SqlConnection connection = new SqlConnection("Data Source = DESKTOP-IJI0N4G\\SQLEXPRESS;Initial Catalog = NorthWind; Integrated Security = True; Encrypt = False;");
        TabControl tabcontrol1;
        int id;
        public Home()
        {
            InitializeComponent();
            tabcontrol1 = new TabControl();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openForm(new Clo());
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            openForm(new Form4());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            openForm(new Students());
           
        }
        private void openForm(Form f3)
        {
            f3.TopLevel = false;
            f3.FormBorderStyle = FormBorderStyle.None;
            f3.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(f3);
            f3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openForm(new Ruberics());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openForm(new RubericsLevel());
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }
    }
}
