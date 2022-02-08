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
using System.Drawing.Printing;



namespace WindowsFormsApp27
{
    public partial class Form5 : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source= Data.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataAdapter adtr = new OleDbDataAdapter();
        DataSet ds = new DataSet();
        public Form5()
        {
            InitializeComponent();
        }
        void ac()
        {
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * from otopark2", baglanti);
            adtr.Fill(ds, "otopark2");
            dataGridView1.DataSource = ds.Tables["otopark2"];
            adtr.Dispose();
            baglanti.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            komut = new OleDbCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "update otopark2 set adsoyad='" + textBox2.Text + "', plaka='" + textBox3.Text + "', tel='" + textBox4.Text + "', girissaati='" +textBox5.Text+ "' where kimlik=" + textBox1.Text + "";

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("kayıt güncellendi");
            ds.Clear();
            ac();




        }

        private void button3_Click(object sender, EventArgs e)
        {
           baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "Delete from otopark2 where Kimlik=" + textBox1.Text + "";
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
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            ac();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form = new Form4();
            form.Show();
            this.Hide();
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime zaman = DateTime.Now;
            label6.Text = zaman.ToShortTimeString();
            textBox6.Text = DateTime.Now.ToLongTimeString();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DateTime giris, cikis;
            giris = DateTime.Parse(textBox5.Text);
            cikis = DateTime.Parse(textBox6.Text);
            TimeSpan fark;
            fark = cikis - giris;
            label7.Text = fark.TotalHours.ToString("0.00");
            label8.Text = (double.Parse(label7.Text) * (7)).ToString("0.00");

            textBox7.Text = textBox3.Text + Environment.NewLine + textBox5.Text + Environment.NewLine + label6.Text + Environment.NewLine + label8.Text;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Fiş yazdırma kodları

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            pd.Print();
        }
        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {

            Font print_font = new Font("Times New Roman", 12);
            e.Graphics.DrawString(textBox7.Text, print_font, Brushes.Black, 0, 0);

        }
    }
}
