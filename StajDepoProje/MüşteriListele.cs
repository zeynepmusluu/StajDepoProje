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
    public partial class MüşteriListele : Form
    {
        public MüşteriListele()
        {
            InitializeComponent();
        }
        static string conString = "server=KBN-BT09; database=Depo; Integrated Security=True;";
        SqlConnection baglanti = new SqlConnection(conString);
        DataSet daset = new DataSet();

        private void MüşteriListele_Load(object sender, EventArgs e)
        {
            Kayıt_Göster();
        }
        private void Kayıt_Göster()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select* from müsteri", baglanti);
            adtr.Fill(daset, "müsteri");
            dataGridView1.DataSource = daset.Tables["müsteri"];
            baglanti.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["ad"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["soyad"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["adres"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["is_bilgisi"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kayıt_Göster();
            guncelle();
            MessageBox.Show("Müşteri Kayıt İşlemi Güncellendi. Sayfayı Yenileme butonuna tıklayarak yenileyiniz!");

            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sil();
            Kayıt_Göster();
            MessageBox.Show("Kayıt Başarıyla Silindi");
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            telefonara();
        }
       
        public void guncelle()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update müsteri set ad=@ad,soyad=@soyad,telefon=@telefon,adres=@adres,is_bilgisi=@is_bilgisi where telefon=@telefon", baglanti);
            komut.Parameters.AddWithValue("@telefon", textBox1.Text);
            komut.Parameters.AddWithValue("@adres", textBox4.Text);
            komut.Parameters.AddWithValue("@ad", textBox2.Text);
            komut.Parameters.AddWithValue("@soyad", textBox3.Text);
            komut.Parameters.AddWithValue("@is_bilgisi", textBox5.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["müsteri"].Clear();
        }
       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox4.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox5.Text = "";
            }
            musteri();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MüşteriListele_Load(null, null);
        }

        public void musteri()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from müsteri where telefon like '" + textBox1.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox4.Text = read["adres"].ToString();
                textBox2.Text = read["ad"].ToString();
                textBox3.Text = read["soyad"].ToString();
                textBox5.Text = read["is_bilgisi"].ToString();

            }
            baglanti.Close();
        }
        public void telefonara()
        {
            DataTable tablo = new DataTable();

            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from müsteri where telefon like '%" + textBox6.Text + "'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        public void sil()
        {
            baglanti.Open();
            DialogResult cikis = new DialogResult();
            cikis = MessageBox.Show("Devam etmek istiyormusunuz ?", "Uyarı", MessageBoxButtons.YesNo);
            if (cikis == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("delete from müsteri where telefon='" + dataGridView1.CurrentRow.Cells["telefon"].Value.ToString() + "'", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                daset.Tables["müsteri"].Clear();
            }
            if (cikis == DialogResult.No)
            {
                MessageBox.Show("Kayıt Silinmedi.");
            }
        }
    }
}
