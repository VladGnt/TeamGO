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
using System.IO;

namespace FamilyFinances
{
    public partial class Profit : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=FamilyFinances.accdb");
        OleDbCommand com = new OleDbCommand();
        OleDbDataAdapter ad = new OleDbDataAdapter();
        DataSet ds = new DataSet();

        public Profit()
        {
            InitializeComponent();
        }

        private void Profit_Load(object sender, EventArgs e)
        {
            loadDGV();
            try
            {
                comboBoxIDM.Items.Clear();
                con.Open();
                OleDbDataAdapter dbAdapter1 = new OleDbDataAdapter("SELECT ID From family",con);
                DataTable dataTable = new DataTable();
                dbAdapter1.Fill(dataTable);
                con.Close();
                DataRow[] result = dataTable.Select();
                foreach (var row in result)
                {
                    comboBoxIDM.Items.Add(row[0].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadDGV()
        {
            try
            {
                ad.SelectCommand = new OleDbCommand("SELECT * From profit", con);
                ds.Clear();
                ad.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                con.Open();
                ad.SelectCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonAddProfit_Click(object sender, EventArgs e)
        {
            StreamReader FileR = new StreamReader("indexProfit.txt");
            string line = FileR.ReadLine();
            FileR.Close();

            int ID = Convert.ToInt16(line);
            ID = ID + 1;

            StreamWriter FileW = new StreamWriter("indexProfit.txt");
            line.Remove(0);
            line = ID.ToString();
            FileW.WriteLine(line);
            FileW.Close();

            if (comboBoxIDM.SelectedIndex == -1 || textBoxDateOperation.Text == String.Empty || textBoxIncome.Text == String.Empty)
            {
                MessageBox.Show("Please, enter all items into cells");
            }
            else
            {
                try
                {
                    ad.InsertCommand = new OleDbCommand("insert into profit values(@ID,@IDM,@DateOperation,@Income)", con);

                    ad.InsertCommand.Parameters.Add("@ID", OleDbType.VarChar).Value = ID;
                    ad.InsertCommand.Parameters.Add("@IDM", OleDbType.VarChar).Value = comboBoxIDM.SelectedItem.ToString();
                    ad.InsertCommand.Parameters.Add("@DateOperation", OleDbType.VarChar).Value = textBoxDateOperation.Text.ToString();
                    ad.InsertCommand.Parameters.Add("@Income", OleDbType.VarChar).Value = textBoxIncome.Text.ToString();


                    con.Open();
                    ad.InsertCommand.ExecuteNonQuery();
                    con.Close();

                    comboBoxIDM.SelectedItem = -1;
                    textBoxDateOperation.Text = String.Empty;
                    textBoxIncome.Text = String.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                MessageBox.Show("Item was successfully added");
            }
            loadDGV();
        }

        private void buttonDeleteProfit_Click(object sender, EventArgs e)
        {
            try
            {
                string ID = (string)dataGridView1.CurrentRow.Cells[0].Value.ToString();
                try
                {
                    ad.DeleteCommand = new OleDbCommand("DELETE FROM profit WHERE (id = '" + ID + "');", con);
                    con.Open();
                    ad.DeleteCommand.ExecuteNonQuery();
                    con.Close();
                    loadDGV();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Item not selected");
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OpenMembers_Click(object sender, EventArgs e)
        {
            MembersOfFamily M = new MembersOfFamily();
            M.ShowDialog();
        }
    }
}
