namespace ImageToolkit.Operations
{
    partial class frmFFTFilter
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
            this.picFFT = new System.Windows.Forms.PictureBox();
            this.udLow = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.udHigh = new System.Windows.Forms.NumericUpDown();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picFFT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udHigh)).BeginInit();
            this.SuspendLayout();
            // 
            // picFFT
            // 
            this.picFFT.Location = new System.Drawing.Point(13, 13);
            this.picFFT.Name = "picFFT";
            this.picFFT.Size = new System.Drawing.Size(300, 300);
            this.picFFT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFFT.TabIndex = 0;
            this.picFFT.TabStop = false;
            this.picFFT.Paint += new System.Windows.Forms.PaintEventHandler(this.drawCircles);
            // 
            // udLow
            // 
            this.udLow.Location = new System.Drawing.Point(323, 46);
            this.udLow.Name = "udLow";
            this.udLow.Size = new System.Drawing.Size(120, 20);
            this.udLow.TabIndex = 1;
            this.udLow.ValueChanged += new System.EventHandler(this.updateCanvas);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(320, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Lower Frequency:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(323, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Upper Frequency:";
            // 
            // udHigh
            // 
            this.udHigh.Location = new System.Drawing.Point(323, 103);
            this.udHigh.Name = "udHigh";
            this.udHigh.Size = new System.Drawing.Size(120, 20);
            this.udHigh.TabIndex = 4;
            this.udHigh.ValueChanged += new System.EventHandler(this.updateCanvas);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(326, 169);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(173, 23);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(326, 199);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(173, 115);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmFFTFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 326);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.udHigh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.udLow);
            this.Controls.Add(this.picFFT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmFFTFilter";
            this.Text = "FFT Filter";
            ((System.ComponentModel.ISupportInitialize)(this.picFFT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udHigh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picFFT;
        private System.Windows.Forms.NumericUpDown udLow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown udHigh;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnOK;
    }
}