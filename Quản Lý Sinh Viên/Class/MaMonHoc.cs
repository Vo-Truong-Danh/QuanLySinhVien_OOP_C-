using System;
using System.Collections.Generic;
using System.Xml;

namespace Quản_Lý_Sinh_Viên.Class
{
    public class MaMonHoc
    {
        public List<string> DocMaMonHocTuFile(string filePath)
        {
            List<string> maMonHocList = new List<string>();

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNodeList maMonHocNodes = doc.SelectNodes("/DTMH/MAMONHOC");
            foreach (XmlNode node in maMonHocNodes)
            {
                maMonHocList.Add(node.InnerText);
            }
            return maMonHocList;
        }

        public void HienThiDanhSachMonHoc(List<string> danhSachMonHoc)
        {
            if (danhSachMonHoc.Count == 0)
            {
                Console.WriteLine("\t\t\t   Không có môn học nào được tìm thấy.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n\n\n\t\t\t   Danh sách môn học:");
                foreach (string monHoc in danhSachMonHoc)
                {
                    Console.WriteLine($"\t\t\t\t{monHoc}");
                }
            }
        }

        public bool KiemTraMaMonHocTonTai(string maMonHoc, List<string> danhSachMonHoc)
        {
            return danhSachMonHoc.Contains(maMonHoc);
        }
    }
}
