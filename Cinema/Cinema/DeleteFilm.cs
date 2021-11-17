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
    public partial class DeleteFilm : Form
    {
		string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Cinema\Cinema\Cinema.mdf;Integrated Security=True";

		public DeleteFilm()
        {
            InitializeComponent();

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

			connection.Close();
		}

        private void button1_Click(object sender, EventArgs e)
        {
            Form informationAboutTheFilm = new InformationAboutTheFilm(comboBox1.Text);
            informationAboutTheFilm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            try
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Films WHERE Name = @Name; ", connection))
                {
                    cmd.Parameters.AddWithValue("@Name", comboBox1.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Успешное удаление фильма!", "Фильм удален", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка удаления фильма!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
                Close();
            }
        }
    }
}
