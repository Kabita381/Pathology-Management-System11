using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pathology_Management_System11
{
    public partial class Category : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-8TNPHQ7\SQLEXPRESS;Initial Catalog=pathology;Integrated Security=True;Encrypt=False");
        public Category()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.Text == "" || comboBox3.Text == "" || textBox4.Text == "" )
            {
                MessageBox.Show("All fields are mandatory");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into [Test Category] values('" + textBox1.Text + "','" + comboBox1.Text + "' ,'" + comboBox3.Text + "' ,'" + textBox4.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data saved successfully");
                    textBox1.Clear();                   
                    textBox4.Clear();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message);
                }
                finally
                {
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
            SqlCommand cmd1 = new SqlCommand("Select * from [Test Category]", con);
            cmd1.ExecuteNonQuery();
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            // Select the data for the second DataGridView
            SqlCommand cmd2 = new SqlCommand("Select * from [Test Name]", con);
            cmd2.ExecuteNonQuery();
            SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            // Enable full row selection for dataGridView2
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = true;
            dataGridView2.DataSource = dt2;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from [Test Category] where TID ='" + textBox1.Text + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record deleted successfully");
            con.Close();
            display();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           Add_User add= new Add_User();    
            add.Show();
            this.Hide();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            { }
        }
    }
}
