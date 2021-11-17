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
    public partial class Authorization : Form
    {
        private const string adminLogin = "admin";
        private const string adminPassword = "123";
        public string id;
        
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Cinema\Cinema\Cinema.mdf;Integrated Security=True";

        public Authorization()
        {
            InitializeComponent();
            checkBox1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);

            try
            {
                connection.Open();

                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Не все поля заполнены!", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (textBox1.Text == adminLogin && textBox2.Text == adminPassword)
                {
                    MessageBox.Show("Успешная автоизация!", "Админ-аккаунт", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form adminMenu = new AdminMenu();
                    adminMenu.Show();
                    Hide();
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Login = @Login AND Password = @Password", connection))
                    {
                        cmd.Parameters.AddWithValue("@Login", textBox1.Text);
                        cmd.Parameters.AddWithValue("@Password", textBox2.Text);
                        if (cmd.ExecuteScalar() == null)
                        {
                            MessageBox.Show("Неверный логин или пароль", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox1.Text = "";
                            textBox2.Text = "";
                        }
                        else
                        {
                            using (SqlCommand cmd1 = new SqlCommand("SELECT TOP 1 id FROM Users WHERE Login = @Login", connection))
                            {
                                cmd1.Parameters.AddWithValue("@Login", textBox1.Text);
                                id = cmd1.ExecuteScalar().ToString();
                            }

                            MessageBox.Show("Успешная авторизация!", "Аккаунт пользователя", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Form userMenu = new UserMenu(id);
                            userMenu.Show();
                            Hide();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка авторизации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form registration = new Registration();
            registration.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = true;
                checkBox1.Text = "показать";
            }
            else
            {
                textBox2.UseSystemPasswordChar = false;
                checkBox1.Text = "скрыть";
            }
        }

        private void Authorization_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
