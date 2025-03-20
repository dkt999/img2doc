namespace Img2Word
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lvImages = new System.Windows.Forms.ListView();
            this.nudQuality = new System.Windows.Forms.NumericUpDown();
            this.btnExportWord = new System.Windows.Forms.Button();
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.btnExportAsImg = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnOpenFoler = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbOutputSize = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuality)).BeginInit();
            this.SuspendLayout();
            // 
            // lvImages
            // 
            this.lvImages.AllowDrop = true;
            this.lvImages.Location = new System.Drawing.Point(12, 12);
            this.lvImages.Name = "lvImages";
            this.lvImages.Size = new System.Drawing.Size(323, 356);
            this.lvImages.TabIndex = 0;
            this.lvImages.UseCompatibleStateImageBehavior = false;
            this.lvImages.DragDrop += new System.Windows.Forms.DragEventHandler(this.LvImages_DragDrop);
            this.lvImages.DragEnter += new System.Windows.Forms.DragEventHandler(this.LvImages_DragEnter);
            // 
            // nudQuality
            // 
            this.nudQuality.Location = new System.Drawing.Point(340, 12);
            this.nudQuality.Name = "nudQuality";
            this.nudQuality.Size = new System.Drawing.Size(47, 20);
            this.nudQuality.TabIndex = 1;
            this.nudQuality.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudQuality.ValueChanged += new System.EventHandler(this.nudQuality_ValueChanged);
            // 
            // btnExportWord
            // 
            this.btnExportWord.Location = new System.Drawing.Point(340, 39);
            this.btnExportWord.Name = "btnExportWord";
            this.btnExportWord.Size = new System.Drawing.Size(96, 23);
            this.btnExportWord.TabIndex = 2;
            this.btnExportWord.Text = "Export as Word";
            this.btnExportWord.UseVisualStyleBackColor = true;
            this.btnExportWord.Click += new System.EventHandler(this.btnExportWord_Click);
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.Location = new System.Drawing.Point(340, 68);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(96, 23);
            this.btnExportPDF.TabIndex = 3;
            this.btnExportPDF.Text = "Export as PDF";
            this.btnExportPDF.UseVisualStyleBackColor = true;
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click);
            // 
            // btnExportAsImg
            // 
            this.btnExportAsImg.Location = new System.Drawing.Point(340, 97);
            this.btnExportAsImg.Name = "btnExportAsImg";
            this.btnExportAsImg.Size = new System.Drawing.Size(96, 23);
            this.btnExportAsImg.TabIndex = 4;
            this.btnExportAsImg.Text = "Export as image";
            this.btnExportAsImg.UseVisualStyleBackColor = true;
            this.btnExportAsImg.Click += new System.EventHandler(this.btnExportAsImg_Click);
            // 
            // txtPath
            // 
            this.txtPath.Enabled = false;
            this.txtPath.Location = new System.Drawing.Point(13, 378);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(322, 20);
            this.txtPath.TabIndex = 5;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(341, 378);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(93, 23);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnOpenFoler
            // 
            this.btnOpenFoler.Location = new System.Drawing.Point(341, 345);
            this.btnOpenFoler.Name = "btnOpenFoler";
            this.btnOpenFoler.Size = new System.Drawing.Size(93, 23);
            this.btnOpenFoler.TabIndex = 7;
            this.btnOpenFoler.Text = "Open folder";
            this.btnOpenFoler.UseVisualStyleBackColor = true;
            this.btnOpenFoler.Click += new System.EventHandler(this.btnOpenFoler_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 402);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Output size (approximate):";
            // 
            // lbOutputSize
            // 
            this.lbOutputSize.Location = new System.Drawing.Point(147, 402);
            this.lbOutputSize.Name = "lbOutputSize";
            this.lbOutputSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbOutputSize.Size = new System.Drawing.Size(58, 23);
            this.lbOutputSize.TabIndex = 9;
            this.lbOutputSize.Text = "0 MB";
            this.lbOutputSize.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(343, 316);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(394, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(42, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 424);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbOutputSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpenFoler);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnExportAsImg);
            this.Controls.Add(this.btnExportPDF);
            this.Controls.Add(this.btnExportWord);
            this.Controls.Add(this.nudQuality);
            this.Controls.Add(this.lvImages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Img2Doc 1.0 (by dkt999)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.nudQuality)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvImages;
        private System.Windows.Forms.NumericUpDown nudQuality;
        private System.Windows.Forms.Button btnExportWord;
        private System.Windows.Forms.Button btnExportPDF;
        private System.Windows.Forms.Button btnExportAsImg;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnOpenFoler;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbOutputSize;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

