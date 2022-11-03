using System;
using System.Collections;
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
    public partial class FrmLobby : Form
    {
        List<Room> rooms;
        Action<string> Warning = (string text) => MessageBox.Show(text);        
        Task Initialize;

        public FrmLobby()
        {
            InitializeComponent();

            // * * * * *

            Initialize = new Task(() =>
            {
                GameMechanics.InitializeLists(Warning);
                
                for (int i = 0; i < 3; i++)
                {
                    GameMechanics.AddTrucoRoom();
                }

                treeView1.Invoke((MethodInvoker)(() => DrawTree(1)));
                listBox1.Invoke((MethodInvoker)(() => DrawPlayersList()));
            });

            Initialize.Start();
        }

        private void FrmLobby_Load(object sender, EventArgs e)
        {
            try
            {
                DrawTree(0);
                DrawPlayersList();
            }
            catch
            {

            }
        }

        private void DrawTree(int opt)
        {
            if(opt == 1)
            {
                treeView1.Nodes.Clear();
            }

            treeView1.Nodes.Add("Rooms");
            foreach(Room room in GameMechanics.rooms)
            {
                TreeNode newRoomNode = new TreeNode();
                newRoomNode.Text = room.Name;

                foreach(Player player in room.Players)
                {
                    TreeNode newPlayerNode = new TreeNode();
                    newPlayerNode.Text = "Player: " + player.Name;
                    newRoomNode.Nodes.Add(newPlayerNode);
                }

                treeView1.Nodes[0].Nodes.Add(newRoomNode);
            }
            treeView1.Nodes[0].ExpandAll();

            label1.Text = "Open rooms: " + GameMechanics.rooms.Count;
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            foreach (Room room in GameMechanics.rooms)
            {
                if (room.Name == e.Node.Text)
                {                    
                    FrmRoom viewRoom = new FrmRoom(room);
                    viewRoom.Show();
                    break;
                }
            }
        }

        private void UpdateInfo()
        {
            DrawTree(1);
            DrawPlayersList();
        }

        private void treeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void DrawPlayersList()
        {
            foreach(Player player in GameMechanics.players)
            {
                listBox1.DisplayMember = "name";
                listBox1.Items.Add(player);
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Node.Level == 1)
            {
                label2.Text = e.Node.Text;
            }
            else
            {
                label2.Text = "";
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                MessageBox.Show(listBox1.SelectedItem.ToString());
            }
        }
    }
}
