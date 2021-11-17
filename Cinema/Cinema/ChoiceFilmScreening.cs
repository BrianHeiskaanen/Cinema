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
    public partial class ChoiceFilmScreening : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Cinema\Cinema\Cinema.mdf;Integrated Security=True";

        string filmId, filmName, date, time;

        string full;

        string userId;

        List<String> idFilmScreening = new List<String>();

        public ChoiceFilmScreening(string userId)
        {
            InitializeComponent();
            this.userId = userId;

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

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Name FROM Films WHERE id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@id", filmId);
                    filmName = cmd.ExecuteScalar().ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            int number = comboBox1.SelectedIndex;

            try
            {
                Form informationAboutTheFilmScreening = new InformationAboutTheFilmScreening(idFilmScreening[number]);
                informationAboutTheFilmScreening.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Ошибка просмотра информации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int number = comboBox1.SelectedIndex;

            try
            {
                Form seatSelection = new SeatSelection(idFilmScreening[number], userId);
                seatSelection.ShowDialog();
                Close();
            }
            catch
            {
                MessageBox.Show("Ошибка выбора киносеанса!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
