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



namespace CRUDMahasiswaADO
{
    public partial class FormMahasiswa : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString = "Data Source=DESKTOP-9K7QG8P;Initial Catalog=DB_Mahasiswa;Integrated Security=True";
        public FormMahasiswa()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);

        }

        private void ConnectDatabase()
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                MessageBox.Show("Koneksi berhasil!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi gagal: " + ex.Message);
            }
       
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectDatabase();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                dataGridView1.Columns.Add("NIM", "NIM");
                dataGridView1.Columns.Add("Nama", "Nama");
                dataGridView1.Columns.Add("JenisKelamin", "Jenis Kelamin");
                dataGridView1.Columns.Add("TanggalLahir", "Tanggal Lahir");
                dataGridView1.Columns.Add("Alamat", "Alamat");
                dataGridView1.Columns.Add("KodeProdi", "Kode Prodi");


                string query = "SELECT * FROM Mahasiswa";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(
                        reader["NIM"].ToString(), 
                        reader["Nama"].ToString(), 
                        reader["JenisKelamin"].ToString(), 
                        Convert.ToDateTime(reader["TanggalLahir"]).ToString("yyyy-MM-dd"), 
                        reader["Alamat"].ToString(), 
                        reader["KodeProdi"].ToString());
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan data: " + ex.Message);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (txtNIM.Text == "")
                {
                    MessageBox.Show("NIM tidak boleh kosong.");
                    txtNIM.Focus();
                    return;
                }

                if (txtNama.Text == "")
                {
                    MessageBox.Show("Nama tidak boleh kosong.");
                    txtNama.Focus();
                    return;
                }

                if (cmbJK.Text == "")
                {
                    MessageBox.Show("Jenis Kelamin harus dipilih.");
                    cmbJK.Focus();
                    return;
                }

                if (txtKodeProdi.Text == "")
                {
                    MessageBox.Show("Kode Prodi tidak boleh kosong.");
                    txtKodeProdi.Focus();
                    return;
                }
                string query = "INSERT INTO Mahasiswa (NIM, Nama, JenisKelamin, TanggalLahir, Alamat, KodeProdi) " +
                               "VALUES (@NIM, @Nama, @JenisKelamin, @TanggalLahir, @Alamat, @KodeProdi)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NIM", textBox1.Text);
                cmd.Parameters.AddWithValue("@Nama", textBox2.Text);
                cmd.Parameters.AddWithValue("@JenisKelamin", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@TanggalLahir", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@Alamat", textBox3.Text);
                cmd.Parameters.AddWithValue("@KodeProdi", txtKodeProdi.Text);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Data berhasil ditambahkan!");
                    btnLoad.PerformClick(); // Refresh data grid
                }
                else
                {
                    MessageBox.Show("Gagal menambahkan data.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
