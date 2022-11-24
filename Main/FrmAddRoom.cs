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
    public partial class FrmAddRoom : Form
    {
        Player player1;
        Player player2;
        GameType gameType;
        GameSubType gameSubType;
        Room newRoom;
        string roomName;
        Action<string> warning;
                
        public Room NewRoom { get => newRoom;}        

        public FrmAddRoom(Action<string> warning)
        {
            InitializeComponent();
            this.warning = warning;
            PopulateCombobox();
        }

        private void PopulateCombobox()
        {
            foreach(Player player in SystemManager.Players)
            {
                comboBox1.DisplayMember = "name";
                comboBox1.Items.Add(player);
                comboBox2.DisplayMember = "name";
                comboBox2.Items.Add(player);
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBoxArranger(comboBox1, comboBox2);
        }


        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBoxArranger(comboBox2, comboBox1);
        }


        private void ComboBoxArranger(ComboBox combo1, ComboBox combo2)
        {
            if (combo2.SelectedItem == combo1.SelectedItem)
            {
                combo2.SelectedItem = null;
                combo2.Text = string.Empty;
                combo2.Items.Clear();

                foreach (Player player in SystemManager.Players)
                {
                    if (player != comboBox1.SelectedItem)
                    {
                        combo2.DisplayMember = "name";
                        combo2.Items.Add(player);
                    }
                }
            }
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void Btn_Accept_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                player1 = (Player)comboBox1.SelectedItem;
                player2 = (Player)comboBox2.SelectedItem;

                gameType = GameType.Cards;
                gameSubType = GameSubType.Truco;

                roomName = "Room #" + (SystemManager.ID + 1).ToString();
                SystemManager.ID++;

                newRoom = new Room(roomName, player1, player2, gameType, gameSubType, warning);

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                label1.Visible = true;
                label1.Text = "You must select 2 players to start a game.";
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Visible = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Visible = false;
        }
    }
}
