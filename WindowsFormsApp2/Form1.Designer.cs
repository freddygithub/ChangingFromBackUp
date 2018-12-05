namespace WindowsFormsApp2
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
            this.upper = new System.Windows.Forms.Button();
            this.lower = new System.Windows.Forms.Button();
            this.spare = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // upper
            // 
            this.upper.BackColor = System.Drawing.Color.Blue;
            this.upper.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upper.Location = new System.Drawing.Point(39, 112);
            this.upper.Name = "upper";
            this.upper.Size = new System.Drawing.Size(204, 194);
            this.upper.TabIndex = 0;
            this.upper.Text = "Upper Server";
            this.upper.UseVisualStyleBackColor = false;
            this.upper.Click += new System.EventHandler(this.upper_Click);
            // 
            // lower
            // 
            this.lower.BackColor = System.Drawing.Color.OrangeRed;
            this.lower.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lower.Location = new System.Drawing.Point(296, 112);
            this.lower.Name = "lower";
            this.lower.Size = new System.Drawing.Size(195, 194);
            this.lower.TabIndex = 1;
            this.lower.Text = "Lower Server";
            this.lower.UseVisualStyleBackColor = false;
            this.lower.Click += new System.EventHandler(this.lower_Click);
            // 
            // spare
            // 
            this.spare.BackColor = System.Drawing.Color.WhiteSmoke;
            this.spare.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spare.Location = new System.Drawing.Point(548, 112);
            this.spare.Name = "spare";
            this.spare.Size = new System.Drawing.Size(195, 194);
            this.spare.TabIndex = 2;
            this.spare.Text = "Spare Server";
            this.spare.UseVisualStyleBackColor = false;
            this.spare.Click += new System.EventHandler(this.spare_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(-3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(805, 448);
            this.panel1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(846, 489);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.spare);
            this.Controls.Add(this.lower);
            this.Controls.Add(this.upper);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button upper;
        private System.Windows.Forms.Button lower;
        private System.Windows.Forms.Button spare;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

