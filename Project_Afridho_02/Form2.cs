using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Afridho_02
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtsearch.Text))

                    dataGridView.DataSource = inventoryBindingSource;
                else
                {
                    var query = from o in this.appData.Inventory
                                where o.Product_Name.Contains(txtsearch.Text) || o.Kategori == txtsearch.Text || o.Harga == txtsearch.Text
                                select o;
                    dataGridView.DataSource = query.ToList();
                }
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Yakin untuk menghapus data ini ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    inventoryBindingSource.RemoveCurrent();
            }
        }

        private void btncari_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|* .jpg", ValidateNames = true,Multiselect=false })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                        pictureBox.Image = Image.FromFile(ofd.FileName);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                panel.Enabled = true;
                txtproduct.Focus();
                this.appData.Inventory.AddInventoryRow(this.appData.Inventory.NewInventoryRow());
                inventoryBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inventoryBindingSource.ResetBindings(false);
            }
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            panel.Enabled = true;
            txtproduct.Focus();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            panel.Enabled = false;
            inventoryBindingSource.ResetBindings(false);
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                inventoryBindingSource.EndEdit();
                inventoryTableAdapter.Update(this.appData.Inventory);
                panel.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inventoryBindingSource.ResetBindings(false);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'appData.Inventory' table. You can move, or remove it, as needed.
            this.inventoryTableAdapter.Fill(this.appData.Inventory);
            inventoryBindingSource.DataSource = this.appData.Inventory;
        }
    }
}
