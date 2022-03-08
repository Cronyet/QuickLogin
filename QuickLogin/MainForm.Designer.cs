namespace QuickLogin
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
            this.groupbox_pwds = new System.Windows.Forms.GroupBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.label_quake = new System.Windows.Forms.Label();
            this.textbox_show_hotkey = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // groupbox_pwds
            // 
            this.groupbox_pwds.Location = new System.Drawing.Point(12, 12);
            this.groupbox_pwds.Name = "groupbox_pwds";
            this.groupbox_pwds.Size = new System.Drawing.Size(328, 410);
            this.groupbox_pwds.TabIndex = 0;
            this.groupbox_pwds.TabStop = false;
            this.groupbox_pwds.Text = "Saved pwd";
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(12, 471);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(190, 40);
            this.btn_add.TabIndex = 1;
            this.btn_add.Text = "Add New pwd";
            this.btn_add.UseVisualStyleBackColor = true;
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(270, 471);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(70, 40);
            this.btn_exit.TabIndex = 2;
            this.btn_exit.Text = "Exit";
            this.btn_exit.UseVisualStyleBackColor = true;
            // 
            // label_quake
            // 
            this.label_quake.AutoSize = true;
            this.label_quake.Location = new System.Drawing.Point(12, 435);
            this.label_quake.Name = "label_quake";
            this.label_quake.Size = new System.Drawing.Size(132, 20);
            this.label_quake.TabIndex = 3;
            this.label_quake.Text = "Hotkey to show: ";
            // 
            // textbox_show_hotkey
            // 
            this.textbox_show_hotkey.Location = new System.Drawing.Point(150, 432);
            this.textbox_show_hotkey.Name = "textbox_show_hotkey";
            this.textbox_show_hotkey.Size = new System.Drawing.Size(190, 27);
            this.textbox_show_hotkey.TabIndex = 4;
            this.textbox_show_hotkey.Text = "Ctrl+Shift+I";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_exit;
            this.ClientSize = new System.Drawing.Size(352, 523);
            this.Controls.Add(this.textbox_show_hotkey);
            this.Controls.Add(this.label_quake);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.groupbox_pwds);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(370, 570);
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.Text = "QuickLogin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupbox_pwds;
        private Button btn_add;
        private Button btn_exit;
        private Label label_quake;
        private TextBox textbox_show_hotkey;
    }
}