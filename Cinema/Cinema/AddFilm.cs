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
    public partial class AddFilm : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Cinema\Cinema\Cinema.mdf;Integrated Security=True";

        public AddFilm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
			SqlConnection connection = new SqlConnection(sql);

			connection.Open();


			if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || richTextBox1.Text == "")
			{
				MessageBox.Show("Не все поля заполнены!", "Ошибка добавления фильма", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				try
				{
					using (SqlCommand cmd1 = new SqlCommand(@"INSERT INTO [Films] (Name, ProductionYear, Genre, Director, Age, Duration, Description) VALUES (@Name, @ProductionYear, @Genre, @Director, @Age, @Duration, @Description)", connection))
					{
						cmd1.Parameters.AddWithValue("@Name", textBox1.Text);
						cmd1.Parameters.AddWithValue("@ProductionYear", textBox2.Text);
						cmd1.Parameters.AddWithValue("@Genre", textBox3.Text);
						cmd1.Parameters.AddWithValue("@Director", textBox4.Text);
						cmd1.Parameters.AddWithValue("@Age", textBox5.Text);
						cmd1.Parameters.AddWithValue("@Duration", textBox6.Text);
						cmd1.Parameters.AddWithValue("@Description", richTextBox1.Text);

						cmd1.ExecuteNonQuery();
					}

					MessageBox.Show("Успешное добавление фильма!", "Фильм добавлен", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch
				{
					MessageBox.Show("Ошибка добавления фильма!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
