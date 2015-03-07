using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FamilyFinances
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Are you sure?", "Exit", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    {
                        Application.Exit();
                        break;
                    }

                case DialogResult.No:
                    {
                        break;
                    }
            }
        }

        private void OpenProfit_Click(object sender, EventArgs e)
        {
            Profit P = new Profit();
            P.ShowDialog();
            
        }

        private void MembersOfFamily_Click(object sender, EventArgs e)
        {
            MembersOfFamily M = new MembersOfFamily();
            M.ShowDialog();
        }

        private void OpenCost_Click(object sender, EventArgs e)
        {
            Cost C = new Cost();
            C.ShowDialog();
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            Info I = new Info();
            I.ShowDialog();
        }

        private void buttonEmail_Click(object sender, EventArgs e)
        {
            Email E = new Email();
            E.ShowDialog();
        }
    }
}
