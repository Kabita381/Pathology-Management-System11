
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pathology_Management_System11
{
    public partial class doctor : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-8TNPHQ7\SQLEXPRESS;Initial Catalog=pathology;Integrated Security=True;Encrypt=False");
        public doctor()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {           
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox3.Text == "" || dateTimePicker1.Text == "")
            {
                MessageBox.Show("All fields are mandatory");
            }
            else
            {
                DateTime selectedDate = dateTimePicker1.Value.Date;
                DateTime today = DateTime.Today;

                if (selectedDate < today)
                {
                    MessageBox.Show("Appointment date cannot be less than today's date.");
                }
                else
                {                 
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into doctor values('" + textBox1.Text + "','" + textBox2.Text + "' ,'" + textBox3.Text + "' ,'" + comboBox3.Text + "','" + dateTimePicker1.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data saved successfully");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();

                    con.Close();
                }
            }       
        }
        
        private void Btn_Login_Click(object sender, EventArgs e)
        {
            display();
        }
        public void display()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select *from doctor", con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda =new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from doctor where id='"+textBox1.Text+"'",con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record deleted successfully");
            con.Close();
            display();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Administrator adm = new Administrator();
            this.Close();
            adm.Show();
        }

        private void Btn_Login_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
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
                    return false; 
                }

                string localPart = parts[0];
                string domainPart = parts[1];

                try
                {                  
                    var hostEntry = Dns.GetHostEntry(domainPart);
                    return hostEntry.HostName.Length > 0;
                }
                catch
                {
                    return false; 
                }
            }
        }
    }
}
