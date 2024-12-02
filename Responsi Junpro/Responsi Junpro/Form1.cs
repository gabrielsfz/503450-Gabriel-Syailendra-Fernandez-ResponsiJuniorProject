using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Responsi_Junpro
{
    public partial class Form1 : Form
    {
        private NpgsqlConnection conn;
        private string sql = null;

        private DataGridViewRow r;

        private string connstring = "Host=localhost;Port=5432;Username=postgres;Password=password;Database=responsi";

        public static NpgsqlCommand cmd;

        public DataTable dt;


        public Form1()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                conn.Open();
                dgvData.DataSource = null;
                sql = "select karyawan.nama, departemen.nama_dep, departemen.id_dep from karyawan JOIN departemen on karyawan.id_dep=departemen.id_dep";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                NpgsqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                dgvData.DataSource = dt;

                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "FAIL!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);

            LoadData();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"INSERT INTO karyawan (nama, id_dep) VALUES (:_nama, :_id_dep)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_nama", tbNama.Text);
                cmd.Parameters.AddWithValue("_id_dep", Int32.Parse(tbDep.Text));

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Data Users Berhasil Diinputkan", "Well Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Insert FAIL!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LoadData();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("Mohon pilih baris data yang akan diupdate", "Good",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Apakah benar anda ingin menghapus data " + r.Cells["nama"].Value.ToString()
                + " ?", "Hapus data terkonfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes
            )
                try
                {
                    conn.Open();
                    sql = @"DELETE FROM karyawan WHERE nama=:_nama";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_nama", r.Cells["nama"].Value.ToString());

                    int rowsAffected = cmd.ExecuteNonQuery();
                    
                    if(rowsAffected > 0)
                    {
                        MessageBox.Show("Data Users Berhasil Dihapus", "Well Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        conn.Close();
                        r = null;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex.Message, "Delete Fail!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            LoadData();
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0)
            {
                r = dgvData.Rows[e.RowIndex];
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {

        }
    }
}
