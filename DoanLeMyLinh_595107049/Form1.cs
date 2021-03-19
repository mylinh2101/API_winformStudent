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

namespace DoanLeMyLinh_595107049
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-MC9SGV88;Initial Catalog=DemoCRUD;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }
        private void GetStudentsRecord()
        {
            
            SqlCommand cmd = new SqlCommand("SELECT * FROM StudentsTb", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            StudentRecordData.DataSource = dt;
        }

        private bool IsValidData()
        {
            if (txtHname.Text == string.Empty || txtTname.Text == string.Empty
                || txtAddress.Text == string.Empty || string.IsNullOrEmpty(txtPhonenumber.Text)
                || string.IsNullOrEmpty(txtRollnumber.Text)
                )
            {
                MessageBox.Show("Có chỗ chưa nhập dữ liệu!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                
                SqlCommand cmd = new SqlCommand("insert into StudentsTb values (@Name, @Fathername, @Rollnumber, @Address, @Mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtHname.Text);
                cmd.Parameters.AddWithValue("@Fathername", txtTname.Text);
                cmd.Parameters.AddWithValue("@Rollnumber", txtRollnumber.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtPhonenumber.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            GetStudentsRecord();
        }
        public int StudentID;
        private void StudentRecordData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            StudentID = Convert.ToInt32(StudentRecordData.Rows[index].Cells[0].Value);
            txtHname.Text = StudentRecordData.Rows[index].Cells[1].Value.ToString();
            txtTname.Text = StudentRecordData.Rows[index].Cells[2].Value.ToString();
            txtRollnumber.Text = StudentRecordData.Rows[index].Cells[3].Value.ToString();
            txtPhonenumber.Text = StudentRecordData.Rows[index].Cells[5].Value.ToString();
            txtAddress.Text = StudentRecordData.Rows[index].Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {

                SqlCommand cmd = new SqlCommand("update StudentsTb set Name = @Name, FatherName = @Fathername, RollNumber = @Rollnumber, Address = @Address, Mobile = @Mobile" +
                    " where StudentID = @ID ", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtHname.Text);
                cmd.Parameters.AddWithValue("@Fathername", txtTname.Text);
                cmd.Parameters.AddWithValue("@Rollnumber", txtRollnumber.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtPhonenumber.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();
            }
            else
            {
                MessageBox.Show("Lỗi cập nhật!!!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {

                SqlCommand cmd = new SqlCommand("Delete from StudentsTb where StudentID = @ID ", con);
                cmd.CommandType = CommandType.Text;
               
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();
            }
            else
            {
                MessageBox.Show("Lỗi cập nhật!!!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc muốn thoát!","Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
