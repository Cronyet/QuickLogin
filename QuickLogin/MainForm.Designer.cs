﻿namespace QuickLogin
{
    partial class MainForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("pwd tree");
            this.groupbox_pwds = new System.Windows.Forms.GroupBox();
            this.pwdtree = new System.Windows.Forms.TreeView();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.label_quake = new System.Windows.Forms.Label();
            this.textbox_show_hotkey = new System.Windows.Forms.TextBox();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.groupbox_pwds.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupbox_pwds
            // 
            this.groupbox_pwds.Controls.Add(this.pwdtree);
            this.groupbox_pwds.Location = new System.Drawing.Point(12, 12);
            this.groupbox_pwds.Name = "groupbox_pwds";
            this.groupbox_pwds.Size = new System.Drawing.Size(328, 410);
            this.groupbox_pwds.TabIndex = 0;
            this.groupbox_pwds.TabStop = false;
            this.groupbox_pwds.Text = "Saved pwd";
            // 
            // pwdtree
            // 
            this.pwdtree.Location = new System.Drawing.Point(6, 26);
            this.pwdtree.Name = "pwdtree";
            treeNode1.Name = "node_root";
            treeNode1.Tag = "root_node";
            treeNode1.Text = "pwd tree";
            this.pwdtree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.pwdtree.Size = new System.Drawing.Size(316, 378);
            this.pwdtree.TabIndex = 0;
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(12, 471);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(190, 40);
            this.btn_add.TabIndex = 2;
            this.btn_add.Text = "Add New pwd";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.Btn_add_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(270, 471);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(70, 40);
            this.btn_exit.TabIndex = 4;
            this.btn_exit.Text = "Exit";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.Btn_exit_Click);
            // 
            // label_quake
            // 
            this.label_quake.AutoSize = true;
            this.label_quake.Location = new System.Drawing.Point(12, 435);
            this.label_quake.Name = "label_quake";
            this.label_quake.Size = new System.Drawing.Size(132, 20);
            this.label_quake.TabIndex = 10;
            this.label_quake.Text = "Hotkey to show: ";
            // 
            // textbox_show_hotkey
            // 
            this.textbox_show_hotkey.Location = new System.Drawing.Point(150, 432);
            this.textbox_show_hotkey.Name = "textbox_show_hotkey";
            this.textbox_show_hotkey.ReadOnly = true;
            this.textbox_show_hotkey.Size = new System.Drawing.Size(190, 27);
            this.textbox_show_hotkey.TabIndex = 1;
            this.textbox_show_hotkey.Text = "Ctrl+Shift+I";
            // 
            // btn_refresh
            // 
            this.btn_refresh.Font = new System.Drawing.Font("Segoe MDL2 Assets", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_refresh.Location = new System.Drawing.Point(208, 471);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(56, 40);
            this.btn_refresh.TabIndex = 3;
            this.btn_refresh.Text = "";
            this.btn_refresh.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_exit;
            this.ClientSize = new System.Drawing.Size(352, 523);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.textbox_show_hotkey);
            this.Controls.Add(this.label_quake);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.groupbox_pwds);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(370, 570);
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.Text = "QuickLogin";
            this.groupbox_pwds.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupbox_pwds;
        private Button btn_add;
        private Button btn_exit;
        private Label label_quake;
        private TextBox textbox_show_hotkey;
        private TreeView pwdtree;
        private Button btn_refresh;
    }
}