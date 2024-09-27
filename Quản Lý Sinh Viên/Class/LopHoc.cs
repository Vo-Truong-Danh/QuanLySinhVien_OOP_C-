using Quản_Lý_Sinh_Viên.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Quản_Lý_Sinh_Viên
{
    public class LopHoc : SinhVien
    {
        string tenLop;

        public string TenLop { get => tenLop; set => tenLop = value; }
        public LopHoc()
        {

            this.Masinhvien = " ";
            this.Hoten = "";
            this.Dantoc = "";
            this.Thanhpho = "";
            this.Sodienthoai = 0.0;
            this.Malop = "";
            this.Ngaysinh = DateTime.Now;
            this.Gioitinh = "";
            this.Nienkhoa = "";
            this.tenLop = "";

        }
        public void Nhap_TT_SV_Lop()
        {

            Console.Write("\n\t\t\t   Tên lớp: ");
            tenLop = Console.ReadLine();


            base.KhaiBaoThongTinSinhVien();
        }

        public void TieuDeXuatDanhSachLopHoc()
        {
            Console.WriteLine("\n  Tên lớp                     MÃ SV          Họ Tên                  Dân tộc  Thành Phố        Số điện thoại  Mã lớp     Ngày sinh   Giới tính   ");
        }
        public void Xuat_TT_SV_Lop()
        {

            Console.Write($"\n{tenLop.PadRight(27)}");
            base.HienThiThongTinSinhVien();
        }
        public bool LopHocDaTonTai(XmlDocument doc, string malop)
        {
            // Tìm kiếm node lớp học
            XmlNode lopHocNode = doc.SelectSingleNode($"//TENLOP[@LOP='{malop}']");

            // Kiểm tra xem node lớp học có tồn tại hay không
            return lopHocNode != null;
        }
    }
}
