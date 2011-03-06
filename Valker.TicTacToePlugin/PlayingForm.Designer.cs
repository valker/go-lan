namespace Valker.TicTacToePlugin
{
    partial class PlayingForm
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
            this.goban1 = new MyGoban.Goban();
            this.SuspendLayout();
            // 
            // goban1
            // 
            this.goban1.Location = new System.Drawing.Point(12, 12);
            this.goban1.Name = "goban1";
            this.goban1.Size = new System.Drawing.Size(447, 394);
            this.goban1.TabIndex = 0;
            // 
            // PlayingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 418);
            this.Controls.Add(this.goban1);
            this.Name = "PlayingForm";
            this.Text = "PlayingForm";
            this.ResumeLayout(false);

        }

        #endregion

        private MyGoban.Goban goban1;
    }
}