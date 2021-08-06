
namespace Simple_GTAV_External_Trainer
{
    partial class Overlay
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
            this.components = new System.ComponentModel.Container();
            this.ProcessTimer = new System.Windows.Forms.Timer(this.components);
            this.MemoryTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.showAddressesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.playerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.godModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.superJumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neverWantedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seatbeltToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noRagdollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undeadOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vehicleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.godModeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.teleportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waypointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newPublicSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProcessTimer
            // 
            this.ProcessTimer.Enabled = true;
            this.ProcessTimer.Tick += new System.EventHandler(this.ProcessTimer_Tick);
            // 
            // MemoryTimer
            // 
            this.MemoryTimer.Enabled = true;
            this.MemoryTimer.Tick += new System.EventHandler(this.MemoryTimer_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.playerToolStripMenuItem,
            this.vehicleToolStripMenuItem,
            this.teleportToolStripMenuItem,
            this.sessionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(838, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.showAddressesToolStripMenuItem,
            this.toolStripMenuItem5});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(46, 20);
            this.toolStripMenuItem2.Text = "Main";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(159, 22);
            this.toolStripMenuItem4.Text = "Re-Init";
            // 
            // showAddressesToolStripMenuItem
            // 
            this.showAddressesToolStripMenuItem.Name = "showAddressesToolStripMenuItem";
            this.showAddressesToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.showAddressesToolStripMenuItem.Text = "Show Addresses";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(159, 22);
            this.toolStripMenuItem5.Text = "Quit";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // playerToolStripMenuItem
            // 
            this.playerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.godModeToolStripMenuItem,
            this.superJumpToolStripMenuItem,
            this.neverWantedToolStripMenuItem,
            this.seatbeltToolStripMenuItem,
            this.noRagdollToolStripMenuItem,
            this.undeadOffToolStripMenuItem});
            this.playerToolStripMenuItem.Name = "playerToolStripMenuItem";
            this.playerToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.playerToolStripMenuItem.Text = "Player";
            // 
            // godModeToolStripMenuItem
            // 
            this.godModeToolStripMenuItem.Name = "godModeToolStripMenuItem";
            this.godModeToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.godModeToolStripMenuItem.Text = "God Mode";
            this.godModeToolStripMenuItem.Click += new System.EventHandler(this.godModeToolStripMenuItem_Click);
            // 
            // superJumpToolStripMenuItem
            // 
            this.superJumpToolStripMenuItem.Name = "superJumpToolStripMenuItem";
            this.superJumpToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.superJumpToolStripMenuItem.Text = "Super Jump";
            this.superJumpToolStripMenuItem.Click += new System.EventHandler(this.superJumpToolStripMenuItem_Click);
            // 
            // neverWantedToolStripMenuItem
            // 
            this.neverWantedToolStripMenuItem.Name = "neverWantedToolStripMenuItem";
            this.neverWantedToolStripMenuItem.ShowShortcutKeys = false;
            this.neverWantedToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.neverWantedToolStripMenuItem.Text = "Never Wanted";
            this.neverWantedToolStripMenuItem.Click += new System.EventHandler(this.neverWantedToolStripMenuItem_Click);
            // 
            // seatbeltToolStripMenuItem
            // 
            this.seatbeltToolStripMenuItem.Name = "seatbeltToolStripMenuItem";
            this.seatbeltToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.seatbeltToolStripMenuItem.Text = "Seatbelt";
            this.seatbeltToolStripMenuItem.Click += new System.EventHandler(this.seatbeltToolStripMenuItem_Click);
            // 
            // noRagdollToolStripMenuItem
            // 
            this.noRagdollToolStripMenuItem.Name = "noRagdollToolStripMenuItem";
            this.noRagdollToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.noRagdollToolStripMenuItem.Text = "No Ragdoll";
            this.noRagdollToolStripMenuItem.Click += new System.EventHandler(this.noRagdollToolStripMenuItem_Click);
            // 
            // undeadOffToolStripMenuItem
            // 
            this.undeadOffToolStripMenuItem.Name = "undeadOffToolStripMenuItem";
            this.undeadOffToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.undeadOffToolStripMenuItem.Text = "Undead Off-Radar";
            this.undeadOffToolStripMenuItem.Click += new System.EventHandler(this.undeadOffToolStripMenuItem_Click);
            // 
            // vehicleToolStripMenuItem
            // 
            this.vehicleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.godModeToolStripMenuItem1});
            this.vehicleToolStripMenuItem.Name = "vehicleToolStripMenuItem";
            this.vehicleToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.vehicleToolStripMenuItem.Text = "Vehicle";
            // 
            // godModeToolStripMenuItem1
            // 
            this.godModeToolStripMenuItem1.Name = "godModeToolStripMenuItem1";
            this.godModeToolStripMenuItem1.Size = new System.Drawing.Size(130, 22);
            this.godModeToolStripMenuItem1.Text = "God Mode";
            this.godModeToolStripMenuItem1.Click += new System.EventHandler(this.godModeToolStripMenuItem1_Click);
            // 
            // teleportToolStripMenuItem
            // 
            this.teleportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.waypointToolStripMenuItem,
            this.objectiveToolStripMenuItem});
            this.teleportToolStripMenuItem.Name = "teleportToolStripMenuItem";
            this.teleportToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.teleportToolStripMenuItem.Text = "Teleport";
            // 
            // waypointToolStripMenuItem
            // 
            this.waypointToolStripMenuItem.Name = "waypointToolStripMenuItem";
            this.waypointToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.waypointToolStripMenuItem.Text = "Waypoint";
            this.waypointToolStripMenuItem.Click += new System.EventHandler(this.waypointToolStripMenuItem_Click);
            // 
            // objectiveToolStripMenuItem
            // 
            this.objectiveToolStripMenuItem.Name = "objectiveToolStripMenuItem";
            this.objectiveToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.objectiveToolStripMenuItem.Text = "Objective";
            this.objectiveToolStripMenuItem.Click += new System.EventHandler(this.objectiveToolStripMenuItem_Click);
            // 
            // sessionToolStripMenuItem
            // 
            this.sessionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPublicSessionToolStripMenuItem});
            this.sessionToolStripMenuItem.Name = "sessionToolStripMenuItem";
            this.sessionToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.sessionToolStripMenuItem.Text = "Session";
            // 
            // newPublicSessionToolStripMenuItem
            // 
            this.newPublicSessionToolStripMenuItem.Name = "newPublicSessionToolStripMenuItem";
            this.newPublicSessionToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.newPublicSessionToolStripMenuItem.Text = "New Public Session";
            this.newPublicSessionToolStripMenuItem.Click += new System.EventHandler(this.newPublicSessionToolStripMenuItem_Click);
            // 
            // Overlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 30);
            this.ControlBox = false;
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Overlay";
            this.ShowInTaskbar = false;
            this.Text = "GTA5MDMNU";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Close);
            this.Load += new System.EventHandler(this.Initialize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer ProcessTimer;
        private System.Windows.Forms.Timer MemoryTimer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem showAddressesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem godModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem neverWantedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem superJumpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noRagdollToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undeadOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seatbeltToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vehicleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem godModeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem teleportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waypointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem objectiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newPublicSessionToolStripMenuItem;
    }
}

