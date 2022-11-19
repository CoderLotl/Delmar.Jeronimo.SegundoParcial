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
using System.Xml.Linq;
using Library;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Main
{
    public partial class FrmRoom : Form
    {
        Room room;
        Action DrawTree;
        bool gameConcluded;
        bool subscribed;
        Task Timer;
        Action<FrmRoom> FormClose;

        public bool Subscribed { get => subscribed; }
        public Room Room { get => room; }
        public bool GameConcluded { get => gameConcluded; set => gameConcluded = value; }

        public FrmRoom(Room room, Action DrawTree, Action<FrmRoom> FormClose)
        {
            InitializeComponent();
            this.room = room;
            this.DrawTree = DrawTree;
            GameConcluded = false;
            richTextBox1.Text = room.NewGame.Log;

            this.FormClose = FormClose;

            foreach(Player player in room.Players)
            {
                listBox1.DisplayMember = "name";
                listBox1.Items.Add(player);
            }

            Timer = new Task(() => this.StartTimer());

            richTextBox1.Rtf = room.NewGame.Log;
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
            lbl_P1Name.Text += room.Players[0].Name;
            lbl_P2Name.Text += room.Players[1].Name;
        }

        private void RefreshTxtBox(object? sender, EventArgs e)
        {
            if (this.richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new MethodInvoker(delegate { richTextBox1.Rtf = room.NewGame.Log; }));
                richTextBox1.Invoke(new MethodInvoker(delegate { richTextBox1.SelectionStart = richTextBox1.Text.Length; }));
                richTextBox1.Invoke(new MethodInvoker(delegate { richTextBox1.ScrollToCaret();}));
                lbl_P1Score.Invoke(new MethodInvoker(delegate { lbl_P1Score.Text = room.NewGame.PlayerOneScore.ToString(); }));
                lbl_P2Score.Invoke(new MethodInvoker(delegate { lbl_P2Score.Text = room.NewGame.PlayerTwoScore.ToString(); }));
            }
            else
            {
                richTextBox1.Rtf = room.NewGame.Log;
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
                lbl_P1Score.Text = room.NewGame.PlayerOneScore.ToString();
                lbl_P2Score.Text = room.NewGame.PlayerTwoScore.ToString();
            }
        }

        private void FrmRoom_FormClosing(object sender, FormClosingEventArgs e)
        {            
            if(GameConcluded == false)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        public void Subscribe()
        {
            room.NewGame.NotifyLogUpdate += RefreshTxtBox;
            room.NewGame.NotifyEndGame += GameFinished;
            subscribed = true;
        }

        public void Unsubscribe()
        {
            room.NewGame.NotifyLogUpdate -= RefreshTxtBox;
            room.NewGame.NotifyEndGame -= GameFinished;
            subscribed = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            room.NewGame.EndGame();
            button1.Enabled = false;
        }

        public void GameFinished(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { Timer.Start(); }));
            }
            else
            {
                Timer.Start();
            }
        }

        private void FrmRoom_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (GameConcluded)
            {
                GameMechanics.RemoveTrucoRoom(room, this.DrawTree);
            }                
        }

        private void StartTimer()
        {
            int timer = 120;

            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { lbl_Timer.Visible = true; }));
            }
            else
            {
                lbl_Timer.Visible = true;
            }
            

            for (int i = 0; i < timer; i++)
            {
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(delegate { lbl_Timer.Text = "This form is about to close in: " + (timer - i) + " seconds."; ; }));
                }
                else
                {
                    lbl_Timer.Text = "This form is about to close in: " + (timer - i) + " seconds.";
                }
                
                Thread.Sleep(1000);
            }
            GameConcluded = true;
            Unsubscribe();

            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { FormClose(this); }));
            }
            else
            {
                FormClose(this);
            }
            
        }

        private void ListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                MessageBox.Show(listBox1.SelectedItem.ToString());
            }
        }
    }
}
