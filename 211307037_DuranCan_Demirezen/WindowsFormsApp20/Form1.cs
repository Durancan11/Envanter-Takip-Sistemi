using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;




namespace WindowsFormsApp20
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection("Server=localhost;Database=mydatabase;Uid=myusername;Pwd=mypassword;");

            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM mytable", connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader["id"] + " " + reader["name"]);
            }
        }
    }
}
