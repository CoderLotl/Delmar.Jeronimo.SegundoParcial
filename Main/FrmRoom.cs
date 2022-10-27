using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library;

namespace Main
{
    public partial class FrmRoom : Form
    {
        Room room;
        Task refresh;

        public FrmRoom(Room room)
        {
            InitializeComponent();
            this.room = room;
            richTextBox1.Text = room.NewGame.Log;            
            refresh = new Task(RefreshTxtBox);
            refresh.Start();
        }

        private void RefreshTxtBox()
        {
            while(true)
            {
                if (richTextBox1.InvokeRequired)
                {
                    richTextBox1.Invoke(new MethodInvoker(delegate { richTextBox1.Text = room.NewGame.Log; }));
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
