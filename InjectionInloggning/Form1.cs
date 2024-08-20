using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace InjectionInloggning
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Inloggning()
        {
            string server = "localhost";
            string database = "labb2";

            string dbUser = "root";
            string dbPass = "";

            string connString = $"SERVER={server};DATABASE={database};UID={dbUser};PASSWORD={dbPass};";

            MySqlConnection conn = new MySqlConnection(connString);

            //Hämta data från textfält
            string user = txtUser.Text;
            string pass = txtPass.Text;

                //Bygger upp SQL querry
                string sqlQuerry = $"SELECT * FROM users WHERE username = @username AND password = @password;"; // Parameteriserat queryn

                lblQuerry.Text = sqlQuerry;

                MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);

                //Exekverar querry
                try
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@username", user);
                    cmd.Parameters.AddWithValue("@password", pass);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    //Kontrollerar resultatet
                    if (reader.Read())
                        lblStatus.Text = "Du har loggat in";
                    else
                        lblStatus.Text = "Du är utloggad";
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    conn.Close();
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Inloggning();
        }
    }
}
