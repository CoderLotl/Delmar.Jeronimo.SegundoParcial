namespace Main
{
    partial class FrmLobby
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_EndGame = new System.Windows.Forms.Button();
            this.btn_CreateRoom = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_NewPlayer = new System.Windows.Forms.Button();
            this.btn_DeletePlayer = new System.Windows.Forms.Button();
            this.btn_Statistics = new System.Windows.Forms.Button();
            this.btn_History = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.treeView1.Location = new System.Drawing.Point(6, 123);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(488, 290);
            this.treeView1.TabIndex = 0;
            this.treeView1.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_BeforeCollapse);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseClick);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.btn_EndGame);
            this.groupBox1.Controls.Add(this.btn_CreateRoom);
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 527);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Active Game Rooms";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(6, 75);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(488, 40);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Selected room:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(17, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 0;
            // 
            // btn_EndGame
            // 
            this.btn_EndGame.Enabled = false;
            this.btn_EndGame.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_EndGame.Location = new System.Drawing.Point(419, 419);
            this.btn_EndGame.Name = "btn_EndGame";
            this.btn_EndGame.Size = new System.Drawing.Size(75, 102);
            this.btn_EndGame.TabIndex = 3;
            this.btn_EndGame.Text = "End Game";
            this.btn_EndGame.UseVisualStyleBackColor = true;
            this.btn_EndGame.Click += new System.EventHandler(this.Btn_EndGame_Click);
            // 
            // btn_CreateRoom
            // 
            this.btn_CreateRoom.Enabled = false;
            this.btn_CreateRoom.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_CreateRoom.Location = new System.Drawing.Point(6, 423);
            this.btn_CreateRoom.Name = "btn_CreateRoom";
            this.btn_CreateRoom.Size = new System.Drawing.Size(75, 102);
            this.btn_CreateRoom.TabIndex = 2;
            this.btn_CreateRoom.Text = "Create New Room";
            this.btn_CreateRoom.UseVisualStyleBackColor = true;
            this.btn_CreateRoom.Click += new System.EventHandler(this.Btn_CreateRoom_Click);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.IndianRed;
            this.listBox1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.listBox1.ForeColor = System.Drawing.Color.YellowGreen;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 19;
            this.listBox1.Location = new System.Drawing.Point(6, 22);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(168, 289);
            this.listBox1.TabIndex = 3;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBox_MouseDoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Firebrick;
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox2.Location = new System.Drawing.Point(1043, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(180, 321);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Players currently online:";
            // 
            // btn_NewPlayer
            // 
            this.btn_NewPlayer.Enabled = false;
            this.btn_NewPlayer.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_NewPlayer.Location = new System.Drawing.Point(1085, 355);
            this.btn_NewPlayer.Name = "btn_NewPlayer";
            this.btn_NewPlayer.Size = new System.Drawing.Size(100, 70);
            this.btn_NewPlayer.TabIndex = 5;
            this.btn_NewPlayer.Text = "Create New Player";
            this.btn_NewPlayer.UseVisualStyleBackColor = true;
            this.btn_NewPlayer.Click += new System.EventHandler(this.btn_NewPlayer_Click);
            // 
            // btn_DeletePlayer
            // 
            this.btn_DeletePlayer.Enabled = false;
            this.btn_DeletePlayer.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_DeletePlayer.Location = new System.Drawing.Point(1085, 441);
            this.btn_DeletePlayer.Name = "btn_DeletePlayer";
            this.btn_DeletePlayer.Size = new System.Drawing.Size(100, 55);
            this.btn_DeletePlayer.TabIndex = 6;
            this.btn_DeletePlayer.Text = "Delete Player";
            this.btn_DeletePlayer.UseVisualStyleBackColor = true;
            this.btn_DeletePlayer.Click += new System.EventHandler(this.btn_DeletePlayer_Click);
            // 
            // btn_Statistics
            // 
            this.btn_Statistics.Enabled = false;
            this.btn_Statistics.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_Statistics.Location = new System.Drawing.Point(1085, 515);
            this.btn_Statistics.Name = "btn_Statistics";
            this.btn_Statistics.Size = new System.Drawing.Size(100, 55);
            this.btn_Statistics.TabIndex = 7;
            this.btn_Statistics.Text = "Statistics";
            this.btn_Statistics.UseVisualStyleBackColor = true;
            this.btn_Statistics.Click += new System.EventHandler(this.btn_Statistics_Click);
            // 
            // btn_History
            // 
            this.btn_History.Enabled = false;
            this.btn_History.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_History.Location = new System.Drawing.Point(1085, 587);
            this.btn_History.Name = "btn_History";
            this.btn_History.Size = new System.Drawing.Size(100, 55);
            this.btn_History.TabIndex = 8;
            this.btn_History.Text = "History";
            this.btn_History.UseVisualStyleBackColor = true;
            this.btn_History.Click += new System.EventHandler(this.btn_History_Click);
            // 
            // FrmLobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::Main.Properties.Resources.cabecera_truc;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1235, 654);
            this.Controls.Add(this.btn_History);
            this.Controls.Add(this.btn_Statistics);
            this.Controls.Add(this.btn_DeletePlayer);
            this.Controls.Add(this.btn_NewPlayer);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmLobby";
            this.Text = "Lobby";
            this.Load += new System.EventHandler(this.FrmLobby_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_EndGame;
        private System.Windows.Forms.Button btn_CreateRoom;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_NewPlayer;
        private System.Windows.Forms.Button btn_DeletePlayer;
        private System.Windows.Forms.Button btn_Statistics;
        private System.Windows.Forms.Button btn_History;
    }
}
