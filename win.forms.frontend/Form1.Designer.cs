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
            this.lblCapturedByWhite = new System.Windows.Forms.Label();
            this.lblCapturedByBlack = new System.Windows.Forms.Label();
            this.stoneControl3 = new win.forms.frontend.StoneControl();
            this.stoneControl2 = new win.forms.frontend.StoneControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblTerritoryOfWhite = new System.Windows.Forms.Label();
            this.lblTerritoryOfBlack = new System.Windows.Forms.Label();
            this.stoneControl1 = new win.forms.frontend.StoneControl();
            this.stoneControl4 = new win.forms.frontend.StoneControl();
            ((System.ComponentModel.ISupportInitialize)(this.goban)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.groupBox2.Controls.Add(this.lblCapturedByWhite);
            this.groupBox2.Controls.Add(this.lblCapturedByBlack);
            this.groupBox2.Controls.Add(this.stoneControl3);
            this.groupBox2.Controls.Add(this.stoneControl2);
            this.groupBox2.Location = new System.Drawing.Point(376, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(138, 100);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Captured";
            // 
            // lblCapturedByWhite
            // 
            this.lblCapturedByWhite.AutoSize = true;
            this.lblCapturedByWhite.Location = new System.Drawing.Point(56, 55);
            this.lblCapturedByWhite.Name = "lblCapturedByWhite";
            this.lblCapturedByWhite.Size = new System.Drawing.Size(35, 13);
            this.lblCapturedByWhite.TabIndex = 3;
            this.lblCapturedByWhite.Text = "label1";
            // 
            // lblCapturedByBlack
            // 
            this.lblCapturedByBlack.AutoSize = true;
            this.lblCapturedByBlack.Location = new System.Drawing.Point(56, 19);
            this.lblCapturedByBlack.Name = "lblCapturedByBlack";
            this.lblCapturedByBlack.Size = new System.Drawing.Size(35, 13);
            this.lblCapturedByBlack.TabIndex = 3;
            this.lblCapturedByBlack.Text = "label1";
            // 
            // stoneControl3
            // 
            this.stoneControl3.IsBlack = false;
            this.stoneControl3.Location = new System.Drawing.Point(6, 55);
            this.stoneControl3.Name = "stoneControl3";
            this.stoneControl3.Size = new System.Drawing.Size(30, 30);
            this.stoneControl3.TabIndex = 2;
            // 
            // stoneControl2
            // 
            this.stoneControl2.IsBlack = true;
            this.stoneControl2.Location = new System.Drawing.Point(6, 19);
            this.stoneControl2.Name = "stoneControl2";
            this.stoneControl2.Size = new System.Drawing.Size(30, 30);
            this.stoneControl2.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblTerritoryOfWhite);
            this.groupBox3.Controls.Add(this.lblTerritoryOfBlack);
            this.groupBox3.Controls.Add(this.stoneControl1);
            this.groupBox3.Controls.Add(this.stoneControl4);
            this.groupBox3.Location = new System.Drawing.Point(376, 224);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(138, 100);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Territory";
            // 
            // lblTerritoryOfWhite
            // 
            this.lblTerritoryOfWhite.AutoSize = true;
            this.lblTerritoryOfWhite.Location = new System.Drawing.Point(56, 55);
            this.lblTerritoryOfWhite.Name = "lblTerritoryOfWhite";
            this.lblTerritoryOfWhite.Size = new System.Drawing.Size(35, 13);
            this.lblTerritoryOfWhite.TabIndex = 3;
            this.lblTerritoryOfWhite.Text = "label1";
            // 
            // lblTerritoryOfBlack
            // 
            this.lblTerritoryOfBlack.AutoSize = true;
            this.lblTerritoryOfBlack.Location = new System.Drawing.Point(56, 19);
            this.lblTerritoryOfBlack.Name = "lblTerritoryOfBlack";
            this.lblTerritoryOfBlack.Size = new System.Drawing.Size(35, 13);
            this.lblTerritoryOfBlack.TabIndex = 3;
            this.lblTerritoryOfBlack.Text = "label1";
            // 
            // stoneControl1
            // 
            this.stoneControl1.IsBlack = false;
            this.stoneControl1.Location = new System.Drawing.Point(6, 55);
            this.stoneControl1.Name = "stoneControl1";
            this.stoneControl1.Size = new System.Drawing.Size(30, 30);
            this.stoneControl1.TabIndex = 2;
            // 
            // stoneControl4
            // 
            this.stoneControl4.IsBlack = true;
            this.stoneControl4.Location = new System.Drawing.Point(6, 19);
            this.stoneControl4.Name = "stoneControl4";
            this.stoneControl4.Size = new System.Drawing.Size(30, 30);
            this.stoneControl4.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 380);
            this.Controls.Add(this.groupBox3);
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
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox goban;
        private StoneControl currentPlayer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblCapturedByWhite;
        private System.Windows.Forms.Label lblCapturedByBlack;
        private StoneControl stoneControl3;
        private StoneControl stoneControl2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblTerritoryOfWhite;
        private System.Windows.Forms.Label lblTerritoryOfBlack;
        private StoneControl stoneControl1;
        private StoneControl stoneControl4;
    }
}

