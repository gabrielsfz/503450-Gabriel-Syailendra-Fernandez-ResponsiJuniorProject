using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Responsi_Junpro
{
    public class Data
    {
        private NpgsqlConnection conn;
        private string sql = null;

        private DataGridViewRow r;
        
        public string connstring = "Host=localhost;Port=5432;Username=postgres;Password=password;Database=responsi";
        
        public static NpgsqlCommand cmd;
        public DataTable dt;

        public void Load()
        {
        }
    }
}
