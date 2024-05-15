using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private const string connectionString = "Data Source=your_server;Initial Catalog=your_database;User ID=your_username;Password=your_password";
        private SqlConnection connection;
        private SqlDataAdapter dataAdapter;
        private DataTable dataTable;

        public Form1()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            dataAdapter = new SqlDataAdapter("SELECT * FROM your_table", connection);
            dataTable = new DataTable();

            // Verileri yükle
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                connection.Open();
                dataAdapter.Fill(dataTable);
                connection.Close();
                dataGridView1.DataSource = dataTable; // DataGridView'e verileri yükle
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        private void InsertData()
        {
            // Yeni bir kayıt ekle
            // Örneğin, kullanıcı adı ve e-posta adresi bir TextBox'tan alınsın
            string username = textBoxUsername.Text;
            string email = textBoxEmail.Text;

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO your_table (Username, Email) VALUES (@Username, @Email)", connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);
                command.ExecuteNonQuery();
                connection.Close();

                // Yeni verileri yükle
                dataTable.Clear();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri eklenirken bir hata oluştu: " + ex.Message);
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            InsertData();
        }

        private void DeleteData()
        {
            // Seçili satırı sil
            int selectedRowIndex = dataGridView.CurrentCell.RowIndex;
            int idToDelete = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["ID"].Value);


            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM your_table WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", idToDelete);
                command.ExecuteNonQuery();
                connection.Close();

                // Veriyi tekrar yükle
                dataTable.Clear();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri silinirken bir hata oluştu: " + ex.Message);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void UpdateData()
        {
            // Seçili satırı güncelle
            int selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            int idToUpdate = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["ID"].Value);
            string newUsername = textBoxNewUsername.Text;
            string newEmail = textBoxNewEmail.Text;

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE your_table SET Username = @Username, Email = @Email WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@Username", newUsername);
                command.Parameters.AddWithValue("@Email", newEmail);
                command.Parameters.AddWithValue("@ID", idToUpdate);
                command.ExecuteNonQuery();
                connection.Close();

                // Veriyi tekrar yükle
                dataTable.Clear();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri güncellenirken bir hata oluştu: " + ex.Message);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            UpdateData();
        }
    }
}
