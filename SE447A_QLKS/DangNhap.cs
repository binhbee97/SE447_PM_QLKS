using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE447A_QLKS
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// method to login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            // init object and parameter
            string[] thamso = new string[] { "@Username", "@Pass" };
            object[] giatri = new object[] { txtTenDangNhap.Text, txtMatKhau.Text };
            DataTable dt = XuLiDuLieu.docDuLieuStored("DangNhap", giatri, thamso);

            if (dt.Rows.Count == 1)
            {
                if (dt.Rows[0]["TrangThai"] + "" == "False")
                    lblThongBao.Text = "Tài khoản " + dt.Rows[0]["Username"].ToString() + " Đã bị khóa";
                else
                {
                    //sent data to main
                    ThongTinDangNhap.IDTaiKhoan = dt.Rows[0]["IDNhanVien"].ToString();
                    ThongTinDangNhap.HoTenNV = dt.Rows[0]["HoTenNV"].ToString();
                    ThongTinDangNhap.Username = dt.Rows[0]["Username"].ToString();
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                lblThongBao.Text = "Tài khoản không tồn tại";
            }
        }
    }
}
