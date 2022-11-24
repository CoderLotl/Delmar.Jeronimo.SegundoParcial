using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library;

namespace Main
{
    public partial class FrmHistory : Form
    {
        List<HistoryRoom> listOfRooms;
        DataAccess dataAccess;
        DataTable roomsDataTable;

        public FrmHistory()
        {
            InitializeComponent();

            dataAccess = new DataAccess(GameMechanics.ConnectionString);
            listOfRooms = new List<HistoryRoom>();
            roomsDataTable = new DataTable();

            if(dataAccess.TestConnection() == true)
            {
                dataAccess.LoadMatchesHistory(listOfRooms);
                DrawDataTable();
            }
            else
            {
                MessageBox.Show("Unable to connect with the Database.\nHistory feature won't work.");
            }
            
            
        }

        private void DrawDataTable()
        {

            DataGridViewButtonColumn viewMatch = new DataGridViewButtonColumn()
            {
                Name = "DGV_ViewMatch",
                Text = "View",
                HeaderText = "View",
                UseColumnTextForButtonValue = true

            };

            Dgv_History.DataSource = null;
            Dgv_History.Columns.Clear();
            roomsDataTable.Clear();

            roomsDataTable.Columns.Add("Name", typeof(string));
            roomsDataTable.Columns.Add("Date", typeof(DateTime));

            foreach(HistoryRoom historyRoom in listOfRooms)
            {
                roomsDataTable.Rows.Add(historyRoom.RoomName, historyRoom.Date);
            }

            Dgv_History.DataSource = roomsDataTable;
            Dgv_History.Columns.Add(viewMatch);
        }

        private void Dgv_History_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Dgv_History.Columns[e.ColumnIndex].Name == "DGV_ViewMatch")
            {
                FrmViewHistory frmViewHistory = new FrmViewHistory(listOfRooms[Dgv_History.CurrentRow.Index]);
                frmViewHistory.Show();
            }
        }
    }
}
