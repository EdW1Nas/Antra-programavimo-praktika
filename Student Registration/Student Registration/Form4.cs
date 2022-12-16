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

namespace Student_Registration
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            load();
        }

        SqlConnection con = new SqlConnection("Server = DESKTOP-C3F8EEP\\SQLEXPRESS; Database=School;Integrated Security = SSPI");
        SqlCommand cmd;
        SqlDataReader read;
        string sql;

        string transferedName, transferedPass;

        public void load()
        {

            try
            {



                sql = "select * from Admin_Table";
                cmd = new SqlCommand(sql, con);
                con.Open();

                read = cmd.ExecuteReader();

                studentPanel.Rows.Clear();


                while (read.Read())
                {

                   
                    studentPanel.Rows.Add(read[0], read[1], read[2], read[4], read[5], read[6], read[7], read[3]);
                    transfer(transferedName, transferedPass);


                }



                con.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);


            }


        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            Hide();
            f2.ShowDialog(); //Show Login
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            load();
        }

        public void transfer(string name, string pass)
        {

            transferedName = name;
            transferedPass = pass;

            txtStudentName.Text = transferedName;
            txtStudentLastName.Text = transferedPass;


            for (int i = 0; i < studentPanel.Rows.Count; i++)
            {
                
                if (studentPanel.Rows[i].Cells[1].Value.ToString() == transferedName && studentPanel.Rows[i].Cells[2].Value.ToString() == transferedPass)
                {
                    studentPanel.Rows[i].Visible = true;
                }
                else studentPanel.Rows[i].Visible = false;
            }
        }
    }
}
