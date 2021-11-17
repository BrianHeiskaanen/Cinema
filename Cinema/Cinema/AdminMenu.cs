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
    public partial class AdminMenu : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Cinema\Cinema\Cinema.mdf;Integrated Security=True";
        SqlDataAdapter adapter, adapter1, adapter2, adapter3 = null;
        DataTable table, table1, table2, table3 = null;
        public AdminMenu()
        {
            InitializeComponent();
        }

        private void AdminMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form authorization = new Authorization();
            Hide();
            authorization.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form personalInformation = new PersonalInformation(textBox4.Text);
            personalInformation.ShowDialog();
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

        private void стоимостьБилетовДляКаждогоКиносеансаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ticketPrices = new TicketPrices();
            ticketPrices.ShowDialog();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users; ", connection))
            {
                adapter = new SqlDataAdapter(cmd);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }

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

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM PurchasedTickets WHERE IsItOver = @IsItOver; ", connection))
            {
                cmd.Parameters.AddWithValue("@IsItOver", "no");
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

        private void добавитьФильмToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form addFilm = new AddFilm();
            addFilm.ShowDialog();
        }

        private void удалитьФильмToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form deleteFilm = new DeleteFilm();
            deleteFilm.ShowDialog();
        }

        private void добавитьКиносеансToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form addFilmScreening = new AddFilmScreening();
            addFilmScreening.ShowDialog();
        }

        private void удалитьКиносеансToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form deleteFilmScreening = new DeleteFilmScreening();
            deleteFilmScreening.ShowDialog();
        }

        private void AdminMenu_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users; ", connection))
            {
                adapter = new SqlDataAdapter(cmd);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }

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

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM PurchasedTickets WHERE IsItOver = @IsItOver; ", connection))
            {
                cmd.Parameters.AddWithValue("@IsItOver", "no");
                adapter3 = new SqlDataAdapter(cmd);
                table3 = new DataTable();
                adapter3.Fill(table3);
                dataGridView4.DataSource = table3;
            }

            connection.Close();
        }
    }
}
