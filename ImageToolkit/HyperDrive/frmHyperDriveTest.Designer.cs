namespace ImageToolkit
{
    partial class frmHyperDriveTest
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
            this.runTest = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblproc = new System.Windows.Forms.Label();
            this.udops = new System.Windows.Forms.NumericUpDown();
            this.udits = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.lbProgress = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.udops)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udits)).BeginInit();
            this.SuspendLayout();
            // 
            // runTest
            // 
            this.runTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runTest.Location = new System.Drawing.Point(13, 13);
            this.runTest.Name = "runTest";
            this.runTest.Size = new System.Drawing.Size(128, 38);
            this.runTest.TabIndex = 0;
            this.runTest.Text = "Run Test";
            this.runTest.UseVisualStyleBackColor = true;
            this.runTest.Click += new System.EventHandler(this.runTest_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(251, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(131, 39);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.closeHyperDriveTest);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Number of Processors: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Number of Operations:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Iterations per Operation: ";
            // 
            // lblproc
            // 
            this.lblproc.AutoSize = true;
            this.lblproc.Location = new System.Drawing.Point(159, 63);
            this.lblproc.Name = "lblproc";
            this.lblproc.Size = new System.Drawing.Size(13, 13);
            this.lblproc.TabIndex = 5;
            this.lblproc.Text = "0";
            // 
            // udops
            // 
            this.udops.Location = new System.Drawing.Point(162, 94);
            this.udops.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.udops.Name = "udops";
            this.udops.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.udops.Size = new System.Drawing.Size(120, 20);
            this.udops.TabIndex = 6;
            // 
            // udits
            // 
            this.udits.Location = new System.Drawing.Point(169, 131);
            this.udits.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.udits.Name = "udits";
            this.udits.Size = new System.Drawing.Size(120, 20);
            this.udits.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Progress: ";
            // 
            // lbProgress
            // 
            this.lbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbProgress.FormattingEnabled = true;
            this.lbProgress.HorizontalScrollbar = true;
            this.lbProgress.Location = new System.Drawing.Point(19, 181);
            this.lbProgress.Name = "lbProgress";
            this.lbProgress.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbProgress.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbProgress.Size = new System.Drawing.Size(363, 264);
            this.lbProgress.TabIndex = 9;
            // 
            // frmHyperDriveTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 456);
            this.Controls.Add(this.lbProgress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.udits);
            this.Controls.Add(this.udops);
            this.Controls.Add(this.lblproc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.runTest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmHyperDriveTest";
            this.Text = "Hyper Drive Test";
            ((System.ComponentModel.ISupportInitialize)(this.udops)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udits)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button runTest;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblproc;
        private System.Windows.Forms.NumericUpDown udops;
        private System.Windows.Forms.NumericUpDown udits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbProgress;
    }
}