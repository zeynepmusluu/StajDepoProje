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
    public partial class MüsteriEkle : Form
    {
        public MüsteriEkle()
        {
            InitializeComponent();
        }
        static string conString = "server=KBN-BT09; database=Depo; Integrated Security=True;";
        SqlConnection baglanti = new SqlConnection(conString);

        public void musteri()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();

                string kayit = "insert into müsteri(telefon,adres,ad,soyad,is_bilgisi) values (@telefon,@adres,@ad,@soyad,@is_bilgisi)";
                SqlCommand komut = new SqlCommand(kayit, baglanti);

                komut.Parameters.AddWithValue("@telefon", textBox1.Text);
                komut.Parameters.AddWithValue("@adres", textBox2.Text);
                komut.Parameters.AddWithValue("@ad", textBox3.Text);
                komut.Parameters.AddWithValue("@soyad", textBox4.Text);
                komut.Parameters.AddWithValue("@is_bilgisi", textBox5.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Müşteri Kayıt İşlemi Gerçekleşti.");
                this.Close();

            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            musteri();
        }
    }
}
