using Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class FrmLobby : Form
    {
        // * * * * * * * * * * * * * * * *

        List<Room> rooms; // THIS IS HERE ONLY FOR DEBUG PURPOSES
        List<FrmRoom> viewForms;
        Action<string> Warning = (string text) => MessageBox.Show(text);
        Task Initialize;
        bool createViewForms;

        // * * * * * * * * * * * * * * * *

        public FrmLobby()
        {
            InitializeComponent();

            viewForms = new List<FrmRoom>();

            // * * * * *

            Initialize = new Task(() =>
            {
                GameMechanics.NotifyUpdate += SetInitialize;

                GameMechanics.InitializeLists(Warning);

                for (int i = 0; i < 1; i++)
                {
                    GameMechanics.AddTrucoRoom();
                    GameMechanics.rooms[i].NewGame.NotifyEndGame += EndGameHandler;                    
                }

                treeView1.Invoke((MethodInvoker)(() => DrawTree()));
                listBox1.Invoke((MethodInvoker)(() => DrawPlayersList()));
            });

            Initialize.Start();
        }

        // - - - - - - - - - - - - - - - -
        private void FrmLobby_Load(object sender, EventArgs e)
        {
            try
            {
                DrawTree();
                DrawPlayersList();
            }
            catch
            {

            }
        }

        // - - - - - - - - - - - - - - - - [ BUTTONS ]

        private void Btn_EndGame_Click(object sender, EventArgs e)
        {
            foreach (FrmRoom frmRoom in viewForms)
            {
                if (frmRoom.Text == treeView1.SelectedNode.Text)
                {
                    frmRoom.Room.NewGame.EndGame();
                    break;
                }
            }
        }

        // - - - - - - - - - - - - - - - - [ METHODS ]

        private void SetInitialize()
        {
            createViewForms = true;
        }

        // +++++++++ [ TREEVIEW ]

        private void DrawTree()
        {
            if (createViewForms == true)
            {
                CreateForms();
                createViewForms = false;
            }

            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    DrawTreeMethod();
                }));
            }
            else
            {
                DrawTreeMethod();
            }
        }

        // = = = = = = = = =

        private void DrawTreeMethod()
        {
            treeView1.Nodes.Clear();

            treeView1.Nodes.Add("Rooms");
            if (GameMechanics.rooms != null)
            {
                foreach (Room room in GameMechanics.rooms)
                {
                    TreeNode newRoomNode = new TreeNode();
                    newRoomNode.Text = room.Name;

                    if (room.GameConcluded == true)
                    {
                        newRoomNode.Text += " | GAME FINISHED";
                        newRoomNode.BackColor = Color.SteelBlue;
                    }
                    else
                    {
                        newRoomNode.Text += " | PLAYING";
                        newRoomNode.BackColor = Color.LightGreen;
                    }

                    foreach (Player player in room.Players)
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
        }

        // = = = = = = = = =

        private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            foreach (FrmRoom viewRoom in viewForms)
            {
                if (viewRoom.Text == e.Node.Text)
                {
                    if (viewRoom.Subscribed == false)
                    {
                        viewRoom.Subscribe();
                    }
                    viewRoom.Show();
                    break;
                }
            }
        }

        // = = = = = = = = =

        private void TreeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        // = = = = = = = = =

        private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 1)
            {
                label2.Text = e.Node.Text;
            }
            else
            {
                label2.Text = "";
            }
        }

        // = = = = = = = = =

        private void TreeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                MessageBox.Show(listBox1.SelectedItem.ToString());
            }
        }

        // +++++++++ [ MISCELLANEOUS ]

        private void CreateForms()
        {
            foreach (Room room in GameMechanics.rooms)
            {
                FrmRoom newViewForm = new FrmRoom(room, DrawTree, this.FormClose);
                newViewForm.Name = room.Name;
                newViewForm.Text = room.Name + " | PLAYING";
                newViewForm.Subscribe();

                viewForms.Add(newViewForm);
            }
        }

        // = = = = = = = = =

        private void DrawPlayersList()
        {
            if (GameMechanics.players != null)
            {
                foreach (Player player in GameMechanics.players)
                {
                    listBox1.DisplayMember = "name";
                    listBox1.Items.Add(player);
                }
            }
        }

        // = = = = = = = = =
        private void EndGameHandler(object sender, EventArgs e)
        {
            foreach (FrmRoom frmRoom in viewForms)
            {
                if (frmRoom.Room.NewGame == sender)
                {
                    Invoke(new MethodInvoker(delegate { frmRoom.Text = frmRoom.Name + " | GAME FINISHED"; }));
                    frmRoom.Room.GameConcluded = true;
                    DrawTree();
                    break;
                }
            }
        }

        // = = = = = = = = =

        private void FormClose(string roomName)
        {
            foreach (FrmRoom viewForm in viewForms)
            {
                if (viewForm.Name == roomName)
                {
                    Thread.Sleep(2000);
                    Invoke(new MethodInvoker(delegate { viewForm.Close(); }));
                    break;
                }
            }
        }

        // = = = = = = = = =

        private void FormClose(FrmRoom form)
        {
            GameMechanics.rooms.Remove(form.Room);
            viewForms.Remove(form);
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate {
                    form.Close();
                    form.Dispose();
                }));
            }
            else
            {
                form.Close();
                form.Dispose();
            }
            DrawTree();
        }
    }
}
