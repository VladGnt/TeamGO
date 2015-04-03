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
    public partial class Cost : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=FamilyFinances.accdb");
        OleDbCommand com = new OleDbCommand();
        OleDbDataAdapter ad = new OleDbDataAdapter();
        DataSet ds = new DataSet();

        public Cost()
        {
            InitializeComponent();
        }
		
		private void loadDGV()
        {
            try
            {
                ad.SelectCommand = new OleDbCommand("SELECT * From cost", con);
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
