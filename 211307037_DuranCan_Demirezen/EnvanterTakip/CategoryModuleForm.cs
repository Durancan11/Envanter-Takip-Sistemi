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

namespace EnvanterTakip
{
    public partial class CategoryModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Duran Can\Documents\dbIMS.mdf"";Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        public CategoryModuleForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Kategori adı boş mu kontrol edilir
                if (string.IsNullOrEmpty(txtCatName.Text))
                {
                    MessageBox.Show("Kategori adı boş olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Aynı isimde kategori var mı kontrol edilir
                using (var con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Duran Can\Documents\dbIMS.mdf"";Integrated Security=True;Connect Timeout=30"))
                {
                    con.Open();

                    string checkCategoryQuery = "SELECT COUNT(*) FROM tbCategory WHERE catname = @catname";
                    using (var cmd = new SqlCommand(checkCategoryQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@catname", txtCatName.Text);

                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Bu isimde bir kategori zaten var!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // Aynı isimde kategori yoksa kaydedilir
                using (var con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Duran Can\Documents\dbIMS.mdf"";Integrated Security=True;Connect Timeout=30"))
                {
                    con.Open();

                    string insertCategoryQuery = "INSERT INTO tbCategory(catname) VALUES(@catname)";
                    using (var cmd = new SqlCommand(insertCategoryQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@catname", txtCatName.Text);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Kategori başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Clear()
        {
            txtCatName.Clear();
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
                if (MessageBox.Show("Bu kategoriyi güncellemek istediğine emin misin?", "Kayıt Güncellendi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                cm = new SqlCommand("UPDATE tbCategory SET catname=@catname WHERE catid LIKE '" + lblCatld.Text + "'", con);
                cm.Parameters.AddWithValue("@catname", txtCatName.Text);
                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kategori başarıyla güncellendi.");
                this.Dispose();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
