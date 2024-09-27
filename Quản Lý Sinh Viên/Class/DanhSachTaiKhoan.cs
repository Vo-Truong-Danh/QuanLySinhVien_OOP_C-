using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Quản_Lý_Sinh_Viên.Class
{
    internal class DanhSachTaiKhoan : TaiKhoan
    {
        List<TaiKhoan> DSTaiKhoan;
        public DanhSachTaiKhoan()
        {
            DSTaiKhoan = new List<TaiKhoan>(); // Khởi tạo danh sách tại đây
        }
        internal List<TaiKhoan> DSTaiKhoan1 { get => DSTaiKhoan; set => DSTaiKhoan = value; }

        public void NhapDSTaiKhoan(string File)
        {
            XmlDocument read = new XmlDocument();
            read.Load(File);

            XmlNodeList TaiKhoanS = read.SelectNodes("DSTK/TAIKHOAN");
            foreach (XmlNode item in TaiKhoanS)
            {
                TaiKhoan tk = new TaiKhoan();
                tk.Loai = int.Parse(item["LOAI"].InnerText);
                tk.User = item["USER"].InnerText;
                tk.Password = item["PASSWORD"].InnerText;
                DSTaiKhoan.Add(tk);
            }
        }
        public int KiemTraTaiKhoan(string user, string password)
        {
            foreach (TaiKhoan tk in DSTaiKhoan)
            {
                if (tk.User == user && tk.Password == password)
                {
                    return tk.Loai; // User và password khớp trả về loại tài khoản
                }
            }
            return 0; // User và password sai
        }
        public int NhapSoNguyen()
        {
            string choice;
            while (true)
            {
                Console.Write("\t\t\t   Lựa chọn: ");
                choice = Console.ReadLine();

                if (int.TryParse(choice, out int result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("\t\t\t  Nhập sai !!! Hãy thử lại:");
                }
            }
        }
        public bool ThemTaiKhoanVaoFileXml(string file, string user, string password, int loai)
        {
            // Tạo một đối tượng XmlDocument để tải tệp XML
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(file);

            // Tạo một đối tượng XmlElement mới cho tài khoản
            XmlElement taiKhoanElement = xmlDoc.CreateElement("TAIKHOAN");

            // Tạo các phần tử con cho tài khoản: LOAI, USER, PASSWORD
            XmlElement loaiElement = xmlDoc.CreateElement("LOAI");
            loaiElement.InnerText = loai.ToString();
            taiKhoanElement.AppendChild(loaiElement);

            XmlElement userElement = xmlDoc.CreateElement("USER");
            userElement.InnerText = user;
            taiKhoanElement.AppendChild(userElement);

            XmlElement passwordElement = xmlDoc.CreateElement("PASSWORD");
            passwordElement.InnerText = password;
            taiKhoanElement.AppendChild(passwordElement);

            // Thêm tài khoản mới vào nút gốc DSTK
            XmlNode root = xmlDoc.SelectSingleNode("DSTK");
            root.AppendChild(taiKhoanElement);

            // Lưu tệp XML sau khi thêm tài khoản
            xmlDoc.Save(file);
            return true;
        }
        public static string NhapMatKhauAn()
        {
            string password = "";
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (key.Key != ConsoleKey.Backspace)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Remove(password.Length - 1);
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    }
                }
            }
            return password;
        }

        string FileTaiKhoan = "..\\..\\..\\..\\..\\Đồ Án _ Quản Lý Sinh Viên\\Quản Lý Sinh Viên\\DanhSach\\DanhSachTaiKhoan.xml";
        string FileDanhSachTTSinhVien = "..\\..\\..\\..\\..\\Đồ Án _ Quản Lý Sinh Viên\\Quản Lý Sinh Viên\\DanhSach\\DanhSachSinhViên.xml";
        string FileDiemMonHoc = "..\\..\\..\\..\\..\\Đồ Án _ Quản Lý Sinh Viên\\Quản Lý Sinh Viên\\DanhSach\\DanhSachDiemMonHoc.xml";
        string FileMaMonHoc = "..\\..\\..\\..\\..\\Đồ Án _ Quản Lý Sinh Viên\\Quản Lý Sinh Viên\\DanhSach\\DanhSachMaMonHoc.xml";
        public void DangNhap()
        {
            string username;
            string password;
            DanhSachTaiKhoan dstk = new DanhSachTaiKhoan();
            dstk.NhapDSTaiKhoan(FileTaiKhoan);

            while (true)
            {
                Console.WriteLine("\n\n\n\t\t\t╔════════════════════════════════════════════╗");
                Console.WriteLine("\t\t\t║           ĐĂNG NHẬP TÀI KHOẢN              ║");
                Console.WriteLine("\t\t\t╠════════════════════════════════════════════╣");
                Console.Write("\t\t\t  Tên người dùng: ");
                username = Console.ReadLine();
                Console.Write("\t\t\t  Nhập mật khẩu: ");
                password = NhapMatKhauAn();


                if (dstk.KiemTraTaiKhoan(username, password) == 0)
                {
                    Console.WriteLine("\n\t\t\t╠════════════════════════════════════════════╣");
                    Console.WriteLine("\t\t\t║         Đăng Nhập Không Thành Công         ║");
                    Console.WriteLine("\t\t\t║     Tài Khoản Hoặc Mật Khẩu Không Đúng     ║");
                    Console.WriteLine("\t\t\t║         Ấn Phím Bất Kỳ Để Thử Lại          ║");
                    Console.WriteLine("\t\t\t╚════════════════════════════════════════════╝");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    break; // Thoát khỏi vòng lặp khi đăng nhập thành công
                }
            }

            Console.WriteLine("\n\t\t\t╠════════════════════════════════════════════╣");
            Console.WriteLine("\t\t\t║            Đăng Nhập Thành Công            ║");
            Console.WriteLine("\t\t\t║          Ấn Phím Bất Kỳ Để Tiếp Tục        ║");
            Console.WriteLine("\t\t\t╚════════════════════════════════════════════╝");
            Console.ReadKey();

            if (dstk.KiemTraTaiKhoan(username, password) == 1)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\n\n\n");
                    Console.WriteLine("\t\t\t╔════════════════════════════════════════════╗");
                    Console.WriteLine("\t\t\t║          TÀI KHOẢN QUẢN TRỊ VIÊN           ║");
                    Console.WriteLine("\t\t\t╠════════════════════════════════════════════╣");
                    Console.WriteLine("\t\t\t║ 1. Quản Lý Thông Tin Sinh Viên             ║");
                    Console.WriteLine("\t\t\t║ 2. Quản Lý Điểm Sinh Viên                  ║");
                    Console.WriteLine("\t\t\t║ 3. Quản Lý Điểm và Thông Tin Sinh Viên     ║");
                    Console.WriteLine("\t\t\t║ 4. Quản Lý Danh Sách Lớp                   ║");
                    Console.WriteLine("\t\t\t║ 5. Cấp Tài Khoản                           ║");
                    Console.WriteLine("\t\t\t║ 0. Đăng Xuất                               ║");
                    Console.WriteLine("\t\t\t╚════════════════════════════════════════════╝");
                    int tmp = NhapSoNguyen();
                    switch (tmp)
                    {
                        case 1:
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("\n\n\n");
                                Console.WriteLine("\t\t\t╔════════════════════════════════════════════╗");
                                Console.WriteLine("\t\t\t║          TÀI KHOẢN QUẢN TRỊ VIÊN           ║");
                                Console.WriteLine("\t\t\t╠════════════════════════════════════════════╣");
                                Console.WriteLine("\t\t\t║ 1. Xem thông tin danh sách sinh viên       ║");
                                Console.WriteLine("\t\t\t║ 2. Thêm thông tin sinh viên                ║");
                                Console.WriteLine("\t\t\t║ 3. Xóa thông tin sinh viên                 ║");
                                Console.WriteLine("\t\t\t║ 4. Tìm thông tin sinh viên theo mã         ║");
                                Console.WriteLine("\t\t\t║ 5. Cập nhật thông tin theo mã sinh viên    ║");
                                Console.WriteLine("\t\t\t║ 6. Xem danh sach theo Tỉnh / TP            ║");
                                Console.WriteLine("\t\t\t║ 0. Thoát                                   ║");
                                Console.WriteLine("\t\t\t╚════════════════════════════════════════════╝");
                                int temp = NhapSoNguyen();
                                switch (temp)
                                {
                                    case 1:
                                        DanhSachSinhVien dssv1 = new DanhSachSinhVien();
                                        dssv1.NhapDSSV_TuFile(FileDanhSachTTSinhVien);
                                        Console.Clear();
                                        dssv1.XuatDSSV();
                                        Console.ReadKey();
                                        break;
                                    case 2:
                                        DanhSachSinhVien dssv2 = new DanhSachSinhVien();
                                        dssv2.NhapDSSV_TuFile(FileDanhSachTTSinhVien);
                                        SinhVien sv = new SinhVien();
                                        sv = sv.KhaiBaoThongTinSinhVien(sv);
                                        bool temps = dssv2.ThemSinhVienVaoFileXml(FileDanhSachTTSinhVien, sv);
                                        if (temps)
                                        {
                                            Console.WriteLine("\t\t\tThêm Sinh Viên Thành Công");
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Thêm Sinh Viên Thất Bại");
                                            Console.ReadKey();
                                        }
                                        break;

                                    case 3:
                                        DanhSachSinhVien dssv3 = new DanhSachSinhVien();
                                        dssv3.NhapDSSV_TuFile(FileDanhSachTTSinhVien);
                                        Console.Write("\n\t\t\t   Nhập mã sinh viên cần thông tin : ");
                                        string macodiemcanxoa = Console.ReadLine();
                                        bool gan = dssv3.XoaSinhVienTheoMa(FileDanhSachTTSinhVien, macodiemcanxoa);

                                        // Kiểm tra kết quả xóa và hiển thị thông báo tương ứng
                                        if (gan)
                                        {
                                            Console.WriteLine($"\t\t\t   Đã xóa thành công sinh viên có mã {macodiemcanxoa}.");
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            Console.WriteLine($"\t\t\t   Không tìm thấy sinh viên có mã {macodiemcanxoa} để xóa.");
                                            Console.ReadKey();
                                        }
                                        // Xóa thông tin sinh viên

                                        break;

                                    case 4:
                                        DanhSachSinhVien dssv4 = new DanhSachSinhVien();
                                        dssv4.NhapDSSV_TuFile(FileDanhSachTTSinhVien);
                                        Console.Write("\t\t\tNhập mã sinh viên cần tìm : ");
                                        string macantim = Console.ReadLine();
                                        if (dssv4.TimSinhVienTheoMa(macantim) != null)
                                        {
                                            Console.Clear();
                                            dssv4.TieuDe();
                                            dssv4.TimKiemVaXuatThongTinTheoMa(macantim);
                                        }
                                        else Console.WriteLine($"\t\t\tKhông tìm thấy mã sinh viên :{macantim}");
                                        Console.ReadKey();
                                        break;
                                    case 5:
                                        //Cập nhật thông tin SV
                                        DanhSachSinhVien dssv5 = new DanhSachSinhVien();
                                        dssv5.NhapDSSV_TuFile(FileDanhSachTTSinhVien);
                                        Console.Write("\n\t\t\tNhập mã sinh viên cần cập nhật : ");
                                        string macodiemcancapnhat = Console.ReadLine();
                                        //xoa thông tin
                                        bool gan1 = dssv5.XoaSinhVienTheoMa(FileDanhSachTTSinhVien, macodiemcancapnhat);
                                        if (gan1 == true)
                                        {
                                            SinhVien sv1 = new SinhVien();
                                            sv1 = sv1.KhaiBaoThongTinSinhVien(sv1);
                                            //them lại thông tin mới
                                            bool temp1 = dssv5.ThemSinhVienVaoFileXml(FileDanhSachTTSinhVien, sv1);
                                            if (temp1 == true && gan1 == true)
                                            {
                                                Console.WriteLine("\t\t\tCập Nhật Sinh Viên Thành Công");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("\t\t\tCập Nhật Sinh Viên Thất Bại");
                                        }
                                        Console.ReadKey();
                                        break;
                                        // Kiểm tra kết quả xóa và hiển thị thông báo tương ứng

                                        break;
                                    case 6:
                                        //Tìm theo điều khiện
                                        DanhSachSinhVien dssv6 = new DanhSachSinhVien();
                                        dssv6.NhapDSSV_TuFile(FileDanhSachTTSinhVien);
                                        List<SinhVien> dstp = dssv6.DsSVTheoQuaQuan();
                                        if (dstp.Count == 0)
                                        {
                                            Console.Write("\t\t\t  Không Có Sinh Viên Nào Thuộc Tỉnh Thành Bạn Cần Tìm");
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            SinhVien svgan = new SinhVien();
                                            svgan.TieuDe();
                                            foreach (var item in dstp)
                                            {
                                                item.HienThiThongTinSinhVien();
                                            }
                                        }
                                        Console.ReadKey();
                                        break;

                                    case 0:
                                        break; // Thoát khỏi vòng lặp quản lý sinh viên

                                    default:
                                        Console.WriteLine("\t\t\tLựa chọn không hợp lệ. Vui lòng chọn lại.");
                                        break;
                                }

                                if (temp == 0)
                                    break; // Thoát khỏi vòng lặp quản lý sinh viên
                            }
                            break;

                        case 2:
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("\n\n\n");
                                Console.WriteLine("\t\t\t╔════════════════════════════════════════════╗");
                                Console.WriteLine("\t\t\t║          TÀI KHOẢN QUẢN TRỊ VIÊN           ║");
                                Console.WriteLine("\t\t\t╠════════════════════════════════════════════╣");
                                Console.WriteLine("\t\t\t║ 1. Xem điểm sinh viên                      ║");
                                Console.WriteLine("\t\t\t║ 2. Xem điểm với mã sinh viên               ║");
                                Console.WriteLine("\t\t\t║ 3. Thêm điểm sinh viên                     ║");
                                Console.WriteLine("\t\t\t║ 4. Xóa điểm sinh viên                      ║");
                                Console.WriteLine("\t\t\t║ 5. Tìm điểm bằng mã sinh viên              ║");
                                Console.WriteLine("\t\t\t║ 6. Cập nhật điểm theo mã sinh viên         ║");
                                Console.WriteLine("\t\t\t║ 7. Xem danh sach theo điểm lựa chọn        ║");
                                Console.WriteLine("\t\t\t║ 8. Xem danh sach theo mã lớp               ║");
                                Console.WriteLine("\t\t\t║ 9. Xuất danh sách sắp xếp theo điểm        ║");
                                Console.WriteLine("\t\t\t║ 0. Thoát                                   ║");
                                Console.WriteLine("\t\t\t╚════════════════════════════════════════════╝");
                                int temp = NhapSoNguyen();
                                switch (temp)
                                {
                                    case 1:
                                        DanhSachDiemMonHoc dsdmh1 = new DanhSachDiemMonHoc();
                                        dsdmh1.NhapDSDiemTuFile(FileDiemMonHoc);
                                        Console.Clear();
                                        dsdmh1.XuatDSDiemMonHoc();
                                        Console.ReadKey();
                                        break;
                                    case 2:
                                        //danh sách điểm với mã
                                        DanhSachDiemMonHoc dsdmh2 = new DanhSachDiemMonHoc();
                                        dsdmh2.NhapDSDiemTuFile(FileDiemMonHoc);
                                        Console.Clear();
                                        dsdmh2.XuatDSDiemVSMaSinhVien();
                                        Console.ReadKey();
                                        break;
                                    case 3:
                                        MaMonHoc mmh = new MaMonHoc();
                                        List<string> maMonHocs = mmh.DocMaMonHocTuFile(FileMaMonHoc);
                                        mmh.HienThiDanhSachMonHoc(maMonHocs);
                                        Console.Write("\t\t\tNhập mã môn học:");
                                        string maMonHocCanTim = Console.ReadLine();

                                        Console.Write("\t\t\tNhập mã sinh viên:");
                                        string maSinhVien= Console.ReadLine();
                                        bool ketQua = mmh.KiemTraMaMonHocTonTai(maMonHocCanTim, maMonHocs);
                                        if (ketQua)
                                        {
                                            DanhSachDiemMonHoc dsdmh3 = new DanhSachDiemMonHoc();
                                            dsdmh3.NhapDSDiemTuFile(FileDiemMonHoc);

                                            // Kiểm tra xem mã sinh viên đã tồn tại trong môn học hay chưa
                                            if (dsdmh3.KiemTraTonTaiMaSinhVienTrongMonHoc(maMonHocCanTim, maSinhVien))
                                            {
                                                Console.WriteLine("\t\t\t   Sinh viên đã có điểm trong môn học này.");
                                            }
                                            else
                                            {
                                                // Thêm điểm sinh viên nếu sinh viên chưa có điểm trong môn học
                                                DiemMonHoc diem1 = new DiemMonHoc();
                                                diem1 = diem1.ThemDiemThi(diem1, maMonHocCanTim, maSinhVien);
                                                bool temps = dsdmh3.ThemDiemMonHocVaoFileXml(FileDiemMonHoc, FileDanhSachTTSinhVien, diem1);
                                                if (temps)
                                                {
                                                    Console.WriteLine("\t\t\t   Thêm Điểm Cho Sinh Viên Thành Công");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("\t\t\t   Thêm Điểm Cho Sinh Viên Thất Bại");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"\t\t\t   Mã môn học {maMonHocCanTim} không tồn tại .");
                                        }

                                        Console.ReadKey();
                                        break;

                                    case 4:
                                        DanhSachDiemMonHoc dsdmh4 = new DanhSachDiemMonHoc();
                                        dsdmh4.NhapDSDiemTuFile(FileDiemMonHoc);
                                        // Xóa thông tin sinh viên
                                        Console.Write("\n\t\t\t   Nhập mã sinh viên cần xóa điểm : ");
                                        string macodiemcanxoa = Console.ReadLine();
                                        int soLuongduocXoa = dsdmh4.XoaDiemSinhVienTheoMa(FileDiemMonHoc, macodiemcanxoa);
                                        if (soLuongduocXoa == 0)
                                        {
                                            Console.WriteLine("\t\t\tKhông tìm thấy mã sinh viên hoặc không có đối tượng nào bị xóa.");
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            Console.WriteLine($"\t\t\tĐã xóa thành công {soLuongduocXoa} đối tượng.");
                                            Console.ReadKey();
                                        }
                                        break;

                                    case 5:
                                        DanhSachDiemMonHoc dsdmh5 = new DanhSachDiemMonHoc();
                                        dsdmh5.NhapDSDiemTuFile(FileDiemMonHoc);
                                        Console.Write("\t\t\t   Nhập mã sinh viên cần tìm : ");
                                        string macantim = Console.ReadLine();
                                        List<DiemMonHoc> dsdtm = dsdmh5.DSDiemTheoMaSV(macantim);
                                        if (dsdtm.Count > 0)
                                        {
                                            Console.Clear();
                                            dsdmh5.TieuDeXuatDiemVSMaSinhVien();
                                            foreach (DiemMonHoc diem in dsdtm)
                                            {
                                                diem.XuatDiemVSMaSinhVien();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"\t\t\t   Không tìm thấy mã sinh viên : {macantim}");
                                        }
                                        Console.ReadKey();
                                        break;
                                    case 6:
                                        DanhSachDiemMonHoc dsdmh6 = new DanhSachDiemMonHoc();
                                        dsdmh6.NhapDSDiemTuFile(FileDiemMonHoc);
                                        // cập nhật thông tin sinh viên
                                        Console.Write("\n\t\t\t   Nhập mã sinh viên cần cập nhật : ");
                                        string macodiemcancapnhat = Console.ReadLine();
                                        int dem = dsdmh6.XoaDiemSinhVienTheoMa(FileDiemMonHoc, macodiemcancapnhat);
                                        if(dem > 0)
                                        {
                                            DiemMonHoc diem2 = new DiemMonHoc();
                                            diem2 = diem2.ThemDiemThi(diem2);
                                            bool dem1 = dsdmh6.CapNhatDiemMonHocVaoFileXml(FileDiemMonHoc, FileDanhSachTTSinhVien, diem2);
                                            if (dem > 0 && dem1 == true)
                                            {
                                                Console.WriteLine("\t\t\tCập Nhật Điểm Cho Sinh Viên Thành Công");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Cập Nhật Cho Sinh Viên Thất Bại");
                                        }
                                        Console.ReadKey();
                                        break;
                                    case 7:
                                        DanhSachDiemMonHoc dsdmh7 = new DanhSachDiemMonHoc();
                                        dsdmh7.NhapDSDiemTuFile(FileDiemMonHoc);
                                        List<DiemMonHoc> dstheodiem = dsdmh7.DsDiemTheoYeuCau();
                                        DiemMonHoc TD = new DiemMonHoc();
                                        TD.TieuDeXuatDiemVSMaSinhVien();
                                        foreach (DiemMonHoc tmp1 in dstheodiem)
                                        {
                                            tmp1.XuatDiemVSMaSinhVien();
                                        }
                                        Console.ReadKey();
                                        break;
                                    case 8:
                                        DanhSachDiemMonHoc dsdmh8 = new DanhSachDiemMonHoc();
                                        dsdmh8.NhapDSDiemTuFile(FileDiemMonHoc);
                                        List<DiemMonHoc> dsdtheomamon = dsdmh8.DsDiemTheoMaLop();
                                        if (dsdtheomamon.Count > 0)
                                        {
                                            DiemMonHoc TD1 = new DiemMonHoc();
                                            TD1.TieuDeXuatDiemVSMaSinhVien();
                                            foreach (DiemMonHoc tmp1 in dsdtheomamon)
                                            {
                                                tmp1.XuatDiemVSMaSinhVien();
                                            }
                                        }
                                        else
                                            Console.WriteLine($"\t\t\tKhông tim thấy mã lớp");
                                        Console.ReadKey();
                                        break;
                                    case 9:
                                        DanhSachDiemMonHoc dsdmh9 = new DanhSachDiemMonHoc();
                                        dsdmh9.NhapDSDiemTuFile(FileDiemMonHoc);
                                        DiemMonHoc TD9 = new DiemMonHoc();
                                        List<DiemMonHoc> dsdaxapsep = dsdmh9.XapSepDiemMonHoc();
                                        Console.Clear();
                                        TD9.TieuDeXuatDiemVSMaSinhVien();
                                        foreach (DiemMonHoc tmp1 in dsdaxapsep)
                                        {
                                            tmp1.XuatDiemVSMaSinhVien();
                                        }
                                        Console.ReadKey();
                                        break;

                                    case 0:
                                        break; // Thoát khỏi vòng lặp quản lý sinh viên

                                    default:
                                        Console.WriteLine("\t\t\tLựa chọn không hợp lệ. Vui lòng chọn lại.");
                                        break;
                                }

                                if (temp == 0)
                                    break; // Thoát khỏi vòng lặp quản lý sinh viên
                            }
                            break;

                        case 3:
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("\n\n\n");
                                Console.WriteLine("\t\t\t╔════════════════════════════════════════════╗");
                                Console.WriteLine("\t\t\t║          TÀI KHOẢN QUẢN TRỊ VIÊN           ║");
                                Console.WriteLine("\t\t\t╠════════════════════════════════════════════╣");
                                Console.WriteLine("\t\t\t║ 1. Xem thông tin và điểm sinh viên         ║");
                                Console.WriteLine("\t\t\t║ 2. Thêm thông tin và điểm sinh viên        ║");
                                Console.WriteLine("\t\t\t║ 3. Xóa thông tin và điểm  sinh viên        ║");
                                Console.WriteLine("\t\t\t║ 4. Tìm điểm và thông tin bằng mã sinh viên ║");
                                Console.WriteLine("\t\t\t║ 5. Cập điểm và thông tin bằng mã sinh viên ║");
                                Console.WriteLine("\t\t\t║ 0. Thoát                                   ║");
                                Console.WriteLine("\t\t\t╚════════════════════════════════════════════╝");
                                int temp = NhapSoNguyen();
                                switch (temp)
                                {
                                    case 1:
                                        DanhSachSinhVien ds1 = new DanhSachSinhVien();
                                        DiemMonHoc diemMonHoc1 = new DiemMonHoc();
                                        DanhSachDiemMonHoc dsd1 = new DanhSachDiemMonHoc();
                                        ds1.NhapDSSV_TuFile(FileDanhSachTTSinhVien);
                                        dsd1.NhapDSDiemTuFile(FileDiemMonHoc);
                                        Console.Clear();
                                        ds1.XuatDSSVTheoDiem(ds1, dsd1);
                                        Console.ReadKey();
                                        break;
                                    case 2:
                                        // Thêm thông tin và diem sinh viên
                                        DanhSachSinhVien ds2 = new DanhSachSinhVien();
                                        DiemMonHoc diemMonHoc2 = new DiemMonHoc();
                                        DanhSachDiemMonHoc dsd2 = new DanhSachDiemMonHoc();
                                        ds2.NhapDSSV_TuFile(FileDanhSachTTSinhVien);
                                        dsd2.NhapDSDiemTuFile(FileDiemMonHoc);
                                        SinhVien sv = new SinhVien();
                                        sv = sv.KhaiBaoThongTinSinhVien(sv);
                                        bool themThanhCongThongTin = ds2.ThemSinhVienVaoFileXml(FileDanhSachTTSinhVien, sv);
                                        DiemMonHoc diem1 = new DiemMonHoc();
                                        diem1 = diem1.ThemDiemThiDaCoMa(diem1, sv.Masinhvien);
                                        bool themThanhCongDiemMon = dsd2.ThemDiemMonHocVaoFileXml(FileDiemMonHoc, FileDanhSachTTSinhVien, diem1);
                                        if (themThanhCongDiemMon && themThanhCongThongTin)
                                        {
                                            Console.WriteLine("Thêm sinh viên thành công.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Thêm sinh viên không thành công.");
                                        }
                                        break;

                                    case 3:
                                        // Xóa thông tin sinh viên
                                        DanhSachSinhVien ds3 = new DanhSachSinhVien();
                                        DiemMonHoc diemMonHoc3 = new DiemMonHoc();
                                        DanhSachDiemMonHoc dsd3 = new DanhSachDiemMonHoc();
                                        ds3.NhapDSSV_TuFile(FileDanhSachTTSinhVien);
                                        dsd3.NhapDSDiemTuFile(FileDiemMonHoc);
                                        Console.Write("\n\t\t\t   Nhập mã sinh viên cần xóa điểm và thông tin : ");
                                        string masinhviencanxoa = Console.ReadLine();
                                        bool gans = ds3.XoaSinhVienTheoMa(FileDanhSachTTSinhVien, masinhviencanxoa);
                                        dsd3.XoaDiemSinhVienTheoMa(FileDiemMonHoc, masinhviencanxoa);
                                        if (gans)
                                        {
                                            Console.WriteLine($"\t\t\t   Đã xóa thành công sinh viên có mã {masinhviencanxoa}.");
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            Console.WriteLine($"\t\t\t   Không tìm thấy sinh viên có mã {masinhviencanxoa} để xóa.");
                                            Console.ReadKey();
                                        }
                                        break;

                                    case 4:
                                        //Tìm SV
                                        DanhSachSinhVien ds4 = new DanhSachSinhVien();
                                        DiemMonHoc diemMonHoc4 = new DiemMonHoc();
                                        DanhSachDiemMonHoc dsd4 = new DanhSachDiemMonHoc();
                                        ds4.NhapDSSV_TuFile(FileDanhSachTTSinhVien);
                                        dsd4.NhapDSDiemTuFile(FileDiemMonHoc);

                                        Console.Write("\t\t\t   Nhập mã sinh viên cần tìm : ");
                                        string macantim = Console.ReadLine();
                                        List<DiemMonHoc> dsdtm = dsd4.DSDiemTheoMaSV(macantim);
                                        if (dsdtm.Count > 0)
                                        {
                                            Console.Clear();
                                            ds4.TieuDe();
                                            ds4.TimKiemVaXuatThongTinTheoMa(macantim);
                                            dsd4.TieuDeXuatDiem();
                                            foreach (DiemMonHoc diem in dsdtm)
                                            {
                                                diem.XuatDiem();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"\t\t\t   Không tìm thấy mã sinh viên : {macantim}");
                                        }
                                        Console.ReadKey();
                                        break;
                                    case 5:
                                        //Cập nhật Sinh Viên
                                        DanhSachSinhVien ds5 = new DanhSachSinhVien();
                                        DiemMonHoc diemMonHoc5 = new DiemMonHoc();
                                        DanhSachDiemMonHoc dsd5 = new DanhSachDiemMonHoc();
                                        ds5.NhapDSSV_TuFile(FileDanhSachTTSinhVien);
                                        dsd5.NhapDSDiemTuFile(FileDiemMonHoc);
                                        Console.Write("\n\t\t\t   Nhập mã sinh viên cần cập nhật điểm và thông tin : ");
                                        string masinhviencancapnhat = Console.ReadLine();
                                        bool gan5 = ds5.XoaSinhVienTheoMa(FileDanhSachTTSinhVien, masinhviencancapnhat);
                                        dsd5.XoaDiemSinhVienTheoMa(FileDiemMonHoc, masinhviencancapnhat);
                                        if (gan5 == true)
                                        {
                                            SinhVien sv5 = new SinhVien();
                                            sv5 = sv5.KhaiBaoThongTinSinhVien(sv5);
                                            bool dem5 = ds5.ThemSinhVienVaoFileXml(FileDanhSachTTSinhVien, sv5);
                                            DiemMonHoc diem5 = new DiemMonHoc();
                                            diem5 = diem5.ThemDiemThiDaCoMa(diem5, sv5.Masinhvien);
                                            bool dem5s = dsd5.ThemDiemMonHocVaoFileXml(FileDiemMonHoc, FileDanhSachTTSinhVien, diem5);
                                            if (dem5 && dem5s)
                                            {
                                                Console.WriteLine("Thêm sinh viên thành công.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Thêm sinh viên không thành công.");
                                        }
                                        break;

                                    case 0:
                                        break; // Thoát khỏi vòng lặp quản lý sinh viên

                                    default:
                                        Console.WriteLine("\t\t\tLựa chọn không hợp lệ. Vui lòng chọn lại.");
                                        break;
                                }

                                if (temp == 0)
                                    break; // Thoát khỏi vòng lặp quản lý sinh viên
                            }
                            break;

                        case 4:
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("\n\n\n");
                                Console.WriteLine("\t\t\t╔════════════════════════════════════════════╗");
                                Console.WriteLine("\t\t\t║          TÀI KHOẢN QUẢN TRỊ VIÊN           ║");
                                Console.WriteLine("\t\t\t╠════════════════════════════════════════════╣");
                                Console.WriteLine("\t\t\t║ 1. Xem thông tin danh sách các lớp         ║");
                                Console.WriteLine("\t\t\t║ 2. Thêm thông tin vào danh sách các lớp    ║");
                                Console.WriteLine("\t\t\t║ 3. Xóa thông tin trong danh sách các lớp   ║");
                                Console.WriteLine("\t\t\t║ 4. Xuất thông tin theo mã lớp              ║");
                                Console.WriteLine("\t\t\t║ 5. Cập thông tin danh sách lớp             ║");
                                Console.WriteLine("\t\t\t║ 0. Thoát                                   ║");
                                Console.WriteLine("\t\t\t╚════════════════════════════════════════════╝");
                                int temp = NhapSoNguyen();
                                switch (temp)
                                {
                                    case 1:
                                        DanhSachLopHoc dslh = new DanhSachLopHoc();
                                        LopHoc LH = new LopHoc();
                                        LH.TieuDeXuatDanhSachLopHoc();
                                        dslh.NhapDSLH_TuFile("..\\..\\..\\..\\..\\Đồ Án _ Quản Lý Sinh Viên\\Quản Lý Sinh Viên\\DanhSach\\DanhSachLopHoc.xml");
                                        dslh.xuat_DSLH();
                                        Console.ReadKey();
                                        break;
                                    case 2:
                                        // Thêm sinh viên vào lớp
                                        DanhSachLopHoc dslh2 = new DanhSachLopHoc();
                                        LopHoc LH2 = new LopHoc();
                                        LH2.TieuDeXuatDanhSachLopHoc();
                                        dslh2.NhapDSLH_TuFile("..\\..\\..\\..\\..\\Đồ Án _ Quản Lý Sinh Viên\\Quản Lý Sinh Viên\\DanhSach\\DanhSachLopHoc.xml");
                                        dslh2.xuat_DSLH();
                                        LH2.Nhap_TT_SV_Lop();
                                        bool temp2 = dslh2.ThemSinhVienVaoLopFileXml("..\\..\\..\\..\\..\\Đồ Án _ Quản Lý Sinh Viên\\Quản Lý Sinh Viên\\DanhSach\\DanhSachLopHoc.xml", LH2);
                                        if (temp2)
                                        {
                                            Console.Write("Thêm thành công.");
                                        }
                                        else
                                        {
                                            Console.Write("Thêm thất bại");
                                        }
                                        Console.ReadLine();
                                        break;

                                    case 3:
                                        // Xóa thông tin lớp
                                        DanhSachLopHoc dslh3 = new DanhSachLopHoc();
                                        LopHoc LH3 = new LopHoc();
                                        LH3.TieuDeXuatDanhSachLopHoc();
                                        dslh3.NhapDSLH_TuFile("..\\..\\..\\..\\..\\Đồ Án _ Quản Lý Sinh Viên\\Quản Lý Sinh Viên\\DanhSach\\DanhSachLopHoc.xml");
                                        dslh3.xuat_DSLH();
                                        Console.WriteLine("Nhập mã sinh viên cần xóa");
                                        string maCanXoa = Console.ReadLine();
                                        int deletedCount = dslh3.XoaSinhVienKhoiLopHocTheoMa("..\\..\\..\\..\\..\\Đồ Án _ Quản Lý Sinh Viên\\Quản Lý Sinh Viên\\DanhSach\\DanhSachLopHoc.xml", maCanXoa);

                                        if (deletedCount > 0)
                                        {
                                            Console.WriteLine("\t\t\t  Xóa sinh viên thành công!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("\t\t\t   Không tìm thấy sinh viên với mã số sinh viên đã cho.");
                                        }
                                        break;

                                    case 4:
                                        //Xuát theo mã
                                        DanhSachLopHoc dslh4 = new DanhSachLopHoc();
                                        LopHoc LH4 = new LopHoc();
                                        LH4.TieuDeXuatDanhSachLopHoc();
                                        dslh4.NhapDSLH_TuFile("..\\..\\..\\..\\..\\Đồ Án _ Quản Lý Sinh Viên\\Quản Lý Sinh Viên\\DanhSach\\DanhSachLopHoc.xml");
                                        dslh4.xuat_DSLH();
                                        List<LopHoc> dslhtheomalop = dslh4.TimSVTheoMaLop();
                                        if (dslhtheomalop.Count > 0)
                                        {
                                            LopHoc TD1 = new LopHoc();
                                            TD1.TieuDeXuatDanhSachLopHoc();
                                            foreach (LopHoc tmp1 in dslhtheomalop)
                                            {
                                                tmp1.Xuat_TT_SV_Lop();
                                            }
                                        }
                                        else
                                            Console.WriteLine($"\t\t\tKhông tim thấy mã lớp");
                                        Console.ReadKey();
                                        break;
                                    case 5:
                                        //Cập nhật Lớp
                                        DanhSachLopHoc dslh6 = new DanhSachLopHoc();
                                        LopHoc LH1 = new LopHoc();
                                        dslh6.NhapDSLH_TuFile("..\\..\\..\\..\\..\\Đồ Án _ Quản Lý Sinh Viên\\Quản Lý Sinh Viên\\DanhSach\\DanhSachLopHoc.xml");
                                        // cập nhật thông tin sinh viên
                                        Console.Write("\n\t\t\t   Nhập mã sinh viên cần cập nhật : ");
                                        string macodiemcancapnhat = Console.ReadLine();
                                        int dem = dslh6.XoaSinhVienKhoiLopHocTheoMa("..\\..\\..\\..\\..\\Đồ Án _ Quản Lý Sinh Viên\\Quản Lý Sinh Viên\\DanhSach\\DanhSachLopHoc.xml", macodiemcancapnhat);
                                        if(dem>0)
                                        {
                                            LH1.Nhap_TT_SV_Lop();
                                            bool dem2 = dslh6.ThemSinhVienVaoLopFileXml("..\\..\\..\\..\\..\\Đồ Án _ Quản Lý Sinh Viên\\Quản Lý Sinh Viên\\DanhSach\\DanhSachLopHoc.xml", LH1);
                                            bool dem1 = dslh6.CapNhatSinhVienTrongLopHocFileXml("..\\..\\..\\..\\..\\Đồ Án _ Quản Lý Sinh Viên\\Quản Lý Sinh Viên\\DanhSach\\DanhSachLopHoc.xml", LH1);
                                            if (dem > 0 && dem2 == true)
                                            {
                                                Console.WriteLine("\t\t\tCập Nhật Viên Thành Công");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Cập Nhật Thất Bại");
                                        }
                                        Console.ReadKey();
                                        break;

                                    case 0:
                                        break; // Thoát khỏi vòng lặp quản lý sinh viên

                                    default:
                                        Console.WriteLine("\t\t\tLựa chọn không hợp lệ. Vui lòng chọn lại.");
                                        break;
                                }

                                if (temp == 0)
                                    break; // Thoát khỏi vòng lặp quản lý sinh viên
                            }
                            break;
                        case 5:
                            DanhSachTaiKhoan danhSachTK = new DanhSachTaiKhoan();
                            Console.Clear();
                            Console.WriteLine("\n\n\t\t\t\t  Cấp Tài Khoản");
                            Console.Write("\n\n\t\t\t   Nhập Tên Tài Khoản : ");
                            string tk = Console.ReadLine();
                            string mk1 = "", mk2 = "";
                            do
                            {
                                if (mk1 != mk2)
                                {
                                    Console.WriteLine("\t\t\t  Mật Khẩu Nhập Lại Không Trùng Khớp");
                                }
                                Console.Write("\n\t\t\t   Nhập Password : ");
                                mk1 = NhapMatKhauAn();
                                Console.Write("\n\t\t\t   Nhập lại Password : ");
                                mk2 = NhapMatKhauAn();
                            } while (mk1 != mk2);
                            Console.Write("\n\t\t\t   Nhập loại tài khoản : ");
                            int loai = int.Parse(Console.ReadLine());
                            bool kt1 = danhSachTK.ThemTaiKhoanVaoFileXml(FileTaiKhoan, tk, mk1, loai);
                            if (kt1 == true)
                                Console.WriteLine($"\t\t\t   Thêm Tài Khoản Loại {loai} Thành Công ");
                            else
                                Console.WriteLine("\t\t\t  Thêm Tài Khoản Thất Bại");
                            Console.ReadKey();
                            break;

                        case 0:
                            return; // Thoát khỏi hàm DangNhap

                        default:
                            Console.WriteLine("\t\t\tLựa chọn không hợp lệ. Vui lòng chọn lại.");
                            break;
                    }
                }
            }
            else if (dstk.KiemTraTaiKhoan(username, password) == 2)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\n\n\n");
                    Console.WriteLine("\t\t\t╔════════════════════════════════════════════╗");
                    Console.WriteLine("\t\t\t║            TÀI KHOẢN SINH VIÊN             ║");
                    Console.WriteLine("\t\t\t╠════════════════════════════════════════════╣");
                    Console.WriteLine("\t\t\t║ 1. Xem thông tin sinh viên                 ║");
                    Console.WriteLine("\t\t\t║ 2. Xem điểm sinh viên                      ║");
                    Console.WriteLine("\t\t\t║ 3. Xem Thông Tin và Điểm Sinh Viên         ║");
                    Console.WriteLine("\t\t\t║ 0. Thoát                                   ║");
                    Console.WriteLine("\t\t\t╚════════════════════════════════════════════╝");
                    int temp = NhapSoNguyen();
                    switch (temp)
                    {
                        case 1:
                            DanhSachSinhVien dssv4 = new DanhSachSinhVien();
                            dssv4.NhapDSSV_TuFile(FileDanhSachTTSinhVien);
                            Console.Clear();
                            dssv4.TieuDe();
                            dssv4.TimKiemVaXuatThongTinTheoMa(username);
                            Console.ReadKey();
                            break;
                        case 2:
                            DanhSachDiemMonHoc dsdmh5 = new DanhSachDiemMonHoc();
                            dsdmh5.NhapDSDiemTuFile(FileDiemMonHoc);
                            List<DiemMonHoc> dsdtm = dsdmh5.DSDiemTheoMaSV(username);
                            Console.Clear();
                            dsdmh5.TieuDeXuatDiemVSMaSinhVien();
                            foreach (DiemMonHoc diem in dsdtm)
                            {
                                diem.XuatDiemVSMaSinhVien();
                            }
                            Console.ReadKey();
                            break;

                        case 3:// Xem thông tin sinh viên
                            DanhSachSinhVien dssv3 = new DanhSachSinhVien();
                            dssv3.NhapDSSV_TuFile(FileDanhSachTTSinhVien);
                            Console.Clear();
                            dssv3.TieuDe();
                            dssv3.TimKiemVaXuatThongTinTheoMa(username);
                            DanhSachDiemMonHoc dsdmh3 = new DanhSachDiemMonHoc();
                            dsdmh3.NhapDSDiemTuFile(FileDiemMonHoc);
                            List<DiemMonHoc> dsdtm3 = dsdmh3.DSDiemTheoMaSV(username);
                            dsdmh3.TieuDeXuatDiemVSMaSinhVien();
                            foreach (DiemMonHoc diem in dsdtm3)
                            {
                                diem.XuatDiemVSMaSinhVien();
                            }
                            Console.ReadKey();
                            break;

                        case 0:
                            break; // Thoát khỏi vòng lặp quản lý sinh viên

                        default:
                            Console.WriteLine("\t\t\tLựa chọn không hợp lệ. Vui lòng chọn lại.");
                            break;
                    }

                    if (temp == 0)
                        break; // Thoát khỏi vòng lặp quản lý sinh viên
                }
            }
        }
    }
}
