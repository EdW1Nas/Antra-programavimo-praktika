using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Registration
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Server = DESKTOP-C3F8EEP\\SQLEXPRESS; Database=School;Integrated Security = SSPI");
       
       
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //login
        {
            string username, password;

            string name, pass;

            username = txtUsername.Text;
            password = txtPassword.Text;

            try
            {
                string studentQuerry = "SELECT * FROM Admin_Table WHERE studentName = '" + txtUsername.Text + "'AND studentLastName = '" + txtPassword.Text + "'";
                string profQuerry = "SELECT * FROM Admin_Table WHERE profName = '" + txtUsername.Text + "'AND profLastName = '" + txtPassword.Text + "'";
                SqlDataAdapter sdaStudent = new SqlDataAdapter(studentQuerry, con);
                SqlDataAdapter sdaProf = new SqlDataAdapter(profQuerry, con);

                DataTable dtableStudent = new DataTable();
                DataTable dtableProf = new DataTable();

                sdaStudent.Fill(dtableStudent);
                sdaProf.Fill(dtableProf);

                if (dtableStudent.Rows.Count > 0)
                {
                    username = txtUsername.Text;
                    password = txtPassword.Text;

                    name = username;
                    pass = password;

                    Form4 f4 = new Form4();
                    f4.transfer(name, pass);
                    Hide();
                    f4.ShowDialog(); //Show Student Info
                    Close();
                }
                else if (dtableProf.Rows.Count > 0)
                {
                    username = txtUsername.Text;
                    password = txtPassword.Text;

                    name = username;
                    pass = password;


                    Form3 f3 = new Form3();
                    f3.transfer(name,pass);
                    Hide();
                    f3.ShowDialog(); //Show Prof Info (Grading Table)
                    Close();
                }
                else if(username == "admin" && password == "admin")
                {

                    Form1 f1 = new Form1();
                    Hide();
                    f1.ShowDialog(); //Show Admin Info (Student Registration)
                    Close();
                }
                else
                {
                    MessageBox.Show("Incorrect Login");
                    txtUsername.Clear();
                    txtPassword.Clear();
                    txtUsername.Focus();
                }

            }
            catch
            {
                MessageBox.Show("Error");

            }
            finally
            {

                con.Close();
            }

        }
    }
}
