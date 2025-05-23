
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Globalization;

namespace Pathology_Management_System11
{
    public partial class Bill : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-8TNPHQ7\SQLEXPRESS;Initial Catalog=pathology;Integrated Security=True;Encrypt=False");
        public Bill()
        {
            InitializeComponent();

        }
        private void label6_Click(object sender, EventArgs e)
        { }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || dateTimePicker1.Text == "" || comboBox3.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
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
                    // Proceed with database insertion
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into bill values('" + textBox1.Text + "','" + textBox2.Text + "' ,'" + comboBox3.Text + "','" + textBox3.Text + "','" + comboBox1.Text + "' , '" + comboBox2.Text + "','" + dateTimePicker1.Text + "','" + textBox4.Text + "' ,'" + textBox5.Text + "' , '" + textBox6.Text + "'  )", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data saved successfully");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox6.Clear();
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
            SqlCommand cmd = new SqlCommand("Select *from bill", con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only allow digit");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only allow letter");
            }

        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        { }

        private void panel1_Paint(object sender, PaintEventArgs e)
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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Enter the data to print", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dataGridView1.Rows.Count > 0)
            {
                // Print the content of panel1
                PrintDocument doc = new PrintDocument();              
                PrintDialog dlg = new PrintDialog();
                dlg.Document = doc;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    doc.Print();
                }
            }
        }             
    private void button3_Click_1(object sender, EventArgs e)
        {
            Report rrr = new Report();
            rrr.Show();
            this .Hide ();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void Btn_Calculate_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    // Parse input values
                    decimal sellingPrice = decimal.Parse(textBox4.Text);
                    decimal discount = decimal.Parse(textBox5.Text);

                    // Calculate total amount after discount
                    decimal discountedAmount = sellingPrice * (1 - discount / 100);

                    // Display the result
                    textBox6.Text = discountedAmount.ToString("F2"); // Format to two decimal places
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid input. Please enter valid numeric values.");
                }
            }
        }
    }
}
    



