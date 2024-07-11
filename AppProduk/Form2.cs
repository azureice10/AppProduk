using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AppProduk
{
    public partial class Form2 : Form
    {
        string connectionString = "Server=localhost;Database=demo;Uid=root;Pwd=;";

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData(string searchQuery = "")
        {
            using MySqlConnection connection = new(connectionString);

            string query = "SELECT * FROM tbl_produk";

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query += " WHERE nama LIKE @searchQuery";
            }

            using MySqlDataAdapter adapter = new(query, connection);

            if (!string.IsNullOrEmpty(searchQuery))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");
            }

            DataTable dataTable = new();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["id_produk"].Value.ToString();
                textBox2.Text = row.Cells["nama"].Value.ToString();
                textBox3.Text = row.Cells["harga"].Value.ToString();
                numericUpDown1.Value = Convert.ToInt16(row.Cells["stok"].Value.ToString());
                comboBox1.Text = row.Cells["kategori"].Value.ToString();
                pictureBox1.ImageLocation = row.Cells["gambar"].Value.ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using MySqlConnection connection = new(connectionString);
            {
                string query = "DELETE FROM tbl_produk WHERE id_produk=@id";
                using MySqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@id", Convert.ToInt32(textBox1.Text));
                connection.Open();
                command.ExecuteNonQuery();
            }
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new(connectionString))
            {
                int aktif = 0;
                if (checkBox1.Checked)
                {
                    aktif = 1;
                }
                string query = "UPDATE tbl_produk SET nama=@nama, harga=@harga, stok=@stok,set_aktif=@aktif, gambar=@gambar WHERE id_produk=@id";
                using MySqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@id", Convert.ToInt32(textBox1.Text));
                command.Parameters.AddWithValue("@nama", textBox2.Text);
                command.Parameters.AddWithValue("@harga", Convert.ToDecimal(textBox3.Text));
                command.Parameters.AddWithValue("@stok", numericUpDown1.Value);
                command.Parameters.AddWithValue("@aktif", aktif);
                command.Parameters.AddWithValue("@gambar", pictureBox1.ImageLocation);
                connection.Open();
                command.ExecuteNonQuery();
            }
            LoadData();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var img = openFileDialog1.ShowDialog();
            if (img == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBox4.Text;
            LoadData(keyword);
        }
    }
}
