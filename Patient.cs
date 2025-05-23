using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Pathology_Management_System11
{

    public partial class Patient : Form
    {
        public const string Pattern = @"^\d{10}$";

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-8TNPHQ7\SQLEXPRESS;Initial Catalog=pathology;Integrated Security=True;Encrypt=False");

        public Patient()
        {
            InitializeComponent();
        }
        private void Txt_Username_TextChanged(object sender, EventArgs e)
        {
        }
        private void Btn_Login_Click(object sender, EventArgs e)
        {
            display();

           void display()
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from patient ", con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Patient where PID='" + textBox1.Text + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record deleted successfully");
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            doctor doc = new doctor();
            this.Hide();
            doc.Show();

            
        }

        private void Btn_Login_Click_1(object sender, EventArgs e)
        {
            display();
        }
        public void display()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select *from Patient", con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Txt_Username_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)       
        {
            if (textBox1.Text == "" || Txt_Username.Text == "" || textBox2.Text == "" || dateTimePicker1.Text == "" || comboBox3.Text == "" || comboBox2.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("All fields are mandatory");
            }
            else if (!Regex.IsMatch(textBox4.Text, Pattern) || textBox4.Text.Length != 10)
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
                    SqlCommand cmd = new SqlCommand("Insert into Patient values('" + textBox1.Text + "','" + Txt_Username.Text + "','" + textBox2.Text + "' ,'" + dateTimePicker1.Text + "' ,'" + comboBox3.Text + "' ,'" + comboBox2.Text + "', '" + textBox4.Text + "' )", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data saved successfully");
                    textBox1.Clear();
                    textBox2.Clear();                   
                    textBox4.Clear();                   
                    con.Close();
                }
            }
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
    
