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
    public partial class ViewTicket : Form
    {
        string idTicket, userId, filmScreeningId, filmId;

        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Cinema\Cinema\Cinema.mdf;Integrated Security=True";

        string login, date, time, hall, film, duration, row, place, price;

        public ViewTicket(string idTicket)
        {
            InitializeComponent();
            this.idTicket = idTicket;
        }

        private void ViewTicket_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection(sql);
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 UserId FROM PurchasedTickets WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", idTicket);
                    userId = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Login FROM Users WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", userId);
                    login = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 FilmScreeningId FROM PurchasedTickets WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", idTicket);
                    filmScreeningId = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Date FROM FilmScreenings WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", filmScreeningId);
                    date = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Time FROM FilmScreenings WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", filmScreeningId);
                    time = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Hall FROM FilmScreenings WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", filmScreeningId);
                    hall = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 FilmId FROM FilmScreenings WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", filmScreeningId);
                    filmId = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Name FROM Films WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", filmId);
                    film = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Duration FROM Films WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", filmId);
                    duration = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Row FROM PurchasedTickets WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", idTicket);
                    row = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Place FROM PurchasedTickets WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", idTicket);
                    place = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Price FROM FilmScreenings WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", filmScreeningId);
                    price = cmd.ExecuteScalar().ToString();
                }

                connection.Close();

                textBox1.Text = login;
                textBox2.Text = date;
                textBox3.Text = time;
                textBox4.Text = hall;
                textBox5.Text = film;
                textBox6.Text = duration;
                textBox7.Text = row;
                textBox8.Text = place;
                textBox9.Text = price;
            }
            catch
            {
                MessageBox.Show("Ошибка просмотра информации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
