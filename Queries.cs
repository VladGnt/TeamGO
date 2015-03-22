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
    public partial class Queries : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=FamilyFinances.accdb");
        OleDbCommand com = new OleDbCommand();
        OleDbDataAdapter ad1 = new OleDbDataAdapter();
        DataSet ds1 = new DataSet();
        OleDbDataAdapter ad2 = new OleDbDataAdapter();
        DataSet ds2 = new DataSet();
        OleDbDataAdapter ad3 = new OleDbDataAdapter();
        DataSet ds3 = new DataSet();

        public Queries()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void loadDGV1()
        {
            try
            {
                ad1.SelectCommand = new OleDbCommand("SELECT profit.ID, family.FirstName, family.LastName, Sum(profit.Income) AS [Sum-Income] FROM profit INNER JOIN family ON profit.ID = family.ID GROUP BY profit.ID, family.FirstName, family.LastName", con);
                ds1.Clear();
                ad1.Fill(ds1);
                dataGridView2.DataSource = ds1.Tables[0];
                con.Open();
                ad1.SelectCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadDGV2()
        {
            try
            {
                ad2.SelectCommand = new OleDbCommand("SELECT family.ID, family.FirstName, family.LastName, Sum(cost.AmountMoney) AS [Sum-AmountMoney] FROM cost INNER JOIN family ON cost.ID = family.ID GROUP BY family.ID, family.FirstName, family.LastName", con);
                ds2.Clear();
                ad2.Fill(ds2);
                dataGridView2.DataSource = ds2.Tables[0];
                con.Open();
                ad2.SelectCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadDGV3()
        {
            try
            {
                ad3.SelectCommand = new OleDbCommand("SELECT family.ID, family.FirstName, family.LastName, Sum(profit.Income) AS [Sum-Income], Sum(cost.AmountMoney) AS [Sum-AmountMoney] FROM profit INNER JOIN (cost INNER JOIN family ON cost.ID = family.ID) ON profit.ID = family.ID GROUP BY family.ID, family.FirstName, family.LastName", con);
                ds3.Clear();
                ad3.Fill(ds3);
                dataGridView2.DataSource = ds3.Tables[0];
                con.Open();
                ad3.SelectCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Queries_Load(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                loadDGV1();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                loadDGV2();
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                loadDGV3();
            }
        }
    }
}
