using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglantı = new NpgsqlConnection("server=localHost; port=5432; Database=vtysodev; user Id=postgres; password=12345");
        private void Form1_Load(object sender, EventArgs e)
        {
            baglantı.Open();
            NpgsqlDataAdapter d1 = new NpgsqlDataAdapter("select * from personel_tur", baglantı);
            DataTable dt = new DataTable();
            d1.Fill(dt);
            comboBox1.DisplayMember = "tur_ad";
            comboBox1.ValueMember = "tur_id";
            comboBox1.DataSource = dt;
            baglantı.Close();

            baglantı.Open();
            NpgsqlDataAdapter d2 = new NpgsqlDataAdapter("select * from personel_derecesi", baglantı);
            DataTable dy = new DataTable();
            d2.Fill(dy);
            comboBox2.DisplayMember = "personel_derece_ad";
            comboBox2.ValueMember = "personel_derece_id";
            comboBox2.DataSource = dy;
            baglantı.Close();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string listeleme = "select* from personel";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(listeleme, baglantı);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

        }

        private void button2_Click(object sender, EventArgs e)
        {

            baglantı.Open();
            NpgsqlCommand ekleme = new NpgsqlCommand("insert into personel(kisi_id,isim,soyad,sehirid,personel_tur_id,personel_derece_no)  values(@p1,@p2,@p3,@p4,@p5,@p6)", baglantı);

            ekleme.Parameters.AddWithValue("@p1", int.Parse(Txtpersonelid.Text));
            ekleme.Parameters.AddWithValue("@p2", Txt_personelad.Text);
            ekleme.Parameters.AddWithValue("@p3", Txtpersonelsoyad.Text);
            ekleme.Parameters.AddWithValue("@p4", int.Parse(Txt_sehirid.Text));
            ekleme.Parameters.AddWithValue("@p5", int.Parse(comboBox1.SelectedValue.ToString()));
            ekleme.Parameters.AddWithValue("@p6", int.Parse(comboBox2.SelectedValue.ToString()));

            ekleme.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("personel ekleme işlemi başarılı bir şekilde gerçekleştir");



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            NpgsqlCommand silme = new NpgsqlCommand("Delete from personel Where kisi_id =@p1", baglantı);
            silme.Parameters.AddWithValue("@p1", int.Parse(Txtpersonelid.Text));
            silme.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Silme İslemi Basariyla Tamamlandı", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            NpgsqlCommand guncelle = new NpgsqlCommand("Update personel set isim=@p1,soyad=@p2,sehirid=@p3,personel_tur_id=@p4,personel_derece_no=@p5 where kisi_id=@p6", baglantı);
            guncelle.Parameters.AddWithValue("@p1", Txt_personelad.Text);
            guncelle.Parameters.AddWithValue("@p2", Txtpersonelsoyad.Text);
            guncelle.Parameters.AddWithValue("@p3", int.Parse(Txt_sehirid.Text));
            guncelle.Parameters.AddWithValue("@p4", int.Parse(comboBox1.SelectedValue.ToString()));
            guncelle.Parameters.AddWithValue("@p5", int.Parse(comboBox2.SelectedValue.ToString()));
            guncelle.Parameters.AddWithValue("@p6", int.Parse(Txtpersonelid.Text));
          
            guncelle.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Guncelleme islemi tamamlandı", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
        }
    }
}
