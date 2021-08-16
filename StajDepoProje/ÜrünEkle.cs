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
    public partial class ÜrünEkle : Form
    {
        public ÜrünEkle()
        {
            InitializeComponent();
        }
        static string conString = "server=KBN-BT09; database=Depo; Integrated Security=True;";
        SqlConnection baglanti = new SqlConnection(conString);

        bool durum;

        private void barkodkontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from urun", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (textBox2.Text == read["barkod_no"].ToString() || textBox2.Text == "")
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }

        private void kategorigetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kategoribilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["kategori"].ToString());
            }
            baglanti.Close();
        }

        private void ÜrünEkle_Load(object sender, EventArgs e)
        {
            kategorigetir();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            kategori();
        }

        public void barkod()
        {
            baglanti.Open();

            string kayit = "insert into urun(kategori, model, marka, barkod_no, alis_fiyati, satis_fiyat, miktar, urun_adi) values(@kategori, @model, @marka, @barkod_no, @alis_fiyati, @satis_fiyat, @miktar, @urun_adi)";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            komut.Parameters.AddWithValue("@kategori", comboBox1.Text);
            komut.Parameters.AddWithValue("@model", textBox1.Text);
            komut.Parameters.AddWithValue("@marka", comboBox2.Text);
            komut.Parameters.AddWithValue("@barkod_no", textBox2.Text);
            komut.Parameters.AddWithValue("@alis_fiyati", double.Parse(textBox5.Text));
            komut.Parameters.AddWithValue("@satis_fiyat", double.Parse(textBox6.Text));
            komut.Parameters.AddWithValue("@miktar", int.Parse(textBox3.Text));
            komut.Parameters.AddWithValue("@urun_adi", textBox4.Text);

            komut.ExecuteNonQuery();
            baglanti.Close();
        }
        public void barkodno()
        {
            
            SqlCommand komut = new SqlCommand("select * from urun where barkod_no like '" + textBox12.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox13.Text = read["kategori"].ToString();
                textBox11.Text = read["model"].ToString();
                textBox14.Text = read["marka"].ToString();
                textBox8.Text = read["alis_fiyati"].ToString();
                textBox7.Text = read["satis_fiyat"].ToString();
                label10.Text = read["miktar"].ToString();
                textBox9.Text = read["urun_adi"].ToString();
            }
            
        }
        public void ekle()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update urun set miktar=miktar + '" + int.Parse(textBox10.Text) + "'where barkod_no='" + textBox12.Text + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
        }
        public void kategori()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from markabilgileri where kategori='" + comboBox1.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox2.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ekle();
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            MessageBox.Show("Var Olan Ürüne Ekleme Yapıldı.");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)

                    barkodkontrol();
                if (durum == true)
                {

                    barkod();
                    MessageBox.Show("Ürün Kayıt İşlemi Gerçekleşti.");
                }
                else
                {
                    MessageBox.Show("Böyle Bir Barkod No Var", "uyarı");
                }

                comboBox2.Items.Clear();
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

            if (textBox12.Text == "")
            {
                label10.Text = "";
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
            barkodno();

        }
    }
}
