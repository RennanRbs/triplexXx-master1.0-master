using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace metodosMySql
{
    public partial class Form2 : Form
    {

        MySqlConnection conectar = new MySqlConnection("Server=lamp01ppgcc.ddns.net; Database=cadastro_bolsistas_lit; Uid=controlador_lit; Pwd=123qwe!@#");
        
        public Form2()
        {
            InitializeComponent();
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            MySqlCommand tabela = new MySqlCommand("Select cod_lit, nome From pessoa",conectar);
            adapter.SelectCommand = tabela;
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

       
    }
}
