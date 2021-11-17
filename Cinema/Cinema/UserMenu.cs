using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cinema
{
    public partial class UserMenu : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Cinema\Cinema\Cinema.mdf;Integrated Security=True";

        string id;

        SqlDataAdapter adapter1, adapter2, adapter3 = null;
        DataTable table1, table2, table3 = null;

        public UserMenu(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void личнаяИнформацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form personalInformation = new PersonalInformation(id);
            personalInformation.ShowDialog();
        }

        private void сменаПароляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string password;

            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT Password FROM Users WHERE id = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                password = cmd.ExecuteScalar().ToString();
            }

            Form passwordChange = new PasswordChange(password, id);
            passwordChange.ShowDialog();

            connection.Close();
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form authorization = new Authorization();
            Hide();
            authorization.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            string name;

            using (SqlCommand cmd = new SqlCommand("SELECT Name FROM Films WHERE id = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", textBox1.Text);
                name = cmd.ExecuteScalar().ToString();
            }

            connection.Close();

            Form informationAboutTheFilm = new InformationAboutTheFilm(name);
            informationAboutTheFilm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {


            Form informationAboutTheFilmScreening = new InformationAboutTheFilmScreening(textBox2.Text);
            informationAboutTheFilmScreening.ShowDialog();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Films; ", connection))
            {
                adapter1 = new SqlDataAdapter(cmd);
                table1 = new DataTable();
                adapter1.Fill(table1);
                dataGridView2.DataSource = table1;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM FilmScreenings; ", connection))
            {
                adapter2 = new SqlDataAdapter(cmd);
                table2 = new DataTable();
                adapter2.Fill(table2);
                dataGridView3.DataSource = table2;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM PurchasedTickets WHERE IsItOver = @IsItOver AND UserId = @UserId; ", connection))
            {
                cmd.Parameters.AddWithValue("@IsItOver", "no");
                cmd.Parameters.AddWithValue("@UserId", id);
                adapter3 = new SqlDataAdapter(cmd);
                table3 = new DataTable();
                adapter3.Fill(table3);
                dataGridView4.DataSource = table3;
            }

            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form viewTicket = new ViewTicket(textBox3.Text);
            viewTicket.ShowDialog();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа: Кинотеатр\nРазработчик: Емельяненко Даниил\nНомер группы: 38ТП", "Справка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UserMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void купитьБилетНаКиносеансToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form choiceFilmScreening = new ChoiceFilmScreening(id);
            choiceFilmScreening.ShowDialog();
        }

        private void UserMenu_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Films; ", connection))
            {
                adapter1 = new SqlDataAdapter(cmd);
                table1 = new DataTable();
                adapter1.Fill(table1);
                dataGridView2.DataSource = table1;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM FilmScreenings; ", connection))
            {
                adapter2 = new SqlDataAdapter(cmd);
                table2 = new DataTable();
                adapter2.Fill(table2);
                dataGridView3.DataSource = table2;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM PurchasedTickets WHERE IsItOver = @IsItOver AND UserId = @UserId; ", connection))
            {
                cmd.Parameters.AddWithValue("@IsItOver", "no");
                cmd.Parameters.AddWithValue("@UserId", id);
                adapter3 = new SqlDataAdapter(cmd);
                table3 = new DataTable();
                adapter3.Fill(table3);
                dataGridView4.DataSource = table3;
            }

            connection.Close();
        }
    }
}
