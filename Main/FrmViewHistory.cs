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
    public partial class FrmViewHistory : Form
    {
        HistoryRoom room;

        public FrmViewHistory(HistoryRoom room)
        {
            InitializeComponent();

            this.room = room;

            richTextBox1.Rtf = room.GameLog;
            this.Text = room.RoomName + " " + room.Date;
        }
    }
}
