namespace ImageToolkit.Operations
{
    partial class frmRotate
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
            this.rbCW = new System.Windows.Forms.RadioButton();
            this.rbCCW = new System.Windows.Forms.RadioButton();
            this.rbFlip = new System.Windows.Forms.RadioButton();
            this.rbArbitrary = new System.Windows.Forms.RadioButton();
            this.udAngle = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.udAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // rbCW
            // 
            this.rbCW.AutoSize = true;
            this.rbCW.Location = new System.Drawing.Point(428, 129);
            this.rbCW.Name = "rbCW";
            this.rbCW.Size = new System.Drawing.Size(92, 17);
            this.rbCW.TabIndex = 0;
            this.rbCW.TabStop = true;
            this.rbCW.Text = "Clockwise 90°";
            this.rbCW.UseVisualStyleBackColor = true;
            this.rbCW.Click += new System.EventHandler(this.disableAngleBox);
            // 
            // rbCCW
            // 
            this.rbCCW.AutoSize = true;
            this.rbCCW.Location = new System.Drawing.Point(428, 153);
            this.rbCCW.Name = "rbCCW";
            this.rbCCW.Size = new System.Drawing.Size(131, 17);
            this.rbCCW.TabIndex = 1;
            this.rbCCW.TabStop = true;
            this.rbCCW.Text = "Counter-clockwise 90°";
            this.rbCCW.UseVisualStyleBackColor = true;
            this.rbCCW.Click += new System.EventHandler(this.disableAngleBox);
            // 
            // rbFlip
            // 
            this.rbFlip.AutoSize = true;
            this.rbFlip.Location = new System.Drawing.Point(428, 177);
            this.rbFlip.Name = "rbFlip";
            this.rbFlip.Size = new System.Drawing.Size(82, 17);
            this.rbFlip.TabIndex = 2;
            this.rbFlip.TabStop = true;
            this.rbFlip.Text = "Rotate 180°";
            this.rbFlip.UseVisualStyleBackColor = true;
            this.rbFlip.Click += new System.EventHandler(this.disableAngleBox);
            // 
            // rbArbitrary
            // 
            this.rbArbitrary.AutoSize = true;
            this.rbArbitrary.Location = new System.Drawing.Point(428, 201);
            this.rbArbitrary.Name = "rbArbitrary";
            this.rbArbitrary.Size = new System.Drawing.Size(129, 17);
            this.rbArbitrary.TabIndex = 3;
            this.rbArbitrary.TabStop = true;
            this.rbArbitrary.Text = "Rotate arbitrary angle:";
            this.rbArbitrary.UseVisualStyleBackColor = true;
            this.rbArbitrary.Click += new System.EventHandler(this.enableAngleBox);
            // 
            // udAngle
            // 
            this.udAngle.Enabled = false;
            this.udAngle.Location = new System.Drawing.Point(564, 201);
            this.udAngle.Maximum = new decimal(new int[] {
            720,
            0,
            0,
            0});
            this.udAngle.Minimum = new decimal(new int[] {
            720,
            0,
            0,
            -2147483648});
            this.udAngle.Name = "udAngle";
            this.udAngle.Size = new System.Drawing.Size(120, 20);
            this.udAngle.TabIndex = 4;
            this.udAngle.ValueChanged += new System.EventHandler(this.checkIfBoundary);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(428, 260);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(260, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // picBox
            // 
            this.picBox.BackColor = System.Drawing.Color.Black;
            this.picBox.Location = new System.Drawing.Point(13, 13);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(408, 383);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox.TabIndex = 6;
            this.picBox.TabStop = false;
            this.picBox.Paint += new System.Windows.Forms.PaintEventHandler(this.picBox_Paint);
            this.picBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseDown);
            this.picBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseMove);
            this.picBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseUp);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(429, 231);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(259, 23);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // frmRotate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 408);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.udAngle);
            this.Controls.Add(this.rbArbitrary);
            this.Controls.Add(this.rbFlip);
            this.Controls.Add(this.rbCCW);
            this.Controls.Add(this.rbCW);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmRotate";
            this.Text = "Rotate";
            this.Load += new System.EventHandler(this.plotPic);
            ((System.ComponentModel.ISupportInitialize)(this.udAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbCW;
        private System.Windows.Forms.RadioButton rbCCW;
        private System.Windows.Forms.RadioButton rbFlip;
        private System.Windows.Forms.RadioButton rbArbitrary;
        private System.Windows.Forms.NumericUpDown udAngle;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.Button btnReset;
    }
}