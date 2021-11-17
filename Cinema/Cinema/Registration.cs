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
    public partial class Registration : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Cinema\Cinema\Cinema.mdf;Integrated Security=True";

        public Registration()
        {
            InitializeComponent();
            checkBox1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox5.Text != textBox6.Text)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlConnection connection = new SqlConnection(sql);

                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Login = @Login", connection))
                {
                    cmd.Parameters.AddWithValue("@Login", textBox1.Text);
                    if (cmd.ExecuteScalar() == null)
                    {
                        try
                        {
                            using (SqlCommand cmd1 = new SqlCommand(@"INSERT INTO [Users] (Login, Password, Name, Surname, Patronymic) VALUES (@Login, @Password, @Name, @Surname, @Patronymic)", connection))
                            {
                                cmd1.Parameters.AddWithValue("@Login", textBox1.Text);
                                cmd1.Parameters.AddWithValue("@Name", textBox2.Text);
                                cmd1.Parameters.AddWithValue("@Surname", textBox3.Text);
                                cmd1.Parameters.AddWithValue("@Patronymic", textBox4.Text);
                                cmd1.Parameters.AddWithValue("@Password", textBox5.Text);

                                cmd1.ExecuteNonQuery();
                            }

                        }
                        catch
                        {
                            MessageBox.Show("Ошибка регистрации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            MessageBox.Show("Успешная регистрация!", "Аккаунт пользователя создан", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            connection.Close();
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Аккаунт с таким логином уже существует", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox5.UseSystemPasswordChar = true;
                textBox6.UseSystemPasswordChar = true;
                checkBox1.Text = "показать";
            }
            else
            {
                textBox5.UseSystemPasswordChar = false;
                textBox6.UseSystemPasswordChar = false;
                checkBox1.Text = "скрыть";
            }
        }
    }
}
