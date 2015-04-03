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
    public partial class MembersOfFamily : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=FamilyFinances.accdb");
        OleDbCommand com = new OleDbCommand();
        OleDbDataAdapter ad = new OleDbDataAdapter();
        DataSet ds = new DataSet();

        public MembersOfFamily()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadDGV();
        }

        private void loadDGV() 
        {
            try
            {
                ad.SelectCommand = new OleDbCommand("SELECT * From family", con);
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

        private void buttonAddFM_Click(object sender, EventArgs e)
        {
            StreamReader FileR = new StreamReader("index.txt");
            string line = FileR.ReadLine();
            FileR.Close();

            int ID = Convert.ToInt16(line);
            ID = ID + 1;
            
            StreamWriter FileW = new StreamWriter("index.txt");
            line.Remove(0);
            line = ID.ToString();
            FileW.WriteLine(line);
            FileW.Close();

            if (textBoxFirstName.Text == String.Empty || textBoxLastName.Text == String.Empty || textBoxMiddleName.Text == String.Empty || textBoxStatys.Text == String.Empty)
            {
                MessageBox.Show("Please, enter all items into cells");
            }
            else
            {
                try
                {
                    ad.InsertCommand = new OleDbCommand("insert into family values(@ID,@FirstName,@LastName,@MiddleName,@Statys)", con);

                    ad.InsertCommand.Parameters.Add("@ID", OleDbType.VarChar).Value = ID;
                    ad.InsertCommand.Parameters.Add("@FirstName", OleDbType.VarChar).Value = textBoxFirstName.Text.ToString();
                    ad.InsertCommand.Parameters.Add("@LastName", OleDbType.VarChar).Value = textBoxLastName.Text.ToString();
                    ad.InsertCommand.Parameters.Add("@MiddleName", OleDbType.VarChar).Value = textBoxMiddleName.Text.ToString();
                    ad.InsertCommand.Parameters.Add("@Statys", OleDbType.VarChar).Value = textBoxStatys.Text.ToString();


                    con.Open();
                    ad.InsertCommand.ExecuteNonQuery();
                    con.Close();

                    textBoxFirstName.Text = String.Empty;
                    textBoxLastName.Text = String.Empty;
                    textBoxMiddleName.Text = String.Empty;
                    textBoxStatys.Text = String.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                MessageBox.Show("Item was successfully added");
            }
            loadDGV();
        }
}