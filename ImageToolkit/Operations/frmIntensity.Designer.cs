namespace ImageToolkit
{
    partial class frmIntensity
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
            this.intensityUD = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.thumbnail = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.intensityUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thumbnail)).BeginInit();
            this.SuspendLayout();
            // 
            // intensityUD
            // 
            this.intensityUD.DecimalPlaces = 2;
            this.intensityUD.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.intensityUD.Location = new System.Drawing.Point(152, 12);
            this.intensityUD.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.intensityUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.intensityUD.Name = "intensityUD";
            this.intensityUD.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.intensityUD.Size = new System.Drawing.Size(120, 20);
            this.intensityUD.TabIndex = 0;
            this.intensityUD.ValueChanged += new System.EventHandler(this.updateThumb);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Relative Intensity:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(100, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.reportIntensity);
            // 
            // thumbnail
            // 
            this.thumbnail.Location = new System.Drawing.Point(296, -1);
            this.thumbnail.Name = "thumbnail";
            this.thumbnail.Size = new System.Drawing.Size(127, 73);
            this.thumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.thumbnail.TabIndex = 3;
            this.thumbnail.TabStop = false;
            // 
            // frmIntensity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 71);
            this.Controls.Add(this.thumbnail);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.intensityUD);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmIntensity";
            this.Text = "Intensity Control";
            ((System.ComponentModel.ISupportInitialize)(this.intensityUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thumbnail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown intensityUD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox thumbnail;
    }
}