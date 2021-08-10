using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Controlwork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'northwindDataSet.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.northwindDataSet.Customers);

        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {

        }
      

   
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void customersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.customersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.northwindDataSet);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            sqlDataAdapter1.Update(northwindDataSet);
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void sqlDataAdapter1_RowUpdating(object sender, System.Data.SqlClient.SqlRowUpdatingEventArgs e)
        {
            NorthwindDataSet.CustomersRow CustRow = (NorthwindDataSet.CustomersRow)e.Row;
            DialogResult response = MessageBox.Show("Continue updating " + CustRow.CustomerID.ToString() + "?", "Continue Update?", MessageBoxButtons.YesNo);
            if (response == DialogResult.No)
            {
                e.Status = UpdateStatus.SkipCurrentRow;
            }

        }
        private void sqlDataAdapter1_RowUpdated(object sender, System.Data.SqlClient.SqlRowUpdatedEventArgs e)
        {
            NorthwindDataSet.CustomersRow CustRow = (NorthwindDataSet.CustomersRow)e.Row;
            MessageBox.Show(CustRow.CustomerID.ToString() + " has been updated");
            northwindDataSet.Customers.Clear();
            sqlDataAdapter1.Fill(northwindDataSet.Customers);

        }
        private void sqlDataAdapter1_FillError(object sender, FillErrorEventArgs e)
        {
            DialogResult response = MessageBox.Show("The following error occurred while Filling the DataSet: " + e.Errors.Message.ToString() + " Continue attempting to fill?", "FillError Encountered", MessageBoxButtons.YesNo);
            if (response == DialogResult.Yes)
            {
                e.Continue = true;
            }
            else
            {
                e.Continue = false;
            }


        }

        private void Addrow_Click(object sender, EventArgs e)
        {
            NorthwindDataSet.CustomersRow NewRow = (NorthwindDataSet.CustomersRow)northwindDataSet.Customers.NewRow();
            NewRow.CustomerID = "";
            NewRow.CompanyName = "";
            NewRow.ContactName = "";
            NewRow.ContactTitle = "";
            NewRow.Address = "";
            NewRow.City = "";
            NewRow.Region = "";
            NewRow.PostalCode = "";
            NewRow.Country = "";
            NewRow.Phone = "";
            NewRow.Fax = "";
            try
            {
                northwindDataSet.Customers.Rows.Add(NewRow);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Row Failed");
            }

        }
        private void Deleterow_Click(object sender, EventArgs e)
        {
            GetSelectedRow().Delete();
        }
        private NorthwindDataSet.CustomersRow GetSelectedRow()
        {
            String SelectedCustomerID = dataGridView1.CurrentRow.Cells["dataGridViewTextBoxColumn1"].Value.ToString();
            NorthwindDataSet.CustomersRow SelectedRow = northwindDataSet.Customers.FindByCustomerID(SelectedCustomerID);
            return SelectedRow;
        }
        private void UpdateRowVersionDisplay()
        {
            try
            {
                CurrentDRVTextBox.Text = GetSelectedRow()[dataGridView1.CurrentCell.OwningColumn.Name, DataRowVersion.Current].ToString();
            }
            catch (Exception ex)
            {
                CurrentDRVTextBox.Text = ex.Message;
            }
            try
            {
                OriginalDRVTextBox.Text = GetSelectedRow()[dataGridView1.CurrentCell.OwningColumn.Name, DataRowVersion.Original].ToString();
            }
            catch (Exception ex)
            {
                OriginalDRVTextBox.Text = ex.Message;
            }
            RowStateTextBox.Text = GetSelectedRow().RowState.ToString();
        }

        private void Update_Click(object sender, EventArgs e)
        {
            GetSelectedRow()[dataGridView1.CurrentCell.OwningColumn.Name] = CellValueTextBox.Text;
            UpdateRowVersionDisplay();

        }
        private void dataGridView_Click(object sender, DataGridViewCellEventArgs e)
        {
            CellValueTextBox.Text = dataGridView1.CurrentCell.Value.ToString();
            UpdateRowVersionDisplay();
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            GetSelectedRow().AcceptChanges();
            UpdateRowVersionDisplay();

        }

        private void Reject_Click(object sender, EventArgs e)
        {
            GetSelectedRow().RejectChanges();
            UpdateRowVersionDisplay();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
                DataGridViewTextBoxColumn LocationColumn = new DataGridViewTextBoxColumn();
                LocationColumn.Name = "LocationColumn";
                LocationColumn.HeaderText = "Location";
                LocationColumn.DataPropertyName = "Location";
                dataGridView1.Columns.Add(LocationColumn);

           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dataGridView1.Columns[0].Name);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Remove("LocationColumn");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CellValueTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
