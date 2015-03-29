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
		private void buttonAddCost_Click(object sender, EventArgs e)
        {
            StreamReader FileR = new StreamReader("indexCost.txt");
            string line = FileR.ReadLine();
            FileR.Close();

            int ID = Convert.ToInt16(line);
            ID = ID + 1;

            StreamWriter FileW = new StreamWriter("indexCost.txt");
            line.Remove(0);
            line = ID.ToString();
            FileW.WriteLine(line);
            FileW.Close();

            if (textBoxSubcategory.Text == String.Empty || textBoxDateCost.Text == String.Empty || textBoxAmount.Text == String.Empty || textBoxAmountMoney.Text == String.Empty || comboBoxFamilyMember.SelectedIndex == -1)
            {
                MessageBox.Show("Please, enter all items into cells");
            }
            else
            {
                try
                {
                    ad.InsertCommand = new OleDbCommand("insert into cost values(@ID,@Subcategory,@DateCost,@Amount,@AmountMoney,@FamilyMember)", con);

                    ad.InsertCommand.Parameters.Add("@ID", OleDbType.VarChar).Value = ID;
                    ad.InsertCommand.Parameters.Add("@Subcategory", OleDbType.VarChar).Value = textBoxSubcategory.Text.ToString();
                    ad.InsertCommand.Parameters.Add("@DateCost", OleDbType.VarChar).Value = textBoxDateCost.Text.ToString();
                    ad.InsertCommand.Parameters.Add("@Amount", OleDbType.VarChar).Value = textBoxAmount.Text.ToString();
                    ad.InsertCommand.Parameters.Add("@AmountMoney", OleDbType.VarChar).Value = textBoxAmountMoney.Text.ToString();
                    ad.InsertCommand.Parameters.Add("@FamilyMember", OleDbType.VarChar).Value = comboBoxFamilyMember.SelectedItem.ToString();


                    con.Open();
                    ad.InsertCommand.ExecuteNonQuery();
                    con.Close();

                    textBoxSubcategory.Text = String.Empty;
                    textBoxDateCost.Text = String.Empty;
                    textBoxAmount.Text = String.Empty;
                    textBoxAmountMoney.Text = String.Empty;
                    comboBoxFamilyMember.SelectedItem = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                MessageBox.Show("Item was successfully added");
            }
            loadDGV();
        }

        private void buttonDeleteCost_Click(object sender, EventArgs e)
        {
            try
            {
                string ID = (string)dataGridView1.CurrentRow.Cells[0].Value.ToString();
                try
                {
                    ad.DeleteCommand = new OleDbCommand("DELETE FROM cost WHERE (id = '" + ID + "');", con);
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

	