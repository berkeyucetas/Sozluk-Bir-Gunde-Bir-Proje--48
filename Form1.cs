using System.Data.OleDb;
using System.Windows.Forms;
using System;

namespace Sözlük_Uygulaması
{
    public partial class Form1 : Form
    {
        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\bekoo\Documents\sozluk.accdb");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (OleDbCommand cmd = new OleDbCommand("SELECT ingilizce FROM sozluk", baglanti))
            {
                baglanti.Open();
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader[0].ToString());
                    }
                }
                baglanti.Close();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                using (OleDbCommand cmd = new OleDbCommand("SELECT turkce FROM sozluk WHERE ingilizce=@p1", baglanti))
                {
                    cmd.Parameters.AddWithValue("@p1", listBox1.SelectedItem.ToString());
                    baglanti.Open();
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            textBox2.Text = reader[0].ToString();
                        }
                    }
                    baglanti.Close();
                    textBox1.Text = listBox1.SelectedItem.ToString();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            using (OleDbCommand cmd = new OleDbCommand("SELECT ingilizce FROM sozluk WHERE ingilizce LIKE @p1", baglanti))
            {
                cmd.Parameters.AddWithValue("@p1", textBox1.Text + "%");
                baglanti.Open();
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader[0].ToString());
                    }
                }
                baglanti.Close();
            }
        }
    }
}
