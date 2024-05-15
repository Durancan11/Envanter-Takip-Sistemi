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
    public partial class ProductModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Duran Can\Documents\dbIMS.mdf"";Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public ProductModuleForm()
        {
            InitializeComponent();
            LoadCategory();
        }
        public void LoadCategory()
        {
            comboCat.Items.Clear();
            cm = new SqlCommand("SELECT catname from tbCategory", con);
            con.Open();
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                comboCat.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Boş alan kontrolü
                if (string.IsNullOrWhiteSpace(txtPName.Text) || string.IsNullOrWhiteSpace(txtPQty.Text) ||
                    string.IsNullOrWhiteSpace(txtPPrice.Text) || string.IsNullOrWhiteSpace(txtPDes.Text) ||
                    string.IsNullOrWhiteSpace(comboCat.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bu ürünü kayıt etmek istediğine emin misin?", "Kayıt Ediliyor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Aynı üründen varsa kontrol
                    SqlCommand checkProductCmd = new SqlCommand("SELECT COUNT(*) FROM tbProduct WHERE pname = @pname", con);
                    checkProductCmd.Parameters.AddWithValue("@pname", txtPName.Text);
                    con.Open();
                    int existingProductCount = (int)checkProductCmd.ExecuteScalar();
                    con.Close();

                    if (existingProductCount > 0)
                    {
                        MessageBox.Show("Bu ürün zaten kayıtlı. Lütfen farklı bir ürün adı deneyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Yeni ürün ekleme
                    SqlCommand insertProductCmd = new SqlCommand("INSERT INTO tbProduct(pname,pqty,pprice,pdescription,pcategory)VALUES(@pname,@pqty,@pprice,@pdescription,@pcategory)", con);
                    insertProductCmd.Parameters.AddWithValue("@pname", txtPName.Text);
                    insertProductCmd.Parameters.AddWithValue("@pqty", Convert.ToInt32(txtPQty.Text));
                    insertProductCmd.Parameters.AddWithValue("@pprice", Convert.ToInt32(txtPPrice.Text));
                    insertProductCmd.Parameters.AddWithValue("@pdescription", txtPDes.Text);
                    insertProductCmd.Parameters.AddWithValue("@pcategory", comboCat.Text);

                    con.Open();
                    insertProductCmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Ürün başarıyla kaydedildi.");
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
            txtPName.Clear();
            txtPQty.Clear();
            txtPPrice.Clear();
            txtPDes.Clear();
            comboCat.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bu ürünü güncellemek istediğine emin misin?", "Ürüc Güncellendi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                cm = new SqlCommand("UPDATE tbProduct SET pname=@pname,pqty=@pqty,pprice=@pprice, pdescription=@pdescription, pcategory=@pcategory WHERE pid LIKE '" + lblPid.Text + "'", con);
                cm.Parameters.AddWithValue("@pname", txtPName.Text);
                cm.Parameters.AddWithValue("@pqty", Convert.ToInt32(txtPQty.Text));
                cm.Parameters.AddWithValue("@pprice", Convert.ToInt32(txtPPrice.Text));
                cm.Parameters.AddWithValue("@pdescription", txtPDes.Text);
                cm.Parameters.AddWithValue("@pcategory", comboCat.Text);
                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Ürün başarıyla güncellendi.");
                this.Dispose();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
