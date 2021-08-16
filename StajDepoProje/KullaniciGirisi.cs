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
    public partial class KullaniciGirisi : Form
    {
        static string conString = "server=KBN-BT09; database=Depo; Integrated Security=True;";
        SqlConnection baglanti = new SqlConnection(conString);

        public KullaniciGirisi()
        {
            InitializeComponent();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Şifre sifre = new Şifre();
            sifre.Show();
        }

        private void KullaniciGirisi_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kullanıcıadı = textBox1.Text;
            string şifre = textBox2.Text;


            baglanti.Open();
            SqlCommand command = baglanti.CreateCommand();
            command.CommandText = "SELECT * FROM kullanicigirisi where kullanıcı_adı='" + textBox1.Text + "' AND sifre='" + textBox2.Text + "'";
            SqlDataReader reader = command.ExecuteReader();

            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("Lütfen Boş Geçmeyiniz.");
            }
            else
            {
                if (reader.Read())
                {
                    MessageBox.Show("Tebrikler! Başarılı bir şekilde giriş yaptınız");
                    AnaEkran satis = new AnaEkran();
                    satis.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adını ve Şifrenizi kontrol ediniz.");
                }
            }
            baglanti.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Kayit kayit = new Kayit();
            kayit.Show();
        }
    }
}
