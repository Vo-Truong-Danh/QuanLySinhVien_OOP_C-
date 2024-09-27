using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Quản_Lý_Sinh_Viên.Class
{
    public class SinhVien
    {
        string masinhvien, hoten, dantoc, thanhpho, malop, gioitinh, nienkhoa;
        double sodienthoai;
        DateTime ngaysinh;

        public string Masinhvien { get => masinhvien; set => masinhvien = value; }
        public string Hoten { get => hoten; set => hoten = value; }
        public string Dantoc { get => dantoc; set => dantoc = value; }
        public string Thanhpho { get => thanhpho; set => thanhpho = value; }
        public string Malop { get => malop; set => malop = value; }
        public string Gioitinh { get => gioitinh; set => gioitinh = value; }
        public double Sodienthoai { get => sodienthoai; set => sodienthoai = value; }
        public DateTime Ngaysinh { get => ngaysinh; set => ngaysinh = value; }
        public string Nienkhoa { get => nienkhoa; set => nienkhoa = value; }

        public SinhVien()
        {
            Masinhvien = "";
            Hoten = "";
            Dantoc = "";
            Thanhpho = "";
            Sodienthoai = 0;
            Malop = "";
            Ngaysinh = DateTime.Now;
            Gioitinh = "";
            nienkhoa = "";
        }
        public SinhVien(string masinhvien, string hoten, string dantoc, string thanhpho, double sodienthoai, string malop, DateTime ngaysinh, string gioitinh, string nienkhoa)
        {
            Masinhvien = masinhvien;
            Hoten = hoten;
            Dantoc = dantoc;
            Thanhpho = thanhpho;
            Sodienthoai = sodienthoai;
            Malop = malop;
            Ngaysinh = ngaysinh;
            Gioitinh = gioitinh;
            Nienkhoa = nienkhoa;
        }

        public SinhVien KhaiBaoThongTinSinhVien(SinhVien sv)
        {
            Console.WriteLine("\t\t\tNhập thông tin sinh viên:");
            //ràng buộc mssv
            do
            {
                Console.Write("\n\t\t\tMã sinh viên: ");
                sv.Masinhvien = Console.ReadLine();

                if (sv.Masinhvien.Length >= 10 && sv.Masinhvien.All(char.IsDigit))
                {
                    break;
                }
                else
                {
                    Console.Write("\n\t\t\tMã sinh viên phải có ít nhất 10 ký tự và chỉ chứa số. Vui lòng nhập lại.");
                }
            } while (true);
            //Ràng buộc tên
            do
            {
                Console.Write("\n\t\t\tHọ tên: ");
                sv.Hoten = Console.ReadLine();

                if (sv.Hoten.Length > 0 && KiemTraChu(sv.Hoten))
                {
                    break;
                }
                else
                {
                    if (sv.Hoten.Length == 5)
                    {
                        Console.Write("\n\t\t\tHọ tên quá ngắn. Vui lòng nhập lại:");
                    }
                    else
                    {
                        Console.Write("\n\t\t\tHọ tên chỉ được chứa chữ cái. Vui lòng nhập lại:");
                    }
                }
            } while (true);

            do
            {
                Console.Write("\n\t\t\tDân tộc: ");
                sv.Dantoc = Console.ReadLine();

                if (sv.Dantoc.Length > 0 && KiemTraChu(sv.Dantoc))
                {
                    break;
                }
                else
                {
                    if (sv.Dantoc.Length == 2)
                    {
                        Console.Write("\n\t\t\tDân tộc sai. Vui lòng nhập lại:");
                    }
                    else
                    {
                        Console.Write("\n\t\t\tDân tộc chỉ được chứa chữ cái. Vui lòng nhập lại.:");
                    }
                }
            } while (true);

            do
            {
                Console.Write("\n\t\t\tTỉnh / TP: ");
                sv.Thanhpho = Console.ReadLine();

                if (sv.Thanhpho.Length > 0 && KiemTraChu(sv.Thanhpho))
                {
                    break;
                }
                else
                {
                    if (sv.Thanhpho.Length == 2)
                    {
                        Console.WriteLine("\n\t\t\tTỉnh / TP sai. Vui lòng nhập lại.");
                    }
                    else
                    {
                        Console.WriteLine("\n\t\t\tTỉnh / TP chỉ được chứa chữ cái. Vui lòng nhập lại.");
                    }
                }
            } while (true);


            do
            {
                Console.Write("\n\t\t\tSố điện thoại: ");
                string input = Console.ReadLine();

                if (double.TryParse(input, out sv.sodienthoai) && input.Length >= 10 && input.Length <= 13)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\n\t\t\tSố điện thoại không hợp lệ. Vui lòng nhập lại.");
                }
            } while (true);

            Console.Write("\t\t\tMã lớp: ");
            sv.Malop = Console.ReadLine();

            // Nhập ngày sinh
            bool TMP = false;
            do
            {
                Console.Write("\t\t\tNhập ngày sinh (dd/MM/yyyy): ");
                string inputDate = Console.ReadLine();

                TMP = DateTime.TryParseExact(inputDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out sv.ngaysinh);

                if (!TMP)
                {
                    Console.WriteLine("\t\t\tNgày sinh không hợp lệ. Vui lòng nhập lại theo định dạng dd/MM/yyyy.");
                }

            } while (!TMP);

            // Nhập giới tính

            do
            {
                Console.Write("\t\t\tGiới tính (Nam/Nữ): ");
                sv.Gioitinh = Console.ReadLine().ToUpper();

                if (sv.Gioitinh.Equals("NAM") || sv.Gioitinh.Equals("NỮ"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\t\t\tGiới tính không hợp lệ ! Vui lòng nhập 'Nam' hoặc 'Nữ'.");
                }
            } while (true);

            do
            {
                Console.Write("\t\t\tNiên Khóa (yyyy-yyyy): ");
                sv.Nienkhoa = Console.ReadLine();
            } while (!KiemTraNienkhoa(sv.Nienkhoa));
            return sv;
        }
        public void KhaiBaoThongTinSinhVien()
        {
            Console.WriteLine("\t\t\tNhập thông tin sinh viên:");

            Console.Write("\t\t\tMã sinh viên: ");
            Masinhvien = Console.ReadLine();
            //Ràng buộc tên
            do
            {
                Console.Write("\t\t\tHọ tên: ");
                Hoten = Console.ReadLine();

                if (KiemTraChu(Hoten))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("H\t\t\tọ tên chỉ được chứa chữ cái. Vui lòng nhập lại.");
                }
            } while (true);

            do
            {
                Console.Write("\t\t\tDân tộc: ");
                Dantoc = Console.ReadLine();

                if (KiemTraChu(Dantoc))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\t\t\tDân tộc chỉ được chứa chữ cái. Vui lòng nhập lại.");
                }
            } while (true);

            do
            {
                Console.Write("\t\t\tTỉnh / TP: ");
                Thanhpho = Console.ReadLine();

                if (KiemTraChu(Thanhpho))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\t\t\tThành phố chỉ được chứa chữ cái. Vui lòng nhập lại.");
                }
            } while (true);

            do
            {
                Console.Write("\t\t\tSố điện thoại: ");
                string input = Console.ReadLine();

                if (double.TryParse(input, out sodienthoai) && input.Length >= 10 && input.Length <= 13)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\t\t\tSố điện thoại không hợp lệ. Vui lòng nhập lại.");
                }
            } while (true);

            Console.Write("\t\t\tMã lớp: ");
            Malop = Console.ReadLine();

            // Nhập ngày sinh
            bool TMP = false;
            do
            {
                Console.Write("\t\t\tNhập ngày sinh (dd/MM/yyyy): ");
                string inputDate = Console.ReadLine();

                TMP = DateTime.TryParseExact(inputDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out ngaysinh);

                if (!TMP)
                {
                    Console.WriteLine("\t\t\tNgày sinh không hợp lệ. Vui lòng nhập lại theo định dạng dd/MM/yyyy.");
                }

            } while (!TMP);

            // Nhập giới tính

            do
            {
                Console.Write("\t\t\tGiới tính (Nam/Nữ): ");
                Gioitinh = Console.ReadLine().ToUpper();

                if (Gioitinh.Equals("NAM") || Gioitinh.Equals("NỮ"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\t\t\tGiới tính không hợp lệ ! Vui lòng nhập 'Nam' hoặc 'Nữ'.");
                }
            } while (true);

            do
            {
                Console.Write("\t\t\tNiên Khóa (yyyy-yyyy): ");
                Nienkhoa = Console.ReadLine();
            } while (!KiemTraNienkhoa(Nienkhoa));
        }
        public void TieuDe()
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ Mã Sinh Viên  Họ Tên                    Dân tộc  Tỉnh / TP       Số Điện Thoại  Mã Lớp      Ngày Sinh   Giới Tính   Niên Khóa ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╣");
        }
        public void HienThiThongTinSinhVien()
        {
            Console.Write("  ");
            Console.Write($"{Masinhvien.PadRight(14)}");
            Console.Write($"{Hoten.PadRight(26)}");
            Console.Write($"{Dantoc.PadRight(9)}");
            Console.Write($"{Thanhpho.PadRight(18)}");
            Console.Write($"0{Sodienthoai.ToString().PadRight(12)}");
            Console.Write($"{Malop.PadRight(12)}");
            Console.Write($"{Ngaysinh.ToString("dd/MM/yyyy").PadRight(12)}");
            Console.Write($"{Gioitinh.PadRight(12)}");
            Console.Write($"{nienkhoa.PadRight(10)}\n");
        }
        // Kiểm tra xem chuỗi chỉ chứa chữ cái
        protected bool KiemTraChu(string input)
        {
            foreach (char character in input)
            {
                if (!char.IsLetter(character) && character != ' ')
                {
                    return false;
                }
            }
            return true;
        }
        static bool KiemTraNienkhoa(string input)
        {
            Regex regex = new Regex(@"^\d{4}-\d{4}$");
            return regex.IsMatch(input);
        }
    }
}
