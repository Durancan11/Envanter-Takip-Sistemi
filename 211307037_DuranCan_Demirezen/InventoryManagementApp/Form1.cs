using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace InventoryManagementApp
{
    public partial class MainForm : Form
    {
        private const string connectionString = "server=localhost;user=root;database=inventorydb;port=3306;password=your_password";
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataAdapter adapter;
        private DataTable dataTable;

        public MainForm()
        {
            InitializeComponent();
            connection = new MySqlConnection(connectionString);
            dataTable = new DataTable();

            // DataGridView'e verileri yükle
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM products";
                adapter = new MySqlDataAdapter(query, connection);
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // Veritabanına yeni bir kayıt ekle
            // Örnek olarak, textBoxProductName, textBoxProductDescription ve textBoxProductPrice'tan alınsın
            string productName = textBoxProductName.Text;
            string productDescription = textBoxProductDescription.Text;
            decimal productPrice = Convert.ToDecimal(textBoxProductPrice.Text);

            try
            {
                connection.Open();
                string query = "INSERT INTO products (productName, productDescription, productPrice) VALUES (@productName, @productDescription, @productPrice)";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@productName", productName);
                command.Parameters.AddWithValue("@productDescription", productDescription);
                command.Parameters.AddWithValue("@productPrice", productPrice);
                command.ExecuteNonQuery();
                connection.Close();

                // DataGridView'e verileri tekrar yükle
                dataTable.Clear();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri eklenirken bir hata oluştu: " + ex.Message);
            }
        }

        // Silme, güncelleme ve diğer işlemler için benzer fonksiyonlar yazılabilir
    }
}
