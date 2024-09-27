using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_Lý_Sinh_Viên.Class
{
    public class DiemMonHoc : SinhVien
    {
        string mamonhoc, tenmonhoc;
        int tinchi;
        double diemthi1, diemthi2;

        public string Mamonhoc { get => mamonhoc; set => mamonhoc = value; }
        public string Tenmonhoc { get => tenmonhoc; set => tenmonhoc = value; }
        public int Tinchi { get => tinchi; set => tinchi = value; }
        public double Diemthi1 { get => diemthi1; set => diemthi1 = value; }
        public double Diemthi2 { get => diemthi2; set => diemthi2 = value; }
        public DiemMonHoc(string mamonhoc, string tenmonhoc, int tinchi, double diemthi1, double diemthi2)
        {
            this.mamonhoc = mamonhoc;
            this.tenmonhoc = tenmonhoc;
            this.tinchi = tinchi;
            this.diemthi1 = diemthi1;
            this.diemthi2 = diemthi2;
        }
        public DiemMonHoc()
        {
            mamonhoc = "";
            tenmonhoc = "";
            tinchi = 0;
            diemthi1 = 0.0;
            diemthi2 = 0.0;
        }
        public DiemMonHoc ThemDiemThi(DiemMonHoc diem)
        {
            Console.Write("\t\t\tNhập mã sinh viên:");
            diem.Masinhvien = Console.ReadLine();
            Console.Write("\t\t\tNhập mã môn học:");
            diem.mamonhoc = Console.ReadLine();
            Console.Write("\t\t\tNhập tên môn học:");
            diem.tenmonhoc = Console.ReadLine();
            diem.Tinchi = NhapSoNguyen("\t\t\tNhập số tín chỉ: ");
            diem.Diemthi1 = NhapSoThuc("\t\t\tNhập điểm thi lần 1: ");
            diem.Diemthi2 = NhapSoThuc("\t\t\tNhập điểm thi lần 2: ");
            diem.Diemthi2 = diemthi2;
            return diem;
        }
        public DiemMonHoc ThemDiemThi(DiemMonHoc diem, string MaMonHoc,string masinhvien)
        {
            diem.Masinhvien = masinhvien;
            diem.mamonhoc = MaMonHoc;
            Console.Write("\t\t\tNhập tên môn học:");
            diem.tenmonhoc = Console.ReadLine();
            diem.Tinchi = NhapSoNguyen("\t\t\tNhập số tín chỉ: ");
            diem.Diemthi1 = NhapSoThuc("\t\t\tNhập điểm thi lần 1: ");
            diem.Diemthi2 = NhapSoThuc("\t\t\tNhập điểm thi lần 2: ");
            diem.Diemthi2 = diemthi2;
            return diem;
        }
        public DiemMonHoc ThemDiemThiDaCoMa(DiemMonHoc diem, string masinhvien)
        {
            diem.Masinhvien = masinhvien;
            Console.Write("\t\t\tNhập mã môn học: ");
            diem.Mamonhoc = Console.ReadLine();
            Console.Write("\t\t\tNhập tên môn học: ");
            diem.Tenmonhoc = Console.ReadLine();
            diem.Tinchi = NhapSoNguyen("\t\t\tNhập số tín chỉ: ");
            diem.Diemthi1 = NhapSoThuc("\t\t\tNhập điểm thi lần 1: ");
            diem.Diemthi2 = NhapSoThuc("\t\t\tNhập điểm thi lần 2: ");
            return diem;
        }
        public int NhapSoNguyen(string gan)
        {
            int tmp;
            Console.Write(gan);
            while (!int.TryParse(Console.ReadLine(), out tmp))
            {
                Console.Write("\t\t\t  Nhập số nguyên: ");
            }
            return tmp;
        }

        public double NhapSoThuc(string gan)
        {
            double tmp;
            Console.Write(gan);
            while (!double.TryParse(Console.ReadLine(), out tmp) || tmp < 0 || tmp > 10)
            {
                Console.Write("\t\t\t  Nhập số thực từ 0 đến 10: ");
            }
            return tmp;
        }

        public void TieuDeXuatDiem()
        {
            Console.WriteLine("\n  Mã môn học  Tên môn học                   Tín chỉ   Điểm thi 1  Điểm thi 2  Điểm Tích Lũy  ");
        }
        public void TieuDeXuatDiemVSMaSinhVien()
        {
            Console.WriteLine("\n  Mã sinh viên   Mã môn học  Tên môn học                   Tín chỉ   Điểm thi 1  Điểm thi 2  Điểm Tích Lũy  ");
        }
        public void XuatDiem()
        {
            Console.Write("  ");
            Console.Write($"{mamonhoc.PadRight(12)}");
            Console.Write($"{tenmonhoc.PadRight(33)}");
            Console.Write($"{tinchi.ToString().PadRight(10)}");
            Console.Write($"{Diemthi1.ToString().PadRight(13)}");
            Console.Write($"{Diemthi2.ToString().PadRight(13)}");
            Console.Write($"{DiemTichLuy().ToString().PadRight(7)}\n");
        }
        public void XuatDiemVSMaSinhVien()
        {
            Console.Write($"   {Masinhvien.PadRight(12)}");
            XuatDiem();
        }
        public double DiemTichLuy()
        {
            if (diemthi1 < Diemthi2)
                return diemthi2;
            return diemthi1;
        }
    }
}
