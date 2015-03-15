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
	private void buttonDeleteFM_Click(object sender, EventArgs e)
        {
            try
            {
                string ID = (string)dataGridView1.CurrentRow.Cells[0].Value.ToString();
                try
                {
                    ad.DeleteCommand = new OleDbCommand("DELETE FROM family WHERE (id = '" + ID + "');", con);
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

        private void OpenProfit_Click(object sender, EventArgs e)
        {
            Profit P = new Profit();
            P.ShowDialog();
        }

        private void OpenCost_Click(object sender, EventArgs e)
        {
            Cost C = new Cost();
            C.ShowDialog();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Queries_Click(object sender, EventArgs e)
        {
            Queries Q = new Queries();
            Q.ShowDialog();
        }
    }
}
