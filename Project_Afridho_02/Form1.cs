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

namespace Project_Afridho_02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\ACER\Documents\users.accdb");
            OleDbDataAdapter da = new OleDbDataAdapter("select count(*) from Login where Username='" + txtuser.Text + "' and Password='" + txtpass.Text + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                Form2 f2 = new Form2();
                f2.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Username atau Password anda salah !!!");
            }
        }
    }
}
