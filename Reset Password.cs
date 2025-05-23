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
    public partial class Reset_Password : Form
    {
        public Reset_Password()
        {
            InitializeComponent();
        }

        private void Reset_Password_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (Txt_Confirm.Text == ""  || Txt_New.Text == "")
            {
                MessageBox.Show("Please check your credentials");
                return;
            }

            try
            {
                if (Txt_New.Text == Txt_Confirm.Text)
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-8TNPHQ7\SQLEXPRESS;Initial Catalog=pathology;Integrated Security=True;Encrypt=False"))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Update [Register] set password ='" + Txt_Confirm.Text + "' where password ='" + Txt_New.Text + "'", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Password Changed");
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("New Password and Confirm Password do not match");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Forgot_Password forgot = new Forgot_Password();
            forgot.Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Txt_New.UseSystemPasswordChar = true;
            }
            else
            {
                Txt_New.UseSystemPasswordChar = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                Txt_Confirm.UseSystemPasswordChar = true;
            }
            else
            {
                Txt_Confirm.UseSystemPasswordChar = false;
            }
        }
    }
    }

