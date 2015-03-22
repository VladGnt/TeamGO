using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FamilyFinances
{
    public partial class Email : Form
    {
        public Email()
        {
            InitializeComponent();
        }

        private void buttonEmailSend_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Please enter the message");
            }
            else
            {
                try
                {

                    SmtpClient client = new SmtpClient("smtp.mail.ru", 587);
                    client.Credentials = new NetworkCredential("vlad-gnatovskiy@mail.ru", "Vlad-gnatovskiy2");
                    string from = "vlad-gnatovskiy@mail.ru";
                    string to = "sanya-osaulenko@mail.ru";
                    string subject = "От: " + Convert.ToString(textBox1.Text) + "  |  " + "ask: " + Convert.ToString(textBox2.Text);
                    string text = Convert.ToString(textBox3.Text);
                    client.EnableSsl = true;

                    client.Send(from, to, subject, text);
                    this.Close();
                    MessageBox.Show("Ваше сообщение отправлено, спасибо за ваше обращение");

                }
                catch { MessageBox.Show("Что-то пошло не так:( Проверте коректность введеных вами данных, если при повторном вводе проблема не решена - обратитесь к системному администратору "); }
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
