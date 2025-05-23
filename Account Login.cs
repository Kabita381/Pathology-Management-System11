using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pathology_Management_System11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Btn_Login_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-8TNPHQ7\SQLEXPRESS;Initial Catalog=pathology;Integrated Security=True;Encrypt=False");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from [Account Login] where Username = @Username AND Password =@Password", con);
            cmd.Parameters.AddWithValue("@Username", Txt_Username.Text);
            cmd.Parameters.AddWithValue("@Password", Txt_Password.Text);
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Login Successfully");
                Administrator ad = new Administrator();
                ad.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please check your credentials");
            }
            con.Close();
        }

        private void Txt_Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Txt_Password_TextChanged(object sender, EventArgs e)
        {
            string[] passWords = { "aX2#", "sed2T", "*v3X", "Ae234&B", "fg234", "g1HL", "#1$23", "5a7%" };
            foreach (string passWord in passWords)
            {
                bool b = ValidatePassword(passWord);
                Console.WriteLine("'{0}' is{1} a valid password", passWord, b ? "" : "n't");
            }
            Console.ReadKey();
        }
        static bool ValidatePassword(string passWord)
        {
            int validConditions = 0;
            foreach (char c in passWord)
            {
                if (c >= 'a' && c <= 'z')
                {
                    validConditions++;
                    break;
                }
            }
            foreach (char c in passWord)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 0) return false;
            foreach (char c in passWord)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 1) return false;
            if (validConditions == 2)
            {
                char[] special = { '@', '#', '$', '%', '^', '&', '+', '=' }; // or whatever    
                if (passWord.IndexOfAny(special) == -1) return false;
            }
            return true;
        }

        private void Txt_Password_KeyPress_1(object sender, KeyPressEventArgs e)
        {

        }
    }
      }
    


