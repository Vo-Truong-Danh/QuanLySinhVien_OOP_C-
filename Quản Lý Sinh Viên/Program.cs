using System.Text;
using Quản_Lý_Sinh_Viên.Class;

namespace Quản_Lý_Sinh_Viên
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = UnicodeEncoding.Unicode;
            Console.InputEncoding = UnicodeEncoding.Unicode;
            DanhSachTaiKhoan TK = new DanhSachTaiKhoan();
            TK.DangNhap();
        }
    }
}
