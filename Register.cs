using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Pathology_Management_System11
{
    public partial class Add_User : Form
    {
        public const string Pattern = @"^\d{10}$";
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-8TNPHQ7\SQLEXPRESS;Initial Catalog=pathology;Integrated Security=True;Encrypt=False");
        public Add_User()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.Text == "" || textBox2.Text == "" || dateTimePicker1.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("All fields are mandatory");
            }
            else if (!Regex.IsMatch(textBox5.Text, Pattern) || textBox5.Text.Length != 10)
            {
                MessageBox.Show("Number must contain 10 digit");
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
                    // Proceed with database insertion
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into Register values(@param1,@param2,@param3,@param4,@param5,@param6,@param7)", con);
                    cmd.Parameters.AddWithValue("@param1", Convert.ToInt32(textBox1.Text));
                    cmd.Parameters.AddWithValue("@param2", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@param3", textBox2.Text);
                    cmd.Parameters.AddWithValue("@param4", dateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("@param5", textBox3.Text);
                    cmd.Parameters.AddWithValue("@param6", textBox4.Text);
                    cmd.Parameters.AddWithValue("@param7", textBox5.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data saved successfully");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    con.Close();
                }
            }
        }
    private void button1_Click(object sender, EventArgs e)

        {
            try
            {
                con.Open();
                SqlCommand cmdUpdate = new SqlCommand("Update Register SET ID= @param1,[User Role]=@param2,Name=@param3,[Date of Birth]=@param4,Email=@param5,Password=@param6,[Mobile NO.]=@param7 where ID=@param1", con);
                cmdUpdate.Parameters.AddWithValue("@param1", Convert.ToInt32(textBox1.Text));
                cmdUpdate.Parameters.AddWithValue("@param2", comboBox1.Text);
                cmdUpdate.Parameters.AddWithValue("@param3", textBox2.Text);
                cmdUpdate.Parameters.AddWithValue("@param4", dateTimePicker1.Text);
                cmdUpdate.Parameters.AddWithValue("@param5", textBox3.Text);
                cmdUpdate.Parameters.AddWithValue("@param6", textBox4.Text);
                cmdUpdate.Parameters.AddWithValue("@param7", textBox5.Text);
                cmdUpdate.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmdUpdate);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Console.WriteLine("Rows in DataTable: " + dt.Rows.Count);
                if (dt.Rows.Count > 0)
                {
                    textBox1.Text = dt.Rows[0]["ID"].ToString();
                    comboBox1.Text = dt.Rows[0]["User Role"].ToString();
                    textBox2.Text = dt.Rows[0]["Name"].ToString();
                    textBox4.Text = dt.Rows[0]["Email"].ToString();
                    textBox5.Text = dt.Rows[0]["Password"].ToString();
                    textBox3.Text = dt.Rows[0]["Mobile NO."].ToString();

                }
                else
                {
                    MessageBox.Show("data updated successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();

        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            display();

            void display()
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Register ", con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView2.DataSource = dt;
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Patient patient = new Patient();
            patient.Show();
            this.Hide();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
       
    }
    

