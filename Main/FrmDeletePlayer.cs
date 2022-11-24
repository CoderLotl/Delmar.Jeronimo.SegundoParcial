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
    public partial class FrmDeletePlayer : Form
    {
        Player playerToDelete;

        public Player PlayerToDelete { get => playerToDelete; }

        public FrmDeletePlayer()
        {
            InitializeComponent();

            DrawPlayersList();            
        }

        private void DrawPlayersList()
        {
            foreach (Player player in SystemManager.Players)
            {
                comboBox1.DisplayMember = "name";
                comboBox1.Items.Add(player);
            }
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void Btn_Accept_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem != null)
            {
                playerToDelete = (Player)comboBox1.SelectedItem;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("You must select a player before confirming.");
            }
        }
    }
}
