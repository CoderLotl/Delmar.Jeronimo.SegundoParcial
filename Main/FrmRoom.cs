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

        public FrmRoom(Room room)
        {
            InitializeComponent();
            this.room = room;
            richTextBox1.Text = room.NewGame.Log;            

            room.NewGame.NotifyLogUpdate += RefreshTxtBox;

            richTextBox1.Rtf = room.NewGame.Log;
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void RefreshTxtBox(object? sender, EventArgs e)
        {

            richTextBox1.Rtf = room.NewGame.Log;
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
            
        }

        private void FrmRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            room.NewGame.NotifyLogUpdate -= RefreshTxtBox;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            room.NewGame.EndGame();
        }
    }
}
