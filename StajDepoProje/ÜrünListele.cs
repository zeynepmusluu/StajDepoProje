using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StajDepoProje
{
    public partial class ÜrünListele : Form
    {
        public ÜrünListele()
        {
            InitializeComponent();
        }

        static string conString = "server=KBN-BT09; database=Depo; Integrated Security=True;";
        SqlConnection baglanti = new SqlConnection(conString);
        DataSet daset = new DataSet();

        private void Urun_Göster()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select* from urun", baglanti);
            adtr.Fill(daset, "urun");
            dataGridView1.DataSource = daset.Tables["urun"];
            baglanti.Close();
        }

        private void ÜrünListele_Load(object sender, EventArgs e)
        {
            Urun_Göster();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["kategori"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["model"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["marka"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["barkod_no"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["alis_fiyati"].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells["satis_fiyat"].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells["miktar"].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells["urun_adi"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           Urun_Göster();
            Urunguncelle();
            MessageBox.Show(" Güncellendi. Sayfayı Yenileme butonuna tıklayarak yenileyiniz!");

            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }
        public void Urunguncelle()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update urun set kategori=@kategori,model=@model,marka=@marka,barkod_no=@barkod_no,alis_fiyati=@alis_fiyati,satis_fiyat=@satis_fiyat,miktar=@miktar, urun_adi=@urun_adi where barkod_no=@barkod_no", baglanti);
            komut.Parameters.AddWithValue("@kategori", textBox1.Text);
            komut.Parameters.AddWithValue("@model", textBox2.Text);
            komut.Parameters.AddWithValue("@marka", textBox3.Text);
            komut.Parameters.AddWithValue("@barkod_no", textBox4.Text);
            komut.Parameters.AddWithValue("@alis_fiyati", textBox5.Text);
            komut.Parameters.AddWithValue("@satis_fiyat", textBox6.Text);
            komut.Parameters.AddWithValue("@miktar", textBox7.Text);
            komut.Parameters.AddWithValue("@urun_adi", textBox8.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["urun"].Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sil();
            Urun_Göster();
            MessageBox.Show("Ürün Başarıyla Silindi");
        }
        public void sil()
        {
            baglanti.Open();
            DialogResult cikis = new DialogResult();
            cikis = MessageBox.Show("Devam etmek istiyormusunuz ?", "Uyarı", MessageBoxButtons.YesNo);
            if (cikis == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("delete from urun where barkod_no='" + dataGridView1.CurrentRow.Cells["barkod_no"].Value.ToString() + "'", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                daset.Tables["urun"].Clear();
            }
            if (cikis == DialogResult.No)
            {
                MessageBox.Show("Ürün Silinmedi.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ÜrünListele_Load(null, null);
        }
        public void Urun()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from urun where barkod_no like '" + textBox4.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox1.Text = read["kategori"].ToString();
                textBox2.Text = read["marka"].ToString();
                textBox3.Text = read["model"].ToString();
                textBox4.Text = read["barkod_no"].ToString();
                textBox5.Text = read["alis_fiyati"].ToString();
                textBox6.Text = read["satis_fiyat"].ToString();
                textBox7.Text = read["miktar"].ToString();
                textBox8.Text = read["urun_adi"].ToString();
            }
            baglanti.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
            }
            Urun();
        }
    }
}
