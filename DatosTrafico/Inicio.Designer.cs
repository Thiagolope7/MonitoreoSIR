namespace DatosTrafico
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.btnOperacion = new System.Windows.Forms.Button();
			this.btnDT = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btnERU = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.DimGray;
			this.panel1.Controls.Add(this.btnERU);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Controls.Add(this.btnOperacion);
			this.panel1.Controls.Add(this.btnDT);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(140, 442);
			this.panel1.TabIndex = 4;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(9, 391);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(125, 39);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 5;
			this.pictureBox1.TabStop = false;
			// 
			// btnOperacion
			// 
			this.btnOperacion.FlatAppearance.BorderColor = System.Drawing.Color.Fuchsia;
			this.btnOperacion.FlatAppearance.BorderSize = 10;
			this.btnOperacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOperacion.Location = new System.Drawing.Point(12, 119);
			this.btnOperacion.Name = "btnOperacion";
			this.btnOperacion.Size = new System.Drawing.Size(114, 29);
			this.btnOperacion.TabIndex = 1;
			this.btnOperacion.Text = "Operación";
			this.btnOperacion.UseVisualStyleBackColor = true;
			this.btnOperacion.Click += new System.EventHandler(this.BtnOperacion_Click);
			// 
			// btnDT
			// 
			this.btnDT.FlatAppearance.BorderColor = System.Drawing.Color.Fuchsia;
			this.btnDT.FlatAppearance.BorderSize = 10;
			this.btnDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDT.Location = new System.Drawing.Point(12, 48);
			this.btnDT.Name = "btnDT";
			this.btnDT.Size = new System.Drawing.Size(114, 29);
			this.btnDT.TabIndex = 0;
			this.btnDT.Text = "Datos tráfico";
			this.btnDT.UseVisualStyleBackColor = true;
			this.btnDT.Click += new System.EventHandler(this.BtnDT_Click);
			// 
			// panel2
			// 
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(140, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(634, 442);
			this.panel2.TabIndex = 6;
			// 
			// btnERU
			// 
			this.btnERU.Location = new System.Drawing.Point(13, 184);
			this.btnERU.Name = "btnERU";
			this.btnERU.Size = new System.Drawing.Size(113, 25);
			this.btnERU.TabIndex = 6;
			this.btnERU.UseVisualStyleBackColor = true;
			this.btnERU.Click += new System.EventHandler(this.btnERU_Click_1);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DarkGray;
			this.ClientSize = new System.Drawing.Size(774, 442);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Form1";
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnDT;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOperacion;
		private System.Windows.Forms.Button btnERU;
	}
}

