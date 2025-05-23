using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pathology_Management_System11
{
    public partial class Administrator : Form
    {     
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-8TNPHQ7\SQLEXPRESS;Initial Catalog=pathology;Integrated Security=True;Encrypt=False");
        public Administrator()
        {
            InitializeComponent();
            label5.Text = "0";
            label9.Text = "0";
            label7.Text = "0";
            label13.Text = "0";
            label10.Text = "0";
            GetCounts();
        }
        private void GetCounts()
        {
            try
            {
                string query = "SELECT COUNT(TID) FROM [Test Category]";
                string query1 = "SELECT COUNT(ID) FROM Doctor";
                string query2 = "SELECT COUNT(ID) FROM Report";
                string query5 = "SELECT COUNT(PID) FROM Patient";
                string query6 = "SELECT COUNT(ID) FROM Bill";
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-8TNPHQ7\\SQLEXPRESS;Initial Catalog=pathology;Integrated Security=True;Encrypt=False"))
                {
                    con.Open();

                    using (SqlCommand pst = new SqlCommand(query, con))
                    using (SqlCommand pst1 = new SqlCommand(query1, con))
                    using (SqlCommand pst2 = new SqlCommand(query2, con))
                    using (SqlCommand pst5 = new SqlCommand(query5, con))
                    using (SqlCommand pst6 = new SqlCommand(query6, con))
                    {
                        using (SqlDataReader rs = pst.ExecuteReader())
                        {
                            if (rs.Read())
                            {
                                label5.Text = rs.GetInt32(0).ToString();
                            }
                        }

                        using (SqlDataReader rs1 = pst1.ExecuteReader())
                        {
                            if (rs1.Read())
                            {
                                label7.Text = rs1.GetInt32(0).ToString();
                            }
                        }

                        using (SqlDataReader rs2 = pst2.ExecuteReader())
                        {
                            if (rs2.Read())
                            {
                                label10.Text = rs2.GetInt32(0).ToString();
                            }
                        }

                        using (SqlDataReader rs5 = pst5.ExecuteReader())
                        {
                            if (rs5.Read())
                            {
                                label13.Text = rs5.GetInt32(0).ToString();
                            }
                        }
                        using (SqlDataReader rs6 = pst6.ExecuteReader())
                        {
                            if (rs6.Read())
                            {
                                label9.Text = rs6.GetInt32(0).ToString();
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());  // Print the exception details for debugging
            }
        }
            private void button4_Click(object sender, EventArgs e)
        {
            Patient pm = new Patient();
            pm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            doctor dm = new doctor();
            dm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Report RR = new Report();
            RR.Show();
            this.Hide();
        }



        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.Show();
            this.Hide();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Bill bill = new Bill();
            bill.Show();
            this.Hide();
        }

        private class Register
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Add_User add = new Add_User();
            add.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Change_Password chh = new Change_Password();
            chh.Show();
            this.Hide();
        }
    }
}
  

