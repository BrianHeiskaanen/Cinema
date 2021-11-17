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

namespace Cinema
{
    public partial class InformationAboutTheFilmScreening : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Cinema\Cinema\Cinema.mdf;Integrated Security=True";

        string filmScreeningsId, filmId;
        string date, time, film, hall, price;

        public InformationAboutTheFilmScreening(string filmScreeningsId)
        {
            InitializeComponent();
            this.filmScreeningsId = filmScreeningsId;
        }

        private void InformationAboutTheFilmScreening_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection(sql);
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Date FROM FilmScreenings WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", filmScreeningsId);
                    date = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Time FROM FilmScreenings WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", filmScreeningsId);
                    time = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 FilmId FROM FilmScreenings WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", filmScreeningsId);
                    filmId = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Name FROM Films WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", filmId);
                    film = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Hall FROM FilmScreenings WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", filmScreeningsId);
                    hall = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Price FROM FilmScreenings WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", filmScreeningsId);
                    price = cmd.ExecuteScalar().ToString();
                }

                connection.Close();

                textBox1.Text = date;
                textBox2.Text = time;
                textBox3.Text = film;
                textBox4.Text = hall;
                textBox5.Text = price;
            }
            catch
            {
                MessageBox.Show("Ошибка просмотра информации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
