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
    public partial class AddFilmScreening : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Cinema\Cinema\Cinema.mdf;Integrated Security=True";

        public AddFilmScreening()
        {
            InitializeComponent();

            dateTimePicker2.Format = DateTimePickerFormat.Time;
            dateTimePicker2.ShowUpDown = true;

			SqlConnection connection = new SqlConnection(sql);
			connection.Open();

			List<String> filmName = new List<String>();
			using (SqlCommand cmd = new SqlCommand(@"SELECT Name FROM Films", connection))
			{
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					filmName.Add(reader.GetString(0));
				}
				reader.Close();
			}

			foreach (var item in filmName)
			{
				comboBox1.Items.Add(item);
			}

			comboBox2.Items.Add("Зал 1");
			comboBox2.Items.Add("Зал 2");

			connection.Close();
		}

        private void button1_Click(object sender, EventArgs e)
        {
			SqlConnection connection = new SqlConnection(sql);

			connection.Open();

			if (dateTimePicker1.Text == "" || dateTimePicker2.Text == "" || comboBox1.Text == "" || comboBox2.Text == "" || textBox1.Text == "")
			{
				MessageBox.Show("Не все поля заполнены!", "Ошибка добавления киносеанса", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				try
				{
					string filmId;

					using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 id FROM Films WHERE Name = @Name", connection))
					{
						cmd.Parameters.AddWithValue("@Name", comboBox1.Text);
						filmId = cmd.ExecuteScalar().ToString();
					}

					using (SqlCommand cmd1 = new SqlCommand(@"INSERT INTO [FilmScreenings] (Date, Time, FilmId, Hall, Price) VALUES (@Date, @Time, @FilmId, @Hall, @Price)", connection))
					{
						cmd1.Parameters.AddWithValue("@Date", dateTimePicker1.Value.ToShortDateString());
						cmd1.Parameters.AddWithValue("@Time", dateTimePicker2.Value.ToShortTimeString());
						cmd1.Parameters.AddWithValue("@FilmId", filmId);
						cmd1.Parameters.AddWithValue("@Hall", comboBox2.Text);
						cmd1.Parameters.AddWithValue("@Price", textBox1.Text);
						//cmd1.Parameters.AddWithValue("@IsItOver", "no");

						cmd1.ExecuteNonQuery();
					}

					MessageBox.Show("Успешное добавление киносеанса!", "Киносеанс добавлен", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch
				{
					MessageBox.Show("Ошибка добавления киносеанса!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
					connection.Close();
					Close();
				}
			}
		}
    }
}
