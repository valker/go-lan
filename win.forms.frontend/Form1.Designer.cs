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
            ((System.ComponentModel.ISupportInitialize)(this.goban)).BeginInit();
            this.SuspendLayout();
            // 
            // goban
            // 
            this.goban.Location = new System.Drawing.Point(12, 12);
            this.goban.Name = "goban";
            this.goban.Size = new System.Drawing.Size(358, 342);
            this.goban.TabIndex = 0;
            this.goban.TabStop = false;
            this.goban.MouseClick += new System.Windows.Forms.MouseEventHandler(this.goban_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 380);
            this.Controls.Add(this.goban);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.goban)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox goban;
    }
}

