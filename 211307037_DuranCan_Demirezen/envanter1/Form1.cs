using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace envanter1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-K2RD493\\SQLEXPRESS;Initial Catalog=envanter;Integrated Security=True");

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            MessageBox.Show("Bağlantı Kuruldu");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Close();
            MessageBox.Show("Bağlantı Kesildi");
        }
    }
}
