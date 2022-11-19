namespace Main
{
    partial class FrmRoom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_Timer = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbl_P2Score = new System.Windows.Forms.Label();
            this.lbl_P2Name = new System.Windows.Forms.Label();
            this.lbl_P1Score = new System.Windows.Forms.Label();
            this.lbl_P1Name = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.richTextBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.richTextBox1.Location = new System.Drawing.Point(22, 34);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(633, 358);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(6, 22);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(115, 154);
            this.listBox1.TabIndex = 2;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBox_MouseDoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Location = new System.Drawing.Point(809, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(133, 233);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Players";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(809, 260);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 46);
            this.button1.TabIndex = 4;
            this.button1.Text = "End this Game";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl_Timer
            // 
            this.lbl_Timer.AutoSize = true;
            this.lbl_Timer.Location = new System.Drawing.Point(22, 9);
            this.lbl_Timer.Name = "lbl_Timer";
            this.lbl_Timer.Size = new System.Drawing.Size(38, 15);
            this.lbl_Timer.TabIndex = 5;
            this.lbl_Timer.Text = "Timer";
            this.lbl_Timer.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lbl_P2Score);
            this.groupBox3.Controls.Add(this.lbl_P2Name);
            this.groupBox3.Controls.Add(this.lbl_P1Score);
            this.groupBox3.Controls.Add(this.lbl_P1Name);
            this.groupBox3.Location = new System.Drawing.Point(22, 398);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(439, 118);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Score";
            // 
            // lbl_P2Score
            // 
            this.lbl_P2Score.AutoSize = true;
            this.lbl_P2Score.Location = new System.Drawing.Point(208, 53);
            this.lbl_P2Score.Name = "lbl_P2Score";
            this.lbl_P2Score.Size = new System.Drawing.Size(12, 15);
            this.lbl_P2Score.TabIndex = 3;
            this.lbl_P2Score.Text = "-";
            // 
            // lbl_P2Name
            // 
            this.lbl_P2Name.AutoSize = true;
            this.lbl_P2Name.Location = new System.Drawing.Point(208, 26);
            this.lbl_P2Name.Name = "lbl_P2Name";
            this.lbl_P2Name.Size = new System.Drawing.Size(54, 15);
            this.lbl_P2Name.TabIndex = 2;
            this.lbl_P2Name.Text = "Player 2: ";
            // 
            // lbl_P1Score
            // 
            this.lbl_P1Score.AutoSize = true;
            this.lbl_P1Score.Location = new System.Drawing.Point(17, 53);
            this.lbl_P1Score.Name = "lbl_P1Score";
            this.lbl_P1Score.Size = new System.Drawing.Size(12, 15);
            this.lbl_P1Score.TabIndex = 1;
            this.lbl_P1Score.Text = "-";
            // 
            // lbl_P1Name
            // 
            this.lbl_P1Name.AutoSize = true;
            this.lbl_P1Name.Location = new System.Drawing.Point(17, 26);
            this.lbl_P1Name.Name = "lbl_P1Name";
            this.lbl_P1Name.Size = new System.Drawing.Size(54, 15);
            this.lbl_P1Name.TabIndex = 0;
            this.lbl_P1Name.Text = "Player 1: ";
            // 
            // FrmRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 546);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lbl_Timer);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.richTextBox1);
            this.Name = "FrmRoom";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRoom_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmRoom_FormClosed);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_Timer;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lbl_P1Score;
        private System.Windows.Forms.Label lbl_P1Name;
        private System.Windows.Forms.Label lbl_P2Score;
        private System.Windows.Forms.Label lbl_P2Name;
    }
}