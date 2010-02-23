namespace win.forms.frontend
{
    partial class Form1
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
            this.goban = new System.Windows.Forms.PictureBox();
            this.currentPlayer = new win.forms.frontend.StoneControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.stoneControl2 = new win.forms.frontend.StoneControl();
            this.stoneControl3 = new win.forms.frontend.StoneControl();
            this.lblBlack = new System.Windows.Forms.Label();
            this.lblWhite = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.goban)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // goban
            // 
            this.goban.Location = new System.Drawing.Point(12, 12);
            this.goban.Name = "goban";
            this.goban.Size = new System.Drawing.Size(358, 342);
            this.goban.TabIndex = 0;
            this.goban.TabStop = false;
            this.goban.Click += new System.EventHandler(this.goban_Click);
            this.goban.MouseClick += new System.Windows.Forms.MouseEventHandler(this.goban_MouseClick);
            // 
            // currentPlayer
            // 
            this.currentPlayer.IsBlack = false;
            this.currentPlayer.Location = new System.Drawing.Point(6, 19);
            this.currentPlayer.Name = "currentPlayer";
            this.currentPlayer.Size = new System.Drawing.Size(69, 75);
            this.currentPlayer.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.currentPlayer);
            this.groupBox1.Location = new System.Drawing.Point(376, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Next";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblWhite);
            this.groupBox2.Controls.Add(this.lblBlack);
            this.groupBox2.Controls.Add(this.stoneControl3);
            this.groupBox2.Controls.Add(this.stoneControl2);
            this.groupBox2.Location = new System.Drawing.Point(376, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(138, 100);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Next";
            // 
            // stoneControl2
            // 
            this.stoneControl2.IsBlack = true;
            this.stoneControl2.Location = new System.Drawing.Point(6, 19);
            this.stoneControl2.Name = "stoneControl2";
            this.stoneControl2.Size = new System.Drawing.Size(30, 30);
            this.stoneControl2.TabIndex = 2;
            // 
            // stoneControl3
            // 
            this.stoneControl3.IsBlack = false;
            this.stoneControl3.Location = new System.Drawing.Point(6, 55);
            this.stoneControl3.Name = "stoneControl3";
            this.stoneControl3.Size = new System.Drawing.Size(30, 30);
            this.stoneControl3.TabIndex = 2;
            // 
            // lblBlack
            // 
            this.lblBlack.AutoSize = true;
            this.lblBlack.Location = new System.Drawing.Point(56, 19);
            this.lblBlack.Name = "lblBlack";
            this.lblBlack.Size = new System.Drawing.Size(35, 13);
            this.lblBlack.TabIndex = 3;
            this.lblBlack.Text = "label1";
            // 
            // lblWhite
            // 
            this.lblWhite.AutoSize = true;
            this.lblWhite.Location = new System.Drawing.Point(56, 55);
            this.lblWhite.Name = "lblWhite";
            this.lblWhite.Size = new System.Drawing.Size(35, 13);
            this.lblWhite.TabIndex = 3;
            this.lblWhite.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 380);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.goban);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.goban)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox goban;
        private StoneControl currentPlayer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblWhite;
        private System.Windows.Forms.Label lblBlack;
        private StoneControl stoneControl3;
        private StoneControl stoneControl2;
    }
}

