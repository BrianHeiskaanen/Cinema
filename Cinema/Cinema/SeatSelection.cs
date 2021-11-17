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
    public partial class SeatSelection : Form
    {
        string filmScreeningId, userId;

        string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\maksi\OneDrive\Desktop\Cinema\Cinema\Cinema.mdf;Integrated Security=True";

        public SeatSelection(string filmScreeningId, string userId)
        {
            InitializeComponent();
            this.filmScreeningId = filmScreeningId;
            this.userId = userId;
        }

        private void SeatSelection_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(sql);
            connection.Open();

            List<String> places = new List<String>();
            using (SqlCommand cmd = new SqlCommand(@"SELECT Place FROM PurchasedTickets WHERE FilmScreeningId = @FilmScreeningId", connection))
            {
                cmd.Parameters.AddWithValue("@FilmScreeningId", filmScreeningId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    places.Add(reader.GetString(0));
                }
                reader.Close();
            }

            foreach (var item in places)
            {
                if(item == "1")
                {
                    pictureBox1.Image = Image.FromFile(@"C:\Users\maksi\OneDrive\Desktop\Cinema\Image\1.png");
                    pictureBox1.Enabled = false;
                }
                else if(item == "2")
                {
                    pictureBox2.Image = Image.FromFile(@"C:\Users\maksi\OneDrive\Desktop\Cinema\Image\1.png");
                    pictureBox2.Enabled = false;
                }
                else if(item == "3")
                {
                    pictureBox3.Image = Image.FromFile(@"C:\Users\maksi\OneDrive\Desktop\Cinema\Image\1.png");
                    pictureBox3.Enabled = false;
                }
                else if(item == "4")
                {
                    pictureBox4.Image = Image.FromFile(@"C:\Users\maksi\OneDrive\Desktop\Cinema\Image\1.png");
                    pictureBox4.Enabled = false;
                }
                else if(item == "5")
                {
                    pictureBox5.Image = Image.FromFile(@"C:\Users\maksi\OneDrive\Desktop\Cinema\Image\1.png");
                    pictureBox5.Enabled = false;
                }
                else if(item == "6")
                {
                    pictureBox6.Image = Image.FromFile(@"C:\Users\maksi\OneDrive\Desktop\Cinema\Image\1.png");
                    pictureBox6.Enabled = false;
                }
                else if(item == "7")
                {
                    pictureBox7.Image = Image.FromFile(@"C:\Users\maksi\OneDrive\Desktop\Cinema\Image\1.png");
                    pictureBox7.Enabled = false;
                }
                else if(item == "8")
                {
                    pictureBox8.Image = Image.FromFile(@"C:\Users\maksi\OneDrive\Desktop\Cinema\Image\1.png");
                    pictureBox8.Enabled = false;
                }
                else if(item == "9")
                {
                    pictureBox9.Image = Image.FromFile(@"C:\Users\maksi\OneDrive\Desktop\Cinema\Image\1.png");
                    pictureBox9.Enabled = false;
                }
                else if(item == "10")
                {
                    pictureBox10.Image = Image.FromFile(@"C:\Users\maksi\OneDrive\Desktop\Cinema\Image\1.png");
                    pictureBox10.Enabled = false;
                }
                else if(item == "11")
                {
                    pictureBox11.Image = Image.FromFile(@"C:\Users\maksi\OneDrive\Desktop\Cinema\Image\1.png");
                    pictureBox11.Enabled = false;
                }
                else if (item == "12")
                {
                    pictureBox12.Image = Image.FromFile(@"C:\Users\maksi\OneDrive\Desktop\Cinema\Image\1.png");
                    pictureBox12.Enabled = false;
                }
                else if(item == "13")
                {
                    pictureBox13.Image = Image.FromFile(@"C:\Users\maksi\OneDrive\Desktop\Cinema\Image\1.png");
                    pictureBox13.Enabled = false;
                }
                else if(item == "14")
                {
                    pictureBox14.Image = Image.FromFile(@"C:\Users\maksi\OneDrive\Desktop\Cinema\Image\1.png");
                    pictureBox14.Enabled = false;
                }
                else if(item == "15")
                {
                    pictureBox15.Image = Image.FromFile(@"C:\Users\maksi\OneDrive\Desktop\Cinema\Image\1.png");
                    pictureBox15.Enabled = false;
                }
                else if(item == "16")
                {
                    pictureBox16.Image = Image.FromFile(@"C:\Users\maksi\OneDrive\Desktop\Cinema\Image\1.png");
                    pictureBox16.Enabled = false;
                }
            }

            connection.Close();
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            string name = null;
            if (sender is PictureBox)
            {
                name = (sender as PictureBox).Name;
            }

            string numberPlace = Convert.ToString(name[name.Length - 2]);
            numberPlace = numberPlace + Convert.ToString(name[name.Length - 1]);

            if (IsDigitsOnly(numberPlace) == false)
            {
                numberPlace = Convert.ToString(name[name.Length - 1]);
            }

            Form buyTicket = new BuyTicket(filmScreeningId, numberPlace, userId);
            buyTicket.ShowDialog();
            Close();
        }
    }
}
