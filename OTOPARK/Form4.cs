using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApp27
{
    public partial class Form4 : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source= Data.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataAdapter adtr = new OleDbDataAdapter();
        DataSet ds = new DataSet();

        public Form4()
        {
            InitializeComponent();
        }
        void ac()
        {
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * from abone", baglanti);
            adtr.Fill(ds, "abone");
            dataGridView1.DataSource = ds.Tables["abone"];
            adtr.Dispose();
            baglanti.Close();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            komut = new OleDbCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "update abone set tc='" + textBox2.Text +  "', adsoyad='" + textBox3.Text  + "', plaka='" + textBox6.Text + "' where kimlik=" + textBox1.Text + "";

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("kayıt güncellendi");
            ds.Clear();
            ac();
        }

        private void button2_Click(object sender, EventArgs e)
        {
             DateTime saat = DateTime.Now;
              DateTime abone = DateTime.Now.AddMonths(1);
              textBox5.Text = abone.ToLongDateString();
              textBox4.Text = saat.ToLongDateString();


            

            komut = new OleDbCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "insert into abone (tc,adsoyad,baslangıc,bitis,plaka) values ('" + textBox2.Text + "','" + textBox3.Text + "','" + Convert.ToDateTime(textBox4.Text) + "','" + Convert.ToDateTime(textBox5.Text) + "','" + textBox6.Text + "')";
            komut.ExecuteNonQuery();
            komut.Dispose();
            ds.Clear();
            baglanti.Close();
            ac();



        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "Delete from abone where Kimlik=" + textBox1.Text + "";
            komut.ExecuteNonQuery();
            komut.Dispose();
            baglanti.Close();
            MessageBox.Show("kayıt silindi");
            ds.Clear();
            ac();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
           
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            ac();
            timer1.Enabled = true;
           
            


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime saat = DateTime.Now;
            label7.Text = saat.ToShortTimeString();
             
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
          
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 form = new Form5();
            form.Show();
            this.Hide();
        }
    }
}
