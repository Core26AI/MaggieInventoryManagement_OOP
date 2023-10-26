﻿using System;
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
    public partial class dgvProductForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public dgvProductForm()
        {
            InitializeComponent();
            LoadProduct();
        }
        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT (pid, pname, pprice, pdescription, pcategory) LIKE '%"+textSearch.Text+"%' ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            con.Close();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductModuleForm formModule = new ProductModuleForm();
            formModule.btnSave.Enabled = true;
            formModule.btnUpdate.Enabled = false;
            formModule.ShowDialog();
            LoadProduct();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgvProductFrom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                ProductModuleForm productModule = new ProductModuleForm();
                productModule.lblPid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                productModule.textPName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                productModule.textPQty.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                productModule.textPPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                productModule.textPDes.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                productModule.comboCat.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();

                productModule.btnSave.Enabled = false;
                productModule.btnUpdate.Enabled = true;
                productModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this product?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("DELETE FROM tbProduct WHERE pid LIKE '" + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted");
                }
            }
            LoadProduct();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }
    }
}
