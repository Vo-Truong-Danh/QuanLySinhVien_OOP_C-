using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_Lý_Sinh_Viên.Class
{
    internal class TaiKhoan : SinhVien
    {
        string user, password;
        int loai;
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
        public int Loai { get => loai; set => loai = value; }
    }
}
