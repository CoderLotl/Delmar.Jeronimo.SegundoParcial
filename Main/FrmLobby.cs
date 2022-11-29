using Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class FrmLobby : Form
    {
        #region Header

        List<FrmRoom> viewForms;
        Action<string> Warning = (string text) => MessageBox.Show(text);
        Task Initialize;
        bool createViewForms;

        #endregion

        public FrmLobby()
        {
            InitializeComponent();
            viewForms = new List<FrmRoom>();

            Initialize = new Task(() =>
            {
                SystemManager.NotifyUpdate += SetInitialize;

                SystemManager.InitializeLists(Warning);

                Invoke((MethodInvoker)(() => btn_CreateRoom.Enabled = true));

                for (int i = 0; i < 1; i++)
                {
                    try
                    {
                        SystemManager.AddTrucoRoom(Warning);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                        return;
                    }

                    SystemManager.Rooms[i].NewGame.NotifyEndGame += EndGameHandler;
                    if (InvokeRequired)
                    {
                        Invoke((MethodInvoker)(() => DrawTree()));
                        Invoke((MethodInvoker)(() => DrawPlayersList()));
                    }
                    else
                    {
                        DrawTree();
                        DrawPlayersList();
                    }
                }
                createViewForms = false;
                EnableAll();
            });

            Initialize.Start();
        }


        private void FrmLobby_Load(object sender, EventArgs e)
        {
            DrawTree();
            DrawPlayersList();
        }

        #region Buttons

        private void Btn_EndGame_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Level == 1)
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
            }
        }

        private void btn_History_Click(object sender, EventArgs e)
        {
            FrmHistory frmHistory = new FrmHistory();
            frmHistory.Show();
        }

        private void btn_NewPlayer_Click(object sender, EventArgs e)
        {
            string newPlayerName;
            Player newPlayer;
            DataAccess dataAccess = new DataAccess(SystemManager.ConnectionString);

            FrmNewPlayer frmNewPlayer = new FrmNewPlayer();

            if (frmNewPlayer.ShowDialog() == DialogResult.OK)
            {
                newPlayerName = frmNewPlayer.NewPlayer;
                if (!string.IsNullOrWhiteSpace( newPlayerName))
                {
                    newPlayer = new();
                    newPlayer.Name = newPlayerName;
                    newPlayer.GamesPlayed = 0;
                    newPlayer.GamesLost = 0;
                    newPlayer.GamesWon = 0;
                    newPlayer.GamesTied = 0;

                    SystemManager.Players.Add(newPlayer);
                    DrawPlayersList();

                    if (dataAccess.TestConnection() == true)
                    {
                        dataAccess.InsertPlayer(newPlayer, Warning);
                    }
                    else
                    {
                        Warning("Unable to connect with the Database.\nThe player won't be written into it.");
                    }
                }
                else
                {
                    Warning("The Player has no name.");
                }
            }
        }

        private void btn_DeletePlayer_Click(object sender, EventArgs e)
        {
            Player newPlayer;
            DataAccess dataAccess = new DataAccess(SystemManager.ConnectionString);

            FrmDeletePlayer frmDeletePlayer = new FrmDeletePlayer();

            if (frmDeletePlayer.ShowDialog() == DialogResult.OK)
            {
                newPlayer = frmDeletePlayer.PlayerToDelete;

                if(CheckIfPlayerIsPlaying(newPlayer) == false)
                {
                    if (dataAccess.TestConnection() == true)
                    {
                        dataAccess.DeletePlayer(newPlayer, Warning);
                        SystemManager.Players.Remove(newPlayer);
                    }
                    else
                    {
                        Warning("Unable to connect with the Database.\nThe player won't be deleted.");
                    }

                    DrawPlayersList();
                }
                else
                {
                    Warning("You can't delete a player that's still playing.");
                }
            }
        }

        private void btn_Statistics_Click(object sender, EventArgs e)
        {
            FrmStatistics statistics = new FrmStatistics();

            statistics.Show();
        }

        private void Btn_CreateRoom_Click(object sender, EventArgs e)
        {
            FrmAddRoom frmAddRoom = new(Warning);
            if (frmAddRoom.ShowDialog() == DialogResult.OK)
            {
                SystemManager.Rooms.Add(frmAddRoom.NewRoom);
                frmAddRoom.NewRoom.NewGame.NotifyEndGame += EndGameHandler;

                //-----

                CreateNewForm(frmAddRoom);

                DrawTree();
            }
        }

        #endregion

        #region Methods

        private void SetInitialize()
        {
            createViewForms = true;
        }

        private void DrawTree()
        {
            if (createViewForms == true)
            {
                CreateForms();
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

        private void DrawTreeMethod()
        {
            treeView1.Nodes.Clear();

            treeView1.Nodes.Add("Rooms");
            if (SystemManager.Rooms != null)
            {
                foreach (Room room in SystemManager.Rooms)
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

                label1.Text = "Open rooms: " + SystemManager.Rooms.Count;
            }
        }

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

        private void TreeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

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

        private void ListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                MessageBox.Show(listBox1.SelectedItem.ToString());
            }
        }

        private void CreateForms()
        {
            foreach (Room room in SystemManager.Rooms)
            {
                FrmRoom newViewForm = new FrmRoom(room, DrawTree, this.FormClose);
                newViewForm.Name = room.Name;
                newViewForm.Text = room.Name + " | PLAYING";
                newViewForm.Subscribe();

                viewForms.Add(newViewForm);
            }
        }

        private void DrawPlayersList()
        {
            listBox1.Items.Clear();
            if (SystemManager.Players != null)
            {
                foreach (Player player in SystemManager.Players)
                {
                    listBox1.DisplayMember = "name";
                    listBox1.Items.Add(player);
                }
            }
        }

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

        private void FormClose(FrmRoom form)
        {
            SystemManager.Rooms.Remove(form.Room);
            viewForms.Remove(form);
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
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

        private void CreateNewForm(FrmAddRoom frmAddRoom)
        {
            FrmRoom frmRoom = new(frmAddRoom.NewRoom, DrawTree, this.FormClose);
            frmRoom.Name = frmAddRoom.NewRoom.Name;
            frmRoom.Text = frmAddRoom.NewRoom.Name + " | PLAYING";
            frmRoom.Subscribe();

            viewForms.Add(frmRoom);
        }

        private void EnableAll()
        {
            if (btn_CreateRoom.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    btn_DeletePlayer.Enabled = true;
                    btn_NewPlayer.Enabled = true;
                    btn_Statistics.Enabled = true;
                    btn_CreateRoom.Enabled = true;
                    btn_EndGame.Enabled = true;
                    btn_History.Enabled = true;
                }));
            }
        }

        private bool CheckIfPlayerIsPlaying(Player player)
        {
            bool playerIsPlaying = false;

            foreach(Room room in SystemManager.Rooms)
            {
                foreach(Player playerInRoom in room.Players)
                {
                    if(player == playerInRoom)
                    {
                        playerIsPlaying = true;
                        break;
                    }
                }
            }

            return playerIsPlaying;
        }

        #endregion


    }
}
