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
    public partial class PasswordChange : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Cinema\Cinema\Cinema.mdf;Integrated Security=True";

        string password, id;

        public PasswordChange(string password, string id)
        {
            InitializeComponent();
            this.password = password;
            this.id = id;
            checkBox1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != password)
            {
                MessageBox.Show("Неверный старый пароль", "Ошибка смены пароля", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка смены пароля", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Вы сменили пароль", "Успешная смена пароля", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                SqlConnection connection = new SqlConnection(sql);
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("Update Users Set Password = @Password Where id = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@Password", textBox2.Text);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                connection.Close();

                Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.UseSystemPasswordChar = true;
                textBox2.UseSystemPasswordChar = true;
                textBox3.UseSystemPasswordChar = true;
                checkBox1.Text = "показать";
            }
            else
            {
                textBox1.UseSystemPasswordChar = false;
                textBox2.UseSystemPasswordChar = false;
                textBox3.UseSystemPasswordChar = false;
                checkBox1.Text = "скрыть";
            }
        }
    }
}
