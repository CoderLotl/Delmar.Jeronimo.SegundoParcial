using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class FrmNewPlayer : Form
    {
        string newPlayer;

        public string NewPlayer { get => newPlayer; }

        public FrmNewPlayer()
        {
            InitializeComponent();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void Btn_Accept_Click(object sender, EventArgs e)
        {
            if( !string.IsNullOrWhiteSpace( textBox1.Text ) )
            {
                newPlayer = textBox1.Text;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                label1.Visible = true;
                label1.Text = "The Name field can't be empty.";
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Visible = false;
        }
    }
}
