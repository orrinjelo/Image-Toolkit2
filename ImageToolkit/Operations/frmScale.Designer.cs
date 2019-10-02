namespace ImageToolkit.Operations
{
    partial class frmScale
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
            this.udx = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.udy = new System.Windows.Forms.NumericUpDown();
            this.cbratio = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.udx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udy)).BeginInit();
            this.SuspendLayout();
            // 
            // udx
            // 
            this.udx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.udx.Location = new System.Drawing.Point(33, 12);
            this.udx.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.udx.Name = "udx";
            this.udx.Size = new System.Drawing.Size(121, 20);
            this.udx.TabIndex = 0;
            this.udx.ValueChanged += new System.EventHandler(this.xvaluechanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y";
            // 
            // udy
            // 
            this.udy.Enabled = false;
            this.udy.Location = new System.Drawing.Point(33, 45);
            this.udy.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.udy.Name = "udy";
            this.udy.Size = new System.Drawing.Size(121, 20);
            this.udy.TabIndex = 3;
            this.udy.ValueChanged += new System.EventHandler(this.yvaluechanged);
            // 
            // cbratio
            // 
            this.cbratio.AutoSize = true;
            this.cbratio.Checked = true;
            this.cbratio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbratio.Location = new System.Drawing.Point(45, 71);
            this.cbratio.Name = "cbratio";
            this.cbratio.Size = new System.Drawing.Size(109, 17);
            this.cbratio.TabIndex = 4;
            this.cbratio.Text = "Keep aspect ratio";
            this.cbratio.UseVisualStyleBackColor = true;
            this.cbratio.Click += new System.EventHandler(this.AbleDisable);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmScale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(166, 127);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbratio);
            this.Controls.Add(this.udy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.udx);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmScale";
            this.Text = "Scale";
            ((System.ComponentModel.ISupportInitialize)(this.udx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown udx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown udy;
        private System.Windows.Forms.CheckBox cbratio;
        private System.Windows.Forms.Button button1;
    }
}