using MySql.Data.MySqlClient;

namespace AppProduk
{
    public partial class Form1 : Form
    {
        string connectionString = "Server=localhost;Database=demo;Uid=root;Pwd=;";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using MySqlConnection connection = new(connectionString);

            string query = "INSERT INTO tbl_produk (nama, harga, stok, kategori, set_aktif, gambar) VALUES (@nama, @harga, @stok, @kat, @akt, @pic)";
            using MySqlCommand command = new(query, connection);
            int aktif = 0;
            if (checkBox1.Checked)
            {
                aktif = 1;
            }
            command.Parameters.AddWithValue("@nama", textBox1.Text);
            command.Parameters.AddWithValue("@harga", Convert.ToSingle(textBox2.Text));
            command.Parameters.AddWithValue("@stok", numericUpDown1.Value);
            command.Parameters.AddWithValue("@kat", comboBox1.Text);
            command.Parameters.AddWithValue("@akt", aktif);
            command.Parameters.AddWithValue("@pic", openFileDialog1.FileName);

            connection.Open();
            command.ExecuteNonQuery();

            MessageBox.Show("Produk berhasil ditambahkan");
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            numericUpDown1.Value = 0;
            comboBox1.Text = string.Empty;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var img = openFileDialog1.ShowDialog();
            if (img == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            var img = openFileDialog1.ShowDialog();
            if (img == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new ();
            form2.ShowDialog();
        }
    }
}
