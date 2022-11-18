﻿using Library;
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

                Invoke((MethodInvoker)(() => Btn_CreateRoom.Enabled = true));

                for (int i = 0; i < 1; i++)
                {
                    try
                    {
                        GameMechanics.AddTrucoRoom();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                        return;
                    }

                    GameMechanics.rooms[i].NewGame.NotifyEndGame += EndGameHandler;
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
            if (treeView1.SelectedNode != null)
            {
                if(treeView1.SelectedNode.Level == 1)
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
                //createViewForms = false;
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

        private void ListBox_MouseDoubleClick(object sender, MouseEventArgs e)
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
            listBox1.Items.Clear();
            if (GameMechanics.Players != null)
            {
                foreach (Player player in GameMechanics.Players)
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

        private void FormClose(FrmRoom form)
        {
            GameMechanics.rooms.Remove(form.Room);
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

        private void Btn_CreateRoom_Click(object sender, EventArgs e)
        {
            FrmAddRoom frmAddRoom = new();
            if (frmAddRoom.ShowDialog() == DialogResult.OK)
            {
                GameMechanics.rooms.Add(frmAddRoom.NewRoom);
                frmAddRoom.NewRoom.NewGame.NotifyEndGame += EndGameHandler;

                //-----

                CreateNewForm(frmAddRoom);

                DrawTree();
            }
        }

        private void CreateNewForm(FrmAddRoom frmAddRoom)
        {
            FrmRoom frmRoom = new(frmAddRoom.NewRoom, DrawTree, this.FormClose);
            frmRoom.Name = frmAddRoom.NewRoom.Name;
            frmRoom.Text = frmAddRoom.NewRoom.Name + " | PLAYING";
            frmRoom.Subscribe();

            viewForms.Add(frmRoom);
        }
    }
}
