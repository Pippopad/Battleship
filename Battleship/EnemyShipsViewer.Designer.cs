
namespace Battleship
{
    partial class EnemyShipsViewer
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
            this.shipGrid = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // shipGrid
            // 
            this.shipGrid.Location = new System.Drawing.Point(12, 12);
            this.shipGrid.Name = "shipGrid";
            this.shipGrid.Size = new System.Drawing.Size(320, 320);
            this.shipGrid.TabIndex = 1;
            // 
            // EnemyShipsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 352);
            this.Controls.Add(this.shipGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EnemyShipsViewer";
            this.ShowIcon = false;
            this.Text = "Enemy Ships Viewer";
            this.Load += new System.EventHandler(this.EnemyShipsViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel shipGrid;
    }
}