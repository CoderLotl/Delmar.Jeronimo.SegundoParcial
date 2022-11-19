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
            newPlayer = textBox1.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
