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
    public partial class CustomerModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Duran Can\Documents\dbIMS.mdf"";Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        public CustomerModuleForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Boş alan kontrolü
                if (string.IsNullOrWhiteSpace(txtCName.Text) || string.IsNullOrWhiteSpace(txtCPhone.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bu müşteriyi kayıt etmek istediğine emin misin?", "Kayıt Ediliyor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Aynı telefon numarasından varsa kontrol
                    SqlCommand checkCustomerCmd = new SqlCommand("SELECT COUNT(*) FROM tbCustomer WHERE cphone = @cphone", con);
                    checkCustomerCmd.Parameters.AddWithValue("@cphone", txtCPhone.Text);
                    con.Open();
                    int existingCustomerCount = (int)checkCustomerCmd.ExecuteScalar();
                    con.Close();

                    if (existingCustomerCount > 0)
                    {
                        MessageBox.Show("Bu telefon numarası zaten kayıtlı. Lütfen farklı bir telefon numarası deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Yeni müşteri ekleme
                    SqlCommand insertCustomerCmd = new SqlCommand("INSERT INTO tbCustomer(cname,cphone)VALUES(@cname,@cphone)", con);
                    insertCustomerCmd.Parameters.AddWithValue("@cname", txtCName.Text);
                    insertCustomerCmd.Parameters.AddWithValue("@cphone", txtCPhone.Text);

                    con.Open();
                    insertCustomerCmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Kullanıcı başarıyla kaydedildi.");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            txtCName.Clear();
            txtCPhone.Clear();
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

            if (MessageBox.Show("Bu müşteriyi güncellemek istediğine emin misin?", "Kayıt Güncellendi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                cm = new SqlCommand("UPDATE tbCustomer SET cname=@cname,cphone=@cphone WHERE cid LIKE '" + lblCld.Text + "'", con);
            cm.Parameters.AddWithValue("@cname", txtCName.Text);
            cm.Parameters.AddWithValue("@cphone", txtCPhone.Text);
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Müşteri başarıyla güncellendi.");
            this.Dispose();
        }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
