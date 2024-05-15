using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EnvanterTakip
{
    public partial class UserModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Duran Can\Documents\dbIMS.mdf"";Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        public UserModuleForm()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Kullanıcı adı, tam ad, şifre ve telefon numarası boşsa uyarı mesajı göster
            if (string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtPass.Text) || string.IsNullOrWhiteSpace(txtRepass.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPass.Text != txtRepass.Text)
            {
                MessageBox.Show("Parolalar eşleşmiyor.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bu kullanıcıyı kayıt etmek istediğine emin misin?", "Kayıt Ediliyor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Duran Can\Documents\dbIMS.mdf"";Integrated Security=True;Connect Timeout=30")) // Improved connection handling
                {
                    con.Open();

                    string checkUsernameQuery = "SELECT COUNT(*) FROM tbUser WHERE username = @username";
                    SqlCommand checkUsernameCmd = new SqlCommand(checkUsernameQuery, con);
                    checkUsernameCmd.Parameters.AddWithValue("@username", txtUserName.Text);

                    int existingUserCount = (int)checkUsernameCmd.ExecuteScalar();

                    if (existingUserCount > 0)
                    {
                        MessageBox.Show("Bu kullanıcı adı zaten kayıtlı. Lütfen farklı bir kullanıcı adı deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string insertUserQuery = "INSERT INTO tbUser(username, fullname, password, phone) VALUES(@username, @fullname, @password, @phone)";
                    SqlCommand insertUserCmd = new SqlCommand(insertUserQuery, con);
                    insertUserCmd.Parameters.AddWithValue("@username", txtUserName.Text);
                    insertUserCmd.Parameters.AddWithValue("@fullname", txtFullName.Text);
                    insertUserCmd.Parameters.AddWithValue("@password", txtPass.Text);
                    insertUserCmd.Parameters.AddWithValue("@phone", txtPhone.Text);

                    insertUserCmd.ExecuteNonQuery();

                    MessageBox.Show("Kullanıcı başarıyla kaydedildi.");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
        public void Clear()
        {
            txtUserName.Clear();
            txtFullName.Clear();
            txtPass.Clear();
            txtRepass.Clear();
            txtPhone.Clear();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPass.Text != txtRepass.Text)
                {
                    MessageBox.Show("Parolalar eşleşmiyor.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bu kullanıcıyı güncellemek istediğine emin misin?", "Kayıt Güncellendi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                cm = new SqlCommand("UPDATE tbUser SET fullname=@fullname,password=@password,phone=@phone WHERE username LIKE '"+txtUserName.Text+"'", con);
                cm.Parameters.AddWithValue("@fullname", txtFullName.Text);
                cm.Parameters.AddWithValue("@password", txtPass.Text);
                cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kullanıcı başarıyla güncellendi.");
                this.Dispose();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
