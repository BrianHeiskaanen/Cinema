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
    public partial class TicketPrices : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Cinema\Cinema\Cinema.mdf;Integrated Security=True";

        string filmId, filmName, date, time;

        string full;

        int fullPrice;
        string price;

        List<String> idFilmScreening = new List<String>();
        List<String> kolvo = new List<String>();

        public TicketPrices()
        {
            InitializeComponent();

            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand(@"SELECT id FROM FilmScreenings", connection))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    idFilmScreening.Add(Convert.ToString(reader.GetInt32(0)));
                }
                reader.Close();
            }

            foreach (var item in idFilmScreening)
            {
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 FilmId FROM FilmScreenings WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", item);
                    filmId = cmd.ExecuteScalar().ToString();
                }

                try
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Name FROM Films WHERE id = @id", connection))
                    {
                        cmd.Parameters.AddWithValue("@id", filmId);
                        filmName = cmd.ExecuteScalar().ToString();
                    }
                }
                catch
                {
                    filmName = "Фильм удален";
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Date FROM FilmScreenings WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", item);
                    date = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Time FROM FilmScreenings WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", item);
                    time = cmd.ExecuteScalar().ToString();
                }

                full = filmName + " | " + date + " | " + time;
                comboBox1.Items.Add(full);
            }

            connection.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int number = comboBox1.SelectedIndex;

            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            try
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT Price FROM FilmScreenings WHERE @id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", idFilmScreening[number]);
                    price = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand(@"SELECT id FROM PurchasedTickets WHERE FilmScreeningId = @FilmScreeningId", connection))
                {
                    cmd.Parameters.AddWithValue("@FilmScreeningId", idFilmScreening[number]);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        kolvo.Add(Convert.ToString(reader.GetInt32(0)));
                    }
                    reader.Close();
                }

                int size = kolvo.Count;
                fullPrice = size * Convert.ToInt32(price);

                label2.Text = Convert.ToString(fullPrice) + " рублей";
            }
            catch
            {
                MessageBox.Show("Ошибка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            connection.Close();
        }
    }
}
