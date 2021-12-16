using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLTB
{
    public partial class Form1 : Form
    {
        public static SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-I6017GJ;Initial Catalog=TB;Integrated Security=True");
        string str, sql_view;
        public Form1()
        {
            InitializeComponent();
        }
        private void refresh()
        {
            txt_idTB.ResetText();
            txt_idNV.ResetText();
            txt_idPhong.ResetText();
            txt_idDenBu.ResetText();
            txt_TenTB.ResetText();
            txt_DonGia.ResetText();
            txt_SL.ResetText();
            cbb_TRANGTHAI.ResetText();
            dtp_NgayNhap.ResetText();
            dtp_NgayHong.ResetText();
            rtb_GhiChu.ResetText();
            btn_Sua.Enabled = false;
            btn_Xoa.Enabled = false;
            btn_Them.Enabled = true;
        }
        private void View_database(string sql)
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand(sql, con);
                SqlDataAdapter data_adapter = new SqlDataAdapter(command);
                DataTable data_table = new DataTable();
                data_adapter.Fill(data_table);
                con.Close();
                this.dgv_ThietBi.DataSource = data_table;
            }
            catch (Exception) { MessageBox.Show("Error !!!"); }
        }
        private bool Check_Key(string sql, string key)
        {
            bool ok = false;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand(sql, con);
                SqlDataReader data = command.ExecuteReader();
                if (data.Read() && (data.GetString(0).ToUpper() == key.ToUpper())) ok = true;
                con.Close();
            }
            catch (Exception) { MessageBox.Show("Error !!!"); }
            return ok;
        }
        //---------------------------------------------------------------

        private void Form1_Load(object sender, EventArgs e)
        {
            sql_view = "SELECT IDThietBi,IDNhanVien, IDPhong, IDDenBu, TenTB, DonGiaTB, SoLuong, " +
                "TrangThai,NgayNhapTB,NgayHongTB,GhiChu FROM ThietBi";
            View_database(sql_view);
            dgv_ThietBi_Click(this.dgv_ThietBi.CurrentRow.Index);
        }

        private void dgv_ThietBi_Click(int Index)
        {
            str = this.txt_idTB.Text = dgv_ThietBi.Rows[Index].Cells[0].Value.ToString();
            this.txt_idNV.Text = this.dgv_ThietBi.Rows[Index].Cells[1].Value.ToString();
            this.txt_idPhong.Text = this.dgv_ThietBi.Rows[Index].Cells[2].Value.ToString();
            this.txt_idDenBu.Text = this.dgv_ThietBi.Rows[Index].Cells[3].Value.ToString();
            this.txt_TenTB.Text = this.dgv_ThietBi.Rows[Index].Cells[4].Value.ToString();
            this.txt_DonGia.Text = this.dgv_ThietBi.Rows[Index].Cells[5].Value.ToString();
            this.txt_SL.Text = this.dgv_ThietBi.Rows[Index].Cells[6].Value.ToString();
            this.cbb_TRANGTHAI.Text = this.dgv_ThietBi.Rows[Index].Cells[7].Value.ToString();
            int dd = int.Parse(this.dgv_ThietBi.Rows[this.dgv_ThietBi.CurrentRow.Index].Cells[8].Value.ToString().Substring(0, 2));
            int mm = int.Parse(this.dgv_ThietBi.Rows[this.dgv_ThietBi.CurrentRow.Index].Cells[8].Value.ToString().Substring(3, 2));
            int yyyy = int.Parse(this.dgv_ThietBi.Rows[this.dgv_ThietBi.CurrentRow.Index].Cells[8].Value.ToString().Substring(6, 4));
            this.dtp_NgayNhap.Value = new DateTime(yyyy, mm, dd);
            int aa = int.Parse(this.dgv_ThietBi.Rows[this.dgv_ThietBi.CurrentRow.Index].Cells[9].Value.ToString().Substring(0, 2));
            int bb = int.Parse(this.dgv_ThietBi.Rows[this.dgv_ThietBi.CurrentRow.Index].Cells[9].Value.ToString().Substring(3, 2));
            int cccc = int.Parse(this.dgv_ThietBi.Rows[this.dgv_ThietBi.CurrentRow.Index].Cells[9].Value.ToString().Substring(6, 4));
            this.dtp_NgayHong.Value = new DateTime(cccc, bb, aa);
            this.rtb_GhiChu.Text = this.dgv_ThietBi.Rows[Index].Cells[10].Value.ToString();

        }

        private void dgv_ThietBi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv_ThietBi_Click(this.dgv_ThietBi.CurrentRow.Index);
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            string key = this.txt_idTB.Text;
            string sql = "SELECT IDThietBi FROM ThietBi WHERE IDThietBi = '" + key + "'";
            if (this.txt_idTB.Text != "" && Check_Key(sql, key))
            {
                string query = "UPDATE ThietBi SET IDThietBi=N'" + this.txt_idTB.Text
                + "',IDNhanVien=N'" + this.txt_idNV.Text
                + "',IDPhong=N'" + this.txt_idPhong.Text
                + "',IDDenBu ='" + this.txt_idDenBu.Text
                + "',TenTB ='" + this.txt_TenTB.Text
                + "',DonGiaTB ='" + this.txt_DonGia.Text
                + "',SoLuong ='" + this.txt_SL.Text
                + "',TrangThai ='" + this.cbb_TRANGTHAI.Text
                + "',NgayNhapTB ='" + this.dtp_NgayNhap.Text
                + "',NgayHongTB ='" + this.dtp_NgayHong.Text
                + "',GhiChu ='" + this.rtb_GhiChu.Text
                + "' WHERE (IDThietBi = '" + this.txt_idTB.Text + "')";
                Execute_Query(query);
                View_database(sql_view);
            }
            else MessageBox.Show("Khóa rỗng hoặc không hợp lệ", "Kiểm tra khóa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            string dd = this.dtp_NgayNhap.Value.Day.ToString();
            string MM = this.dtp_NgayNhap.Value.Month.ToString();
            string yyyy = this.dtp_NgayNhap.Value.Year.ToString();
            string aa = this.dtp_NgayHong.Value.Day.ToString();
            string BB = this.dtp_NgayHong.Value.Month.ToString();
            string cccc = this.dtp_NgayHong.Value.Year.ToString();
            string key = this.txt_idTB.Text;
            string sql = "SELECT IDThietBi FROM ThietBi WHERE IDThietBi = '" + key + "'";
            if (this.txt_idTB.Text != "" && !Check_Key(sql, key))
            {
                string query = "INSERT INTO ThietBi VALUES('"
                + this.txt_idTB.Text + "',N'"
                + this.txt_idNV.Text + "',N'"
                + this.txt_idPhong.Text + "',N'"
                + this.txt_idDenBu.Text + "',N'"
                + this.txt_TenTB.Text + "',N'"
                + this.txt_DonGia.Text + "',N'"
                + this.txt_SL.Text + "',N'"
                + this.cbb_TRANGTHAI.Text + "',N'"
                + yyyy + "/" + MM + "/" + dd + ",'"
                + cccc + "/" + BB + "/" + aa + ",'"
                + this.rtb_GhiChu.Text + "')'";
                Execute_Query(query);
                View_database(sql_view);
               
            }
            else MessageBox.Show("Khóa rỗng hoặc trùng", "Kiểm tra khóa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Ban chác chắn xóa bản ghi này ?", "C# Programming",
               MessageBoxButtons.OKCancel, MessageBoxIcon.Question).ToString() == "OK")
                {
                    Execute_Query(@"DELETE FROM ThietBi WHERE (IDThietBi='" + str + "')");
                    View_database(sql_view);
                }
            }
            catch (Exception) { MessageBox.Show("Error !!!"); }
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_NhapLai_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void Execute_Query(string query)
        {
            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception) { MessageBox.Show("Error !!!"); }
        }
    }
}
