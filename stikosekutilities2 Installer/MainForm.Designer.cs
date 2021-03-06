namespace stikosekutilities2_Installer
{
    partial class MainForm
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
            this.mainFormSkin = new FlatUI.FormSkin();
            this.uninstallButton = new System.Windows.Forms.PictureBox();
            this.versionSelectionBox = new FlatUI.FlatComboBox();
            this.currentVersionLabel = new FlatUI.FlatLabel();
            this.latestVersionLabel = new FlatUI.FlatLabel();
            this.flatMini1 = new FlatUI.FlatMini();
            this.flatClose1 = new FlatUI.FlatClose();
            this.mainButton = new FlatUI.FlatButton();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.mainFormSkin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uninstallButton)).BeginInit();
            this.SuspendLayout();
            // 
            // mainFormSkin
            // 
            this.mainFormSkin.BackColor = System.Drawing.Color.White;
            this.mainFormSkin.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.mainFormSkin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(58)))), ((int)(((byte)(60)))));
            this.mainFormSkin.Controls.Add(this.uninstallButton);
            this.mainFormSkin.Controls.Add(this.versionSelectionBox);
            this.mainFormSkin.Controls.Add(this.currentVersionLabel);
            this.mainFormSkin.Controls.Add(this.latestVersionLabel);
            this.mainFormSkin.Controls.Add(this.flatMini1);
            this.mainFormSkin.Controls.Add(this.flatClose1);
            this.mainFormSkin.Controls.Add(this.mainButton);
            this.mainFormSkin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainFormSkin.FlatColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.mainFormSkin.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.mainFormSkin.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.mainFormSkin.HeaderMaximize = false;
            this.mainFormSkin.Location = new System.Drawing.Point(0, 0);
            this.mainFormSkin.Name = "mainFormSkin";
            this.mainFormSkin.Size = new System.Drawing.Size(535, 159);
            this.mainFormSkin.TabIndex = 0;
            this.mainFormSkin.Text = "stikosekutilities2 - Installer";
            // 
            // uninstallButton
            // 
            this.uninstallButton.BackColor = System.Drawing.Color.Transparent;
            this.uninstallButton.Image = global::stikosekutilities2_Installer.Properties.Resources.Red_Cross;
            this.uninstallButton.Location = new System.Drawing.Point(472, 95);
            this.uninstallButton.Name = "uninstallButton";
            this.uninstallButton.Size = new System.Drawing.Size(51, 41);
            this.uninstallButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.uninstallButton.TabIndex = 6;
            this.uninstallButton.TabStop = false;
            this.uninstallButton.Click += new System.EventHandler(this.UninstallButton_Click);
            // 
            // versionSelectionBox
            // 
            this.versionSelectionBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.versionSelectionBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.versionSelectionBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.versionSelectionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.versionSelectionBox.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.versionSelectionBox.ForeColor = System.Drawing.Color.White;
            this.versionSelectionBox.FormattingEnabled = true;
            this.versionSelectionBox.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.versionSelectionBox.ItemHeight = 18;
            this.versionSelectionBox.Location = new System.Drawing.Point(298, 61);
            this.versionSelectionBox.Name = "versionSelectionBox";
            this.versionSelectionBox.Size = new System.Drawing.Size(225, 24);
            this.versionSelectionBox.TabIndex = 5;
            this.versionSelectionBox.SelectedIndexChanged += new System.EventHandler(this.VersionSelectionBox_SelectedIndexChanged);
            // 
            // currentVersionLabel
            // 
            this.currentVersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.currentVersionLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.currentVersionLabel.ForeColor = System.Drawing.Color.White;
            this.currentVersionLabel.Location = new System.Drawing.Point(12, 104);
            this.currentVersionLabel.Name = "currentVersionLabel";
            this.currentVersionLabel.Size = new System.Drawing.Size(242, 23);
            this.currentVersionLabel.TabIndex = 4;
            this.currentVersionLabel.Text = "Current Version:   {version}";
            // 
            // latestVersionLabel
            // 
            this.latestVersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.latestVersionLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.latestVersionLabel.ForeColor = System.Drawing.Color.White;
            this.latestVersionLabel.Location = new System.Drawing.Point(12, 62);
            this.latestVersionLabel.Name = "latestVersionLabel";
            this.latestVersionLabel.Size = new System.Drawing.Size(242, 23);
            this.latestVersionLabel.TabIndex = 3;
            this.latestVersionLabel.Text = "Latest Version:      {version}";
            // 
            // flatMini1
            // 
            this.flatMini1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flatMini1.BackColor = System.Drawing.Color.White;
            this.flatMini1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.flatMini1.Font = new System.Drawing.Font("Marlett", 12F);
            this.flatMini1.Location = new System.Drawing.Point(481, 13);
            this.flatMini1.Name = "flatMini1";
            this.flatMini1.Size = new System.Drawing.Size(18, 18);
            this.flatMini1.TabIndex = 2;
            this.flatMini1.Text = "flatMini1";
            this.flatMini1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            // 
            // flatClose1
            // 
            this.flatClose1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flatClose1.BackColor = System.Drawing.Color.White;
            this.flatClose1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.flatClose1.Font = new System.Drawing.Font("Marlett", 10F);
            this.flatClose1.Location = new System.Drawing.Point(505, 13);
            this.flatClose1.Name = "flatClose1";
            this.flatClose1.Size = new System.Drawing.Size(18, 18);
            this.flatClose1.TabIndex = 1;
            this.flatClose1.Text = "flatClose1";
            this.flatClose1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            // 
            // mainButton
            // 
            this.mainButton.BackColor = System.Drawing.Color.Transparent;
            this.mainButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.mainButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mainButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.mainButton.Location = new System.Drawing.Point(298, 95);
            this.mainButton.Name = "mainButton";
            this.mainButton.Rounded = true;
            this.mainButton.Size = new System.Drawing.Size(168, 41);
            this.mainButton.TabIndex = 0;
            this.mainButton.Text = "Install";
            this.mainButton.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.mainButton.UseCustomColor = false;
            this.mainButton.Click += new System.EventHandler(this.MainButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 159);
            this.Controls.Add(this.mainFormSkin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.mainFormSkin.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uninstallButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FlatUI.FormSkin mainFormSkin;
        private FlatUI.FlatButton mainButton;
        private FlatUI.FlatMini flatMini1;
        private FlatUI.FlatClose flatClose1;
        private FlatUI.FlatLabel latestVersionLabel;
        private FlatUI.FlatLabel currentVersionLabel;
        private FlatUI.FlatComboBox versionSelectionBox;
        private System.Windows.Forms.PictureBox uninstallButton;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}