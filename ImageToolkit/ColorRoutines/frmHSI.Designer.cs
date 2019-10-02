namespace ImageToolkit.ColorRoutines
{
    partial class frmHSI
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
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.picHue = new System.Windows.Forms.PictureBox();
            this.picSaturation = new System.Windows.Forms.PictureBox();
            this.picIntensity = new System.Windows.Forms.PictureBox();
            this.tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSaturation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIntensity)).BeginInit();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Controls.Add(this.tabPage1);
            this.tabs.Controls.Add(this.tabPage2);
            this.tabs.Controls.Add(this.tabPage3);
            this.tabs.Location = new System.Drawing.Point(12, 12);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(847, 435);
            this.tabs.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(839, 409);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Hue";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(839, 409);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Saturation";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(839, 409);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Intensity";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.picHue);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(827, 400);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.picSaturation);
            this.panel2.Location = new System.Drawing.Point(7, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(826, 396);
            this.panel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.picIntensity);
            this.panel3.Location = new System.Drawing.Point(6, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(827, 397);
            this.panel3.TabIndex = 0;
            // 
            // picHue
            // 
            this.picHue.Location = new System.Drawing.Point(4, 4);
            this.picHue.Name = "picHue";
            this.picHue.Size = new System.Drawing.Size(100, 50);
            this.picHue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picHue.TabIndex = 0;
            this.picHue.TabStop = false;
            // 
            // picSaturation
            // 
            this.picSaturation.Location = new System.Drawing.Point(4, 4);
            this.picSaturation.Name = "picSaturation";
            this.picSaturation.Size = new System.Drawing.Size(100, 50);
            this.picSaturation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picSaturation.TabIndex = 0;
            this.picSaturation.TabStop = false;
            // 
            // picIntensity
            // 
            this.picIntensity.Location = new System.Drawing.Point(4, 4);
            this.picIntensity.Name = "picIntensity";
            this.picIntensity.Size = new System.Drawing.Size(100, 50);
            this.picIntensity.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picIntensity.TabIndex = 0;
            this.picIntensity.TabStop = false;
            // 
            // frmHSI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 459);
            this.Controls.Add(this.tabs);
            this.Name = "frmHSI";
            this.Text = "HSI View";
            this.tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSaturation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIntensity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picHue;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picSaturation;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox picIntensity;
    }
}