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
    public partial class FrmStatistics : Form
    {
        DataTable playersDT;

        public FrmStatistics()
        {
            InitializeComponent();

            playersDT = new DataTable();

            DrawDataTable();
        }

        private void DrawDataTable()
        {
            playersDT.Columns.Add("Name", typeof(string));
            playersDT.Columns.Add("Games Played", typeof(int));
            playersDT.Columns.Add("Games Won", typeof(int));
            playersDT.Columns.Add("Games Lost", typeof(int));
            playersDT.Columns.Add("Games Tied", typeof(int));

            foreach(Player player in SystemManager.Players)
            {
                playersDT.Rows.Add(player.Name, player.GamesPlayed, player.GamesWon, player.GamesLost, player.GamesTied);
            }

            Dgv_PassView.DataSource = playersDT;
        }
    }
}
