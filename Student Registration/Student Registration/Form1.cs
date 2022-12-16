using Microsoft.VisualBasic.ApplicationServices;
using System.Data.SqlClient;
using System.Formats.Asn1;

namespace Student_Registration
{
    public partial class Form1 : Form
    {
        public Form1()
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

        public void load()
        {

            try
            {

                sql = "select * from Admin_Table";
                cmd = new SqlCommand(sql, con);
                con.Open();

                read = cmd.ExecuteReader();

                adminPanel.Rows.Clear();

                while(read.Read())
                {

                    adminPanel.Rows.Add(read[0], read[1], read[2], read[3], read[4], read[5], read[6]);


                }
                con.Close();

            }
            catch(Exception ex)
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
            
            while(read.Read())
            {

                txtStudentName.Text = read[1].ToString();
                txtStudentLastName.Text = read[2].ToString();
                txtClass.Text = read[3].ToString();
                txtProfName.Text = read[4].ToString();
                txtProfLastName.Text = read[5].ToString();
                txtCourse.Text= read[6].ToString();

                


            }
            con.Close();
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //Save
        {

            string studentName = txtStudentName.Text;
            string studentLastName = txtStudentLastName.Text;
            string studentClass = txtClass.Text;
            string profName = txtProfName.Text;
            string profLastName = txtProfLastName.Text;
            string course = txtCourse.Text;

            if (mode == true)
            {

                sql = "insert into Admin_Table(studentName,studentLastName, class, profName, profLastName, course) values(@studentName,@studentLastName, @class, @profName, @profLastName, @course)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@studentName", studentName);
                cmd.Parameters.AddWithValue("@studentLastName", studentLastName);
                cmd.Parameters.AddWithValue("@class", studentClass);
                cmd.Parameters.AddWithValue("@profName", profName);
                cmd.Parameters.AddWithValue("@profLastName", profLastName);
                cmd.Parameters.AddWithValue("@course", course);
                MessageBox.Show("Record Added");
                cmd.ExecuteNonQuery();

                txtStudentName.Clear();
                txtStudentLastName.Clear();
                txtClass.Clear();
                txtProfName.Clear();
                txtProfLastName.Clear();
                txtCourse.Clear();
                txtStudentName.Focus();
                

            }
            else
            {
                id = adminPanel.CurrentRow.Cells[0].Value.ToString();
                sql = "update Admin_Table set studentName = @studentName, studentLastName = @studentLastName, class = @class, profName = @profName, profLastName = @profLastName, course = @course where id = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@studentName", studentName);
                cmd.Parameters.AddWithValue("@studentLastName", studentLastName);
                cmd.Parameters.AddWithValue("@class", studentClass);
                cmd.Parameters.AddWithValue("@profName", profName);
                cmd.Parameters.AddWithValue("@profLastName", profLastName);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@id", id);
                MessageBox.Show("Record Updated");
                cmd.ExecuteNonQuery();

                txtStudentName.Clear();
                txtStudentLastName.Clear();
                txtClass.Clear();
                txtProfName.Clear();
                txtProfLastName.Clear();
                txtCourse.Clear();
                txtStudentName.Focus();
                button2.Text = "Save";
                mode = true;
                

            }

            con.Close();
            load();
        }

        private void button1_Click(object sender, EventArgs e) //Clear
        {
            txtStudentName.Clear();
            txtStudentLastName.Clear();
            txtClass.Clear();
            txtProfName.Clear();
            txtProfLastName.Clear();
            txtCourse.Clear();
            txtStudentName.Focus();
            button2.Text = "Save";
            mode = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) //Table
        {
            if(e.ColumnIndex == adminPanel.Columns["edit"].Index && e.RowIndex >= 0) //edit
            {

                mode = false;
                id = adminPanel.CurrentRow.Cells[0].Value.ToString();
                getID(id);
                button2.Text = "Edit";

            }
            if (e.ColumnIndex == adminPanel.Columns["delete"].Index && e.RowIndex >= 0) //delete
            {
              
                id = adminPanel.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from Admin_Table where id = @id ";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted");
                con.Close();
                button2.Text = "Save";
                mode = true;
                load();


            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            Hide();
            f2.ShowDialog(); //Show Login
            Close();
        }
    }
}