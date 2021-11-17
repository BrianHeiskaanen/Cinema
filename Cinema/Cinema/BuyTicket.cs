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
    public partial class BuyTicket : Form
    {
        string filmScreeningId, numberPlace, filmId, userId;

        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Cinema\Cinema\Cinema.mdf;Integrated Security=True";

        string date, time, film, hall, price;

        public BuyTicket(string filmScreeningId, string numberPlace, string userId)
        {
            InitializeComponent();
            this.filmScreeningId = filmScreeningId;
            this.numberPlace = numberPlace;
            this.userId = userId;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            try
            {
                using (SqlCommand cmd1 = new SqlCommand(@"INSERT INTO [PurchasedTickets] (UserId, FilmScreeningId, Row, Place, IsItOver) VALUES (@UserId, @FilmScreeningId, @Row, @Place, @IsItOver)", connection))
                {
                    cmd1.Parameters.AddWithValue("@UserId", userId);
                    cmd1.Parameters.AddWithValue("@FilmScreeningId", filmScreeningId);
                    cmd1.Parameters.AddWithValue("@Row", textBox6.Text);
                    cmd1.Parameters.AddWithValue("@Place", numberPlace);
                    cmd1.Parameters.AddWithValue("@IsItOver", "no");

                    cmd1.ExecuteNonQuery();
                }

                MessageBox.Show("Успешная покупка билета!", "Билет куплен", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка покупки билета!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
                Close();
            }
        }

        private void BuyTicket_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection(sql);
                connection.Open();

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

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Hall FROM FilmScreenings WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", filmScreeningId);
                    hall = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Price FROM FilmScreenings WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", filmScreeningId);
                    price = cmd.ExecuteScalar().ToString();
                }

                connection.Close();

                textBox1.Text = date;
                textBox2.Text = time;
                textBox3.Text = film;
                textBox4.Text = hall;
                textBox5.Text = price;

                if(numberPlace == "1" || numberPlace == "2" || numberPlace == "3" || numberPlace == "4")
                {
                    textBox6.Text = "1";
                }
                else if((numberPlace == "5" || numberPlace == "6" || numberPlace == "7" || numberPlace == "8" || numberPlace == "9" || numberPlace == "10"))
                {
                    textBox6.Text = "2";
                }
                else
                {
                    textBox6.Text = "3";
                }

                textBox7.Text = numberPlace;
            }
            catch
            {
                MessageBox.Show("Ошибка покупки билета!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
