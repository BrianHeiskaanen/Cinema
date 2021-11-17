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
    public partial class PersonalInformation : Form
    {
        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Cinema\Cinema\Cinema.mdf;Integrated Security=True";

        string login, name, surname, patronymic;

        public PersonalInformation(string id)
        {
            InitializeComponent();
            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Login FROM Users WHERE id = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                login = cmd.ExecuteScalar().ToString();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Name FROM Users WHERE id = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                name = cmd.ExecuteScalar().ToString();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Surname FROM Users WHERE id = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                surname = cmd.ExecuteScalar().ToString();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Patronymic FROM Users WHERE id = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                patronymic = cmd.ExecuteScalar().ToString();
            }

            connection.Close();

            textBox1.Text = login;
            textBox2.Text = name;
            textBox3.Text = surname;
            textBox4.Text = patronymic;
        }
    }
}
