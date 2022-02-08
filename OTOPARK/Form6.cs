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
    public partial class Form6 : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source= Data.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataAdapter adtr = new OleDbDataAdapter();
        DataSet ds = new DataSet();
        public Form6()
        {
            InitializeComponent();
        }
        void ac()
        {
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * from giris", baglanti);
            adtr.Fill(ds, "giris");
            adtr.Dispose();
            baglanti.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {

           komut = new OleDbCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "update giris set sifre='" + textBox2.Text +  "' where Kimlik=" + textBox3.Text + "";

           komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Şifre değitirildi");
            ds.Clear();
            ac();
           
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
            this.Hide();
        }
    }
}
