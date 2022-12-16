using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Registration
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            
            InitializeComponent();
            load();
            
            


        }
       

        SqlConnection con = new SqlConnection("Server = DESKTOP-C3F8EEP\\SQLEXPRESS; Database=School;Integrated Security = SSPI");
        SqlCommand cmd;
        SqlDataReader read;
        string id;
        bool mode = true;
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

                gradingPanel.Rows.Clear();

                
                while (read.Read())
                {

                    
                    gradingPanel.Rows.Add(read[0], read[1], read[2], read[3], read[6], read[7], read[4], read[5]);
                    transfer(transferedName, transferedPass);

                }

                con.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);


            }

        }

        public void getID(string id)
        {

            sql = "select * from Admin_Table where id = '" + id + "' ";

            cmd = new SqlCommand(sql, con);
            con.Open();
            read = cmd.ExecuteReader();

            while (read.Read())
            {

                txtGrade.Text = read[7].ToString();
               

            }
            con.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string grade = txtGrade.Text;

            if (mode == true)
            {

                MessageBox.Show("First select student");

            }
            else
            {
                id = gradingPanel.CurrentRow.Cells[0].Value.ToString();
                sql = "update Admin_Table set grade = @grade where id = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                
                cmd.Parameters.AddWithValue("@grade", grade);
                cmd.Parameters.AddWithValue("@id", id);
                MessageBox.Show("Record Updated");
                cmd.ExecuteNonQuery();

                
                txtGrade.Clear();
                txtGrade.Focus();
                mode = true;


            }

            con.Close();
            load();
        }

        private void gradingPanel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gradingPanel.Columns["edit"].Index && e.RowIndex >= 0) //edit
            {

                mode = false;
                id = gradingPanel.CurrentRow.Cells[0].Value.ToString();
                getID(id);
                

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            Hide();
            f2.ShowDialog(); //Show Login
            Close();
        }

        public void transfer(string name, string pass)
        {


            transferedName = name;
            transferedPass = pass;

            txtProfName.Text = transferedName;
            txtProfLastName.Text = transferedPass;

            for (int i = 0; i < gradingPanel.Rows.Count; i++)
            {

                if (gradingPanel.Rows[i].Cells[6].Value.ToString() == transferedName && gradingPanel.Rows[i].Cells[7].Value.ToString() == transferedPass)
                {
                    gradingPanel.Rows[i].Visible = true;
                }
                else gradingPanel.Rows[i].Visible = false;
            }
        }
    }
}

