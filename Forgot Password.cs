using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;


namespace Pathology_Management_System11
{

    public partial class Forgot_Password : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-8TNPHQ7\SQLEXPRESS;Initial Catalog=pathology;Integrated Security=True;Encrypt=False");
        string randomcode;
        public static string to;
        public Forgot_Password()
        {
            InitializeComponent();
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Txt_Email.Text == "")
            {
                MessageBox.Show("Email is needed. Please enter a valid email.");
                return;
            }
            else
            {
                string from, pass, messagebody;
                Random random = new Random();
                randomcode = (random.Next(999999)).ToString();
                MailMessage message = new MailMessage();
                to=(Txt_Email.Text).ToString();
                from = "Kavitadha6@gmail.com";
                pass = "hjog xdlt wmot ywyu";
                messagebody = $"Your Reset Code is{randomcode}";
                message.To.Add(to);
                message.From=new MailAddress(from);
                message.Body=messagebody;
                message.Subject = "Password Reset Code";
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(from,pass);
                try
                {
                    smtp.Send(message);
                     MessageBox.Show("Code successfully send");

                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
            }
    }

        private void button3_Click(object sender, EventArgs e)
        {
            if (randomcode == (textBox1.Text).ToString())
            {
                to=Txt_Email.Text;
                Reset_Password rp = new Reset_Password();
                this.Hide();
                rp.Show();
            }
            else
            {
                MessageBox.Show("Wrong Code");
            }
        }

        private void Txt_Email_KeyPress(object sender, KeyPressEventArgs e)
        {
            string email = "myemail@email.com";
            bool isValid = isValidEmailDomain(email);
            Console.WriteLine($"Is {email} a valid email? {isValid}");
            bool isValidEmailDomain(string Txt_Email)
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return false;
                }

                string[] parts = email.Split('@');
                if (parts.Length != 2)
                {
                    return false; // email must have exactly one @ symbol
                }

                string localPart = parts[0];
                string domainPart = parts[1];

                try
                {
                    // check if domain name has a valid MX record
                    var hostEntry = Dns.GetHostEntry(domainPart);
                    return hostEntry.HostName.Length > 0;
                }
                catch
                {
                    return false; // domain name is invalid or does not have a valid MX record
                }
            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
    }



