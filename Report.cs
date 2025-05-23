using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pathology_Management_System11
{
    public partial class Report : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-8TNPHQ7\SQLEXPRESS;Initial Catalog=pathology;Integrated Security=True;Encrypt=False");
        public Report()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
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
                        SqlCommand cmd = new SqlCommand("Insert into Report values(@param1,@param2,@param3,@param4,@param5,@param6,@param7)", con);
                        {
                            cmd.Parameters.AddWithValue("@param1", textBox1.Text);
                            cmd.Parameters.AddWithValue("@param2", textBox2.Text);
                            cmd.Parameters.AddWithValue("@param3", textBox3.Text);
                            cmd.Parameters.AddWithValue("@param4", comboBox3.Text);
                            cmd.Parameters.AddWithValue("@param5", dateTimePicker1.Value);
                            cmd.Parameters.AddWithValue("@param6", comboBox1.Text);
                            cmd.Parameters.AddWithValue("@param7", comboBox2.Text);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Data saved successfully");
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        con.Close();
                    }
                }
            }
        }


        private void Btn_Login_Click(object sender, EventArgs e)
        {
            display();

            void display()
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Report ", con);
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
            SqlCommand cmd = new SqlCommand("delete from Report where ID='" + textBox1.Text + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record deleted successfully");
            con.Close();
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
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
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
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
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

        private void button3_Click(object sender, EventArgs e)
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

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT PID, Name,[Date of Registration], [Refer By] FROM Patient WHERE PID LIKE '%" + textBox1.Text + "%'", con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // Assuming the columns are named "Name" and "Address"
                        textBox1.Text = dt.Rows[0]["PID"].ToString();
                        textBox2.Text = dt.Rows[0]["Name"].ToString();
                        dateTimePicker1.Text = dt.Rows[0]["Date of Registration"].ToString();
                        comboBox2.Text = dt.Rows[0]["Refer By"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No matching records found.");
                    }

                    dataGridView1.DataSource = dt;
                    dataGridView1.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}