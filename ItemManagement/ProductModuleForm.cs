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

namespace ItemManagement
{
    public partial class ProductModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public ProductModuleForm()
        {
            InitializeComponent();
            LoadCategory();
        }

        public void LoadCategory() 
        {
            comboCat.Items.Clear();
            cmd = new SqlCommand("SELECT catname FROM tbCategory", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read()) 
            {
                comboCat.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this product?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO tbProduct(pname,pqty,pprice,pdescription,pcategory)VALUES(@pname, @pqty, @pprice, @pdescription, @pcategory)", con);
                    cmd.Parameters.AddWithValue("@pname", textPName.Text);
                    cmd.Parameters.AddWithValue("@pqty", Convert.ToInt16(textPQty.Text));
                    cmd.Parameters.AddWithValue("@pprice", Convert.ToInt16(textPPrice.Text));
                    cmd.Parameters.AddWithValue("@pdescription", textPDes.Text);
                    cmd.Parameters.AddWithValue("@pcategory", comboCat.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been successfully saved!");
                    Clear();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void Clear() 
        { 
            textPName.Clear();
            textPQty.Clear();
            textPPrice.Clear();
            textPDes.Clear();
            comboCat.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this product?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE tbProduct SET pname=@pname, pqty=@pqty, pprice=@pprice, pdescription=@pdescription, pcategory=@pcategory WHERE pid LIKE '" + lblPid.Text + "' ", con);
                    cmd.Parameters.AddWithValue("@pname", textPName.Text);
                    cmd.Parameters.AddWithValue("@pqty", Convert.ToInt16(textPQty.Text));
                    cmd.Parameters.AddWithValue("@pprice", Convert.ToInt16(textPPrice.Text));
                    cmd.Parameters.AddWithValue("@pdescription", textPDes.Text);
                    cmd.Parameters.AddWithValue("@pcategory", comboCat.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been successfully updated!");
                    this.Dispose();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
