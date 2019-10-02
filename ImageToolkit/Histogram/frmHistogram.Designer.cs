namespace ImageToolkit.Histogram
{
    partial class frmHistogram
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
            this.pnlHist = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rbRed = new System.Windows.Forms.RadioButton();
            this.rbIntensity = new System.Windows.Forms.RadioButton();
            this.rbGreen = new System.Windows.Forms.RadioButton();
            this.rbBlue = new System.Windows.Forms.RadioButton();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnAuto = new System.Windows.Forms.Button();
            this.picThumbnail = new System.Windows.Forms.PictureBox();
            this.tbAdjust = new System.Windows.Forms.TrackBar();
            this.cbCPF = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picThumbnail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAdjust)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHist
            // 
            this.pnlHist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHist.BackColor = System.Drawing.Color.Black;
            this.pnlHist.Location = new System.Drawing.Point(36, 13);
            this.pnlHist.Name = "pnlHist";
            this.pnlHist.Size = new System.Drawing.Size(455, 135);
            this.pnlHist.TabIndex = 0;
            this.pnlHist.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(466, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "255";
            // 
            // rbRed
            // 
            this.rbRed.AutoSize = true;
            this.rbRed.Location = new System.Drawing.Point(12, 225);
            this.rbRed.Name = "rbRed";
            this.rbRed.Size = new System.Drawing.Size(45, 17);
            this.rbRed.TabIndex = 3;
            this.rbRed.Text = "Red";
            this.rbRed.UseVisualStyleBackColor = true;
            this.rbRed.CheckedChanged += new System.EventHandler(this.modeChange);
            // 
            // rbIntensity
            // 
            this.rbIntensity.AutoSize = true;
            this.rbIntensity.Checked = true;
            this.rbIntensity.Location = new System.Drawing.Point(12, 202);
            this.rbIntensity.Name = "rbIntensity";
            this.rbIntensity.Size = new System.Drawing.Size(53, 17);
            this.rbIntensity.TabIndex = 4;
            this.rbIntensity.TabStop = true;
            this.rbIntensity.Text = "White";
            this.rbIntensity.UseVisualStyleBackColor = true;
            this.rbIntensity.CheckedChanged += new System.EventHandler(this.modeChange);
            // 
            // rbGreen
            // 
            this.rbGreen.AutoSize = true;
            this.rbGreen.Location = new System.Drawing.Point(12, 248);
            this.rbGreen.Name = "rbGreen";
            this.rbGreen.Size = new System.Drawing.Size(54, 17);
            this.rbGreen.TabIndex = 5;
            this.rbGreen.Text = "Green";
            this.rbGreen.UseVisualStyleBackColor = true;
            this.rbGreen.CheckedChanged += new System.EventHandler(this.modeChange);
            // 
            // rbBlue
            // 
            this.rbBlue.AutoSize = true;
            this.rbBlue.Location = new System.Drawing.Point(12, 271);
            this.rbBlue.Name = "rbBlue";
            this.rbBlue.Size = new System.Drawing.Size(46, 17);
            this.rbBlue.TabIndex = 6;
            this.rbBlue.Text = "Blue";
            this.rbBlue.UseVisualStyleBackColor = true;
            this.rbBlue.CheckedChanged += new System.EventHandler(this.modeChange);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(377, 262);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(114, 26);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUndo.Location = new System.Drawing.Point(377, 225);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(114, 26);
            this.btnUndo.TabIndex = 8;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnAuto
            // 
            this.btnAuto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAuto.Location = new System.Drawing.Point(255, 225);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(117, 26);
            this.btnAuto.TabIndex = 9;
            this.btnAuto.Text = "Auto Adjust";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // picThumbnail
            // 
            this.picThumbnail.Location = new System.Drawing.Point(65, 155);
            this.picThumbnail.Name = "picThumbnail";
            this.picThumbnail.Size = new System.Drawing.Size(183, 133);
            this.picThumbnail.TabIndex = 10;
            this.picThumbnail.TabStop = false;
            // 
            // tbAdjust
            // 
            this.tbAdjust.Location = new System.Drawing.Point(255, 178);
            this.tbAdjust.Maximum = 128;
            this.tbAdjust.Minimum = -128;
            this.tbAdjust.Name = "tbAdjust";
            this.tbAdjust.Size = new System.Drawing.Size(236, 45);
            this.tbAdjust.TabIndex = 11;
            this.tbAdjust.MouseUp += new System.Windows.Forms.MouseEventHandler(this.reevaluateContrast);
            // 
            // cbCPF
            // 
            this.cbCPF.AutoSize = true;
            this.cbCPF.Location = new System.Drawing.Point(12, 13);
            this.cbCPF.Name = "cbCPF";
            this.cbCPF.Size = new System.Drawing.Size(15, 14);
            this.cbCPF.TabIndex = 12;
            this.cbCPF.Tag = "CPF?";
            this.cbCPF.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cbCPF.UseVisualStyleBackColor = true;
            this.cbCPF.CheckedChanged += new System.EventHandler(this.modeChange);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(255, 262);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(116, 26);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmHistogram
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnUndo;
            this.ClientSize = new System.Drawing.Size(503, 300);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbCPF);
            this.Controls.Add(this.tbAdjust);
            this.Controls.Add(this.picThumbnail);
            this.Controls.Add(this.btnAuto);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.rbBlue);
            this.Controls.Add(this.rbGreen);
            this.Controls.Add(this.rbIntensity);
            this.Controls.Add(this.rbRed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlHist);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmHistogram";
            this.Text = "frmHistogram";
            this.Load += new System.EventHandler(this.frmHistogram_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picThumbnail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAdjust)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbRed;
        private System.Windows.Forms.RadioButton rbIntensity;
        private System.Windows.Forms.RadioButton rbGreen;
        private System.Windows.Forms.RadioButton rbBlue;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.PictureBox picThumbnail;
        private System.Windows.Forms.TrackBar tbAdjust;
        private System.Windows.Forms.CheckBox cbCPF;
        private System.Windows.Forms.Button btnCancel;
    }
}