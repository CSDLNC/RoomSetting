using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement
{
    public partial class HotelSearching : Form
    {
        public HotelSearching()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            ConnectData.Connect();
            string sql = "SELECT ks.maKS ,ks.tenKS,ks.thanhPho,ks.soSao,ks.giaTB,ks.soNha,ks.duong,ks.quan,ks.mota FROM ((KhachSan ks JOIN LoaiPhong lp ON ks.maKS=lp.maKS) JOIN Phong p ON lp.maLoaiPhong=p.LoaiPhong) JOIN TrangThaiPhong tt ON p.maPhong=tt.maPhong";
            DataTable dsKS = new DataTable();
            dsKS = ConnectData.LoadData(string.Format(sql));
            for (int i = 0; i < dsKS.Rows.Count; i++)
                danhsachkhachsan.Rows.Add(dsKS.Rows[i][0], dsKS.Rows[i][1], dsKS.Rows[i][3], dsKS.Rows[i][5], dsKS.Rows[i][6], dsKS.Rows[i][7], dsKS.Rows[i][2],dsKS.Rows[i][4],dsKS.Rows[i][8]);
                

        }

    }
}
