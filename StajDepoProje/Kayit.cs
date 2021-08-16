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
    public partial class Kayit : Form
    {
        static string conString = "server=KBN-BT09; database=Depo; Integrated Security=True;";
        SqlConnection baglanti = new SqlConnection(conString);

        public Kayit()
        {
            InitializeComponent();
        }

        public void giris()
        {

            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            string kayit = "insert into kullanicigirisi(adsoyad,kullanıcı_adı,sifre,soru,cevap) values (@adsoyad,@kullanıcı_adı,@sifre,@soru,@cevap)";
            SqlCommand komut = new SqlCommand(kayit, baglanti);

            komut.Parameters.AddWithValue("@adsoyad", textBox1.Text);
            komut.Parameters.AddWithValue("@kullanıcı_adı", textBox2.Text);
            komut.Parameters.AddWithValue("@sifre", textBox3.Text);
            komut.Parameters.AddWithValue("@soru", textBox4.Text);
            komut.Parameters.AddWithValue("@cevap", textBox5.Text);

            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                giris();
                MessageBox.Show("Kullanıcı Kayıt İşlemi Gerçekleşti.");
                this.Hide();
                KullaniciGirisi kullanicigirisi = new KullaniciGirisi();
                kullanicigirisi.Show();
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }
    }
}
