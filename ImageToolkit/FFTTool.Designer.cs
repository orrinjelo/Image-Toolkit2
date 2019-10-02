namespace ImageToolkit
{
    partial class FFTTool
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnReal = new System.Windows.Forms.RadioButton();
            this.rbImag = new System.Windows.Forms.RadioButton();
            this.rbMagnitude = new System.Windows.Forms.RadioButton();
            this.rbPhase = new System.Windows.Forms.RadioButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbLogMagnitude = new System.Windows.Forms.RadioButton();
            this.rbLogImag = new System.Windows.Forms.RadioButton();
            this.rbLogReal = new System.Windows.Forms.RadioButton();
            this.rbIFFT = new System.Windows.Forms.RadioButton();
            this.rbOriginal = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ImageToolkit.Properties.Resources.grayscaledomo;
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnReal
            // 
            this.btnReal.AutoSize = true;
            this.btnReal.Location = new System.Drawing.Point(3, 32);
            this.btnReal.Name = "btnReal";
            this.btnReal.Size = new System.Drawing.Size(47, 17);
            this.btnReal.TabIndex = 1;
            this.btnReal.Text = "Real";
            this.btnReal.UseVisualStyleBackColor = true;
            this.btnReal.CheckedChanged += new System.EventHandler(this.btnReal_CheckedChanged);
            // 
            // rbImag
            // 
            this.rbImag.AutoSize = true;
            this.rbImag.Location = new System.Drawing.Point(3, 56);
            this.rbImag.Name = "rbImag";
            this.rbImag.Size = new System.Drawing.Size(70, 17);
            this.rbImag.TabIndex = 2;
            this.rbImag.Text = "Imaginary";
            this.rbImag.UseVisualStyleBackColor = true;
            this.rbImag.CheckedChanged += new System.EventHandler(this.rbImag_CheckedChanged);
            // 
            // rbMagnitude
            // 
            this.rbMagnitude.AutoSize = true;
            this.rbMagnitude.Location = new System.Drawing.Point(3, 80);
            this.rbMagnitude.Name = "rbMagnitude";
            this.rbMagnitude.Size = new System.Drawing.Size(91, 17);
            this.rbMagnitude.TabIndex = 3;
            this.rbMagnitude.Text = "Magnitude (Z)";
            this.rbMagnitude.UseVisualStyleBackColor = true;
            this.rbMagnitude.CheckedChanged += new System.EventHandler(this.rbMagnitude_CheckedChanged);
            // 
            // rbPhase
            // 
            this.rbPhase.AutoSize = true;
            this.rbPhase.Location = new System.Drawing.Point(3, 104);
            this.rbPhase.Name = "rbPhase";
            this.rbPhase.Size = new System.Drawing.Size(79, 17);
            this.rbPhase.TabIndex = 4;
            this.rbPhase.Text = "Phase (Phi)";
            this.rbPhase.UseVisualStyleBackColor = true;
            this.rbPhase.CheckedChanged += new System.EventHandler(this.rbPhase_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "DFT",
            "AForge FFT",
            "JeloFFT"});
            this.comboBox1.Location = new System.Drawing.Point(275, 32);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(230, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.rbLogMagnitude);
            this.panel1.Controls.Add(this.rbLogImag);
            this.panel1.Controls.Add(this.rbLogReal);
            this.panel1.Controls.Add(this.rbIFFT);
            this.panel1.Controls.Add(this.rbOriginal);
            this.panel1.Controls.Add(this.btnReal);
            this.panel1.Controls.Add(this.rbImag);
            this.panel1.Controls.Add(this.rbPhase);
            this.panel1.Controls.Add(this.rbMagnitude);
            this.panel1.Location = new System.Drawing.Point(279, 98);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(224, 161);
            this.panel1.TabIndex = 6;
            // 
            // rbLogMagnitude
            // 
            this.rbLogMagnitude.AutoSize = true;
            this.rbLogMagnitude.Enabled = false;
            this.rbLogMagnitude.Location = new System.Drawing.Point(119, 80);
            this.rbLogMagnitude.Name = "rbLogMagnitude";
            this.rbLogMagnitude.Size = new System.Drawing.Size(96, 17);
            this.rbLogMagnitude.TabIndex = 9;
            this.rbLogMagnitude.TabStop = true;
            this.rbLogMagnitude.Text = "Log Magnitude";
            this.rbLogMagnitude.UseVisualStyleBackColor = true;
            this.rbLogMagnitude.CheckedChanged += new System.EventHandler(this.rbLogMagnitude_CheckedChanged);
            // 
            // rbLogImag
            // 
            this.rbLogImag.AutoSize = true;
            this.rbLogImag.Enabled = false;
            this.rbLogImag.Location = new System.Drawing.Point(119, 56);
            this.rbLogImag.Name = "rbLogImag";
            this.rbLogImag.Size = new System.Drawing.Size(91, 17);
            this.rbLogImag.TabIndex = 8;
            this.rbLogImag.TabStop = true;
            this.rbLogImag.Text = "Log Imaginary";
            this.rbLogImag.UseVisualStyleBackColor = true;
            this.rbLogImag.CheckedChanged += new System.EventHandler(this.rbLogImag_CheckedChanged);
            // 
            // rbLogReal
            // 
            this.rbLogReal.AutoSize = true;
            this.rbLogReal.Enabled = false;
            this.rbLogReal.Location = new System.Drawing.Point(119, 32);
            this.rbLogReal.Name = "rbLogReal";
            this.rbLogReal.Size = new System.Drawing.Size(68, 17);
            this.rbLogReal.TabIndex = 7;
            this.rbLogReal.TabStop = true;
            this.rbLogReal.Text = "Log Real";
            this.rbLogReal.UseVisualStyleBackColor = true;
            this.rbLogReal.CheckedChanged += new System.EventHandler(this.rbLogReal_CheckedChanged);
            // 
            // rbIFFT
            // 
            this.rbIFFT.AutoSize = true;
            this.rbIFFT.Location = new System.Drawing.Point(3, 127);
            this.rbIFFT.Name = "rbIFFT";
            this.rbIFFT.Size = new System.Drawing.Size(82, 17);
            this.rbIFFT.TabIndex = 6;
            this.rbIFFT.TabStop = true;
            this.rbIFFT.Text = "Inverse FFT";
            this.rbIFFT.UseVisualStyleBackColor = true;
            this.rbIFFT.CheckedChanged += new System.EventHandler(this.rbIFFT_CheckedChanged);
            // 
            // rbOriginal
            // 
            this.rbOriginal.AutoSize = true;
            this.rbOriginal.Checked = true;
            this.rbOriginal.Location = new System.Drawing.Point(3, 9);
            this.rbOriginal.Name = "rbOriginal";
            this.rbOriginal.Size = new System.Drawing.Size(60, 17);
            this.rbOriginal.TabIndex = 5;
            this.rbOriginal.TabStop = true;
            this.rbOriginal.Text = "Original";
            this.rbOriginal.UseVisualStyleBackColor = true;
            this.rbOriginal.CheckedChanged += new System.EventHandler(this.rbOriginal_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(276, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Algorithm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(279, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Output Image:";
            // 
            // FFTTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 281);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FFTTool";
            this.Text = "FFTTool";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton btnReal;
        private System.Windows.Forms.RadioButton rbImag;
        private System.Windows.Forms.RadioButton rbMagnitude;
        private System.Windows.Forms.RadioButton rbPhase;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbOriginal;
        private System.Windows.Forms.RadioButton rbIFFT;
        private System.Windows.Forms.RadioButton rbLogMagnitude;
        private System.Windows.Forms.RadioButton rbLogImag;
        private System.Windows.Forms.RadioButton rbLogReal;
    }
}