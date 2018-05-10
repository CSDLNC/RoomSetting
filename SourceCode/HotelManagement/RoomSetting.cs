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

namespace HotelManagement
{
    public partial class RoomSetting : Form
    {
        public RoomSetting()
        {
            InitializeComponent();
            LoadData();
        }
        
        private void LoadData()
        {
            DataConnectSQL.Connect();
            string sql = "SELECT ks.maKS as STT,ks.tenKS as 'Tên khách sạn',ks.thanhPho as 'Thành phố',ks.soSao as 'Số sao',ks.giaTB as 'Giá cả',tt.maPhong as 'Số phòng',tt.tinhTrang as 'Tình trạng' FROM ((KhachSan ks JOIN LoaiPhong lp ON ks.maKS=lp.maKS) JOIN Phong p ON lp.maLoaiPhong=p.LoaiPhong) JOIN TrangThaiPhong tt ON p.maPhong=tt.maPhong";
            DataTable dsKS = new DataTable();
            dsKS = DataConnectSQL.LoadData(string.Format(sql));
            //danhsachphongtrong.DataSource = dsKS;
            //this.tenKS.DataGridView.Rows.Add(dsKS.Rows[1][1]);
            //this.tenKS.DataGridView.Rows.Add(dsKS.Rows[2][1]);
            //this.STT.DataGridView.Columns.Add(dsKS.Columns[0]);
            for (int i = 0; i < dsKS.Rows.Count; i++)
                danhsachphongtrong.Rows.Add(dsKS.Rows[i][0], dsKS.Rows[i][1], dsKS.Rows[i][2], dsKS.Rows[i][3], dsKS.Rows[i][4], dsKS.Rows[i][5], dsKS.Rows[i][6]);
            

        }
        private void InvokeUI(Action a)
        {
            BeginInvoke(new MethodInvoker(a));
        }
        private void InsertData(int index)
        {
            DataConnectSQL.Connect();
            string user  = "anguyen";
            int numGuest = 12;
            string numRoom = danhsachphongtrong.Rows[index].Cells[5].Value.ToString();
            int num;
            int.TryParse(numRoom,out num);
            DateTime dateIn = DateTime.Now;
            DateTime dateOut = dateIn.AddDays(3);
            int result;
            string sql = "sp_booking";

            //+  user +","+ numGuest + "," + numRoom + "," + checkIn + "," + checkOut + "," + result
            result = DataConnectSQL.ExcuteProcedure(sql,user,numGuest,num,dateIn,dateOut);
            if (result == 0)
            {
                MessageBox.Show("We're sorry about this !! \n This room is booked. Please try book the another!", "Error");
                danhsachphongtrong.CancelEdit();
            }
            else
            {
                MessageBox.Show("Successfull !!");
                DataConnectSQL.Connect();
            }
        }
        private void danhsachphongtrong_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
            var result = MessageBox.Show("Are you really agree book room?", "Warning !!!", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                sender = danhsachphongtrong.CancelEdit();
            }
            else
            {
                int rowIndex = -1;
                foreach (DataGridViewRow row in danhsachphongtrong.Rows)
                {
                    if (row.Cells[7].Selected == true)
                    {
                        rowIndex = row.Cells[7].RowIndex;
                        InsertData(rowIndex);
                        break;
                    }
                }
            }
        }

        private void QuayLaicl_Option_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void usernamelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }

        private void dangxuatlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }
        
    }
}
