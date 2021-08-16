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

namespace StajDepoProje
{
    public partial class AnaEkran : Form
    {
        public AnaEkran()
        {
            InitializeComponent();
        }

        static string conString = "server=KBN-BT09; database=Depo; Integrated Security=True;";
        SqlConnection baglanti = new SqlConnection(conString);
        DataSet daset = new DataSet();

        private void AnaEkran_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from urun", baglanti);
            DataSet daset = new DataSet();
            adtr.Fill(daset, "urun");
            dataGridView1.DataSource = daset.Tables["urun"];
            baglanti.Close();
        }


        private void btnMüşteriEkleme_Click(object sender, EventArgs e)
        {
            MüsteriEkle ekle = new MüsteriEkle();
            ekle.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ÜrünEkle ekle = new ÜrünEkle();
            ekle.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MüşteriListele listele = new MüşteriListele();
            listele.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Kategori kategori = new Kategori();
            kategori.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Marka marka = new Marka();
            marka.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VerileriGöster verigöster = new VerileriGöster();
            verigöster.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {

            ÜrünListele listele = new ÜrünListele();
            listele.ShowDialog();
        }

       

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
                if (txtBarkodNo.Text == "")
                {
                    foreach (Control item in groupBox2.Controls)
                    {
                        if (item is TextBox)
                        {
                            if (item != txtMiktarı)
                            {
                                item.Text = "";
                            }
                        }
                    }
                }
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select * from urun where barkod_no like '" + txtBarkodNo.Text + "'", baglanti);
                SqlDataReader read = komut.ExecuteReader();
                while (read.Read())
                {
                    txtModel.Text = read["model"].ToString();
                    txtMarka.Text = read["marka"].ToString();
                    txtKategori.Text = read["kategori"].ToString();
                    txtMiktarı.Text = read["miktar"].ToString();
                    txtÜrünAdi.Text = read["urun_adi"].ToString();
                    txtAlısFiyatı.Text = read["alis_fiyati"].ToString();
                    txtSatısFiyatı.Text = read["satis_fiyat"].ToString();

                }
                baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

                AnaEkran_Load(null, null);
        }

        private void txtTelefon_TextChanged(object sender, EventArgs e)
        {
          
                if (txtTelefon.Text == "")
                {
                    txtAdres.Text = "";
                    txtAd.Text = "";
                    txtSoyad.Text = "";
                    txtİşBilgisi.Text = "";
                }
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select * from müsteri where telefon like '" + txtTelefon.Text + "'", baglanti);
                SqlDataReader read = komut.ExecuteReader();
                while (read.Read())
                {
                    txtAdres.Text = read["adres"].ToString();
                    txtAd.Text = read["ad"].ToString();
                    txtSoyad.Text = read["soyad"].ToString();
                    txtİşBilgisi.Text = read["is_bilgisi"].ToString();

                }
                baglanti.Close();
            
        }
    }
}
