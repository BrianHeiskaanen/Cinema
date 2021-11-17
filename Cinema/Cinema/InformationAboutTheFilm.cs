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
    public partial class InformationAboutTheFilm : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Cinema\Cinema\Cinema.mdf;Integrated Security=True";

        string name, productionYear, genre, director, age, duration, description;

        public InformationAboutTheFilm(string name)
        {
            InitializeComponent();
            this.name = name;
        }

        private void InformationAboutTheFilm_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection(sql);
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 ProductionYear FROM Films WHERE Name = @Name", connection))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    productionYear = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Genre FROM Films WHERE Name = @Name", connection))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    genre = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Director FROM Films WHERE Name = @Name", connection))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    director = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Age FROM Films WHERE Name = @Name", connection))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    age = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Duration FROM Films WHERE Name = @Name", connection))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    duration = cmd.ExecuteScalar().ToString();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Description FROM Films WHERE Name = @Name", connection))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    description = cmd.ExecuteScalar().ToString();
                }

                connection.Close();

                textBox1.Text = name;
                textBox2.Text = productionYear;
                textBox3.Text = genre;
                textBox4.Text = director;
                textBox5.Text = age;
                textBox6.Text = duration;
                richTextBox1.Text = description;
            }
            catch
            {
                MessageBox.Show("Ошибка просмотра информации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
