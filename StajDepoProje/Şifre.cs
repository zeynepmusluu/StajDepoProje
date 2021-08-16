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
    public partial class Şifre : Form
    {
        static string conString = "server=KBN-BT09; database=Depo; Integrated Security=True;";
        SqlConnection baglanti = new SqlConnection(conString);
        public Şifre()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            try
            {

            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Password();
            MessageBox.Show("Başarılı bir şekilde değişim yaptınız");
            KullaniciGirisi giris = new KullaniciGirisi();
            giris.Show();
            this.Hide();
        }
        public void Password()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();


            SqlCommand komut = new SqlCommand("Update kullanicigirisi Set sifre=@sifre Where kullanıcı_adı='" + textBox1.Text + "'", baglanti);
            komut.Parameters.AddWithValue("@sifre", textBox2.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
        }
    }
}
