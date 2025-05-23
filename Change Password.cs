
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
using System.Configuration;


namespace Pathology_Management_System11
{
    public partial class Change_Password : Form
    {

        public Change_Password()

        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Txt_Current.Text == "" || Txt_New.Text == "" || Txt_Confirm.Text == "")
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
                        SqlCommand checkPasswordCmd = new SqlCommand("SELECT  COUNT(*) FROM [Account Login] WHERE password = @currentPassword", con);
                        checkPasswordCmd.Parameters.AddWithValue("@currentPassword", Txt_Current.Text);
                        int rowCount = (int)checkPasswordCmd.ExecuteScalar();

                        if (rowCount == 1)
                        {
                            SqlCommand updatePasswordCmd = new SqlCommand("UPDATE [Account Login] SET password = @newPassword WHERE password = @currentPassword", con);
                            updatePasswordCmd.Parameters.AddWithValue("@newPassword", Txt_Confirm.Text);
                            updatePasswordCmd.Parameters.AddWithValue("@currentPassword", Txt_Current.Text);
                            updatePasswordCmd.ExecuteNonQuery();

                            label1.Text = "Successfully Updated";
                        }
                        else
                        {
                            label1.Text = "Updation Failed. Check your login information.";
                        }
                    }
                }
                else
                {
                    label1.Text = "New password and confirm password should be the same!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
            private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Txt_Current.UseSystemPasswordChar = true;
            }
            else
            {
                Txt_Current.UseSystemPasswordChar = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                Txt_New.UseSystemPasswordChar = true;
            }
            else
            {
                Txt_New.UseSystemPasswordChar = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                Txt_Confirm.UseSystemPasswordChar = true;
            }
            else
            {
                Txt_Confirm.UseSystemPasswordChar = false;
            }

        }

        private void Txt_Current_KeyPress(object sender, KeyPressEventArgs e)
        {
            string[] passWords = { "aX2#", "sed2T", "*v3X", "Ae234&B", "fg234", "g1HL", "#1$23", "5a7%" };
            foreach (string passWord in passWords)
            {
                bool b = ValidatePassword(passWord);
                Console.WriteLine("'{0}' is{1} a valid password", passWord, b ? "" : "n't");
            }
            Console.Read();
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
    }
}  

