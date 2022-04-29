
namespace Battleship
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
            this.shipGrid = new System.Windows.Forms.Panel();
            this.atkGrid = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnPlaceShips = new System.Windows.Forms.Button();
            this.lstShips = new System.Windows.Forms.ComboBox();
            this.lblShipGrid = new System.Windows.Forms.Label();
            this.lblAtkGrid = new System.Windows.Forms.Label();
            this.btnTurn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.english = new System.Windows.Forms.ToolStripMenuItem();
            this.italian = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // shipGrid
            // 
            this.shipGrid.Location = new System.Drawing.Point(12, 48);
            this.shipGrid.Name = "shipGrid";
            this.shipGrid.Size = new System.Drawing.Size(320, 320);
            this.shipGrid.TabIndex = 0;
            // 
            // atkGrid
            // 
            this.atkGrid.Enabled = false;
            this.atkGrid.Location = new System.Drawing.Point(344, 48);
            this.atkGrid.Name = "atkGrid";
            this.atkGrid.Size = new System.Drawing.Size(320, 320);
            this.atkGrid.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(746, 346);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnPlaceShips
            // 
            this.btnPlaceShips.Location = new System.Drawing.Point(670, 48);
            this.btnPlaceShips.Name = "btnPlaceShips";
            this.btnPlaceShips.Size = new System.Drawing.Size(75, 23);
            this.btnPlaceShips.TabIndex = 4;
            this.btnPlaceShips.Text = "Place Ships";
            this.btnPlaceShips.UseVisualStyleBackColor = true;
            this.btnPlaceShips.Click += new System.EventHandler(this.btnPlaceShips_Click);
            // 
            // lstShips
            // 
            this.lstShips.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstShips.Enabled = false;
            this.lstShips.FormattingEnabled = true;
            this.lstShips.Location = new System.Drawing.Point(671, 78);
            this.lstShips.Name = "lstShips";
            this.lstShips.Size = new System.Drawing.Size(90, 21);
            this.lstShips.TabIndex = 5;
            this.lstShips.SelectionChangeCommitted += new System.EventHandler(this.lstShips_SelectionChangeCommitted);
            // 
            // lblShipGrid
            // 
            this.lblShipGrid.AutoSize = true;
            this.lblShipGrid.Location = new System.Drawing.Point(12, 29);
            this.lblShipGrid.Name = "lblShipGrid";
            this.lblShipGrid.Size = new System.Drawing.Size(52, 13);
            this.lblShipGrid.TabIndex = 6;
            this.lblShipGrid.Text = "Your grid:";
            // 
            // lblAtkGrid
            // 
            this.lblAtkGrid.AutoSize = true;
            this.lblAtkGrid.Location = new System.Drawing.Point(341, 29);
            this.lblAtkGrid.Name = "lblAtkGrid";
            this.lblAtkGrid.Size = new System.Drawing.Size(61, 13);
            this.lblAtkGrid.TabIndex = 7;
            this.lblAtkGrid.Text = "Attack grid:";
            // 
            // btnTurn
            // 
            this.btnTurn.Enabled = false;
            this.btnTurn.Location = new System.Drawing.Point(767, 78);
            this.btnTurn.Name = "btnTurn";
            this.btnTurn.Size = new System.Drawing.Size(54, 23);
            this.btnTurn.TabIndex = 8;
            this.btnTurn.Text = "Turn";
            this.btnTurn.UseVisualStyleBackColor = true;
            this.btnTurn.Click += new System.EventHandler(this.btnTurn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(833, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languageToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.english,
            this.italian});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // english
            // 
            this.english.Name = "english";
            this.english.Size = new System.Drawing.Size(112, 22);
            this.english.Text = "English";
            this.english.Click += new System.EventHandler(this.changeLanguageToolStripMenuItem_Click);
            // 
            // italian
            // 
            this.italian.Name = "italian";
            this.italian.Size = new System.Drawing.Size(112, 22);
            this.italian.Text = "Italian";
            this.italian.Click += new System.EventHandler(this.changeLanguageToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 383);
            this.Controls.Add(this.btnTurn);
            this.Controls.Add(this.lblAtkGrid);
            this.Controls.Add(this.lblShipGrid);
            this.Controls.Add(this.lstShips);
            this.Controls.Add(this.btnPlaceShips);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.atkGrid);
            this.Controls.Add(this.shipGrid);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Battleship";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel shipGrid;
        private System.Windows.Forms.Panel atkGrid;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnPlaceShips;
        private System.Windows.Forms.ComboBox lstShips;
        private System.Windows.Forms.Label lblShipGrid;
        private System.Windows.Forms.Label lblAtkGrid;
        private System.Windows.Forms.Button btnTurn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem english;
        private System.Windows.Forms.ToolStripMenuItem italian;
    }
}

