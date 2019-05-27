namespace DatosTrafico
{
	partial class ERU
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Fec_ult_gr = new System.Windows.Forms.Label();
            this.Tip_ult_gran = new System.Windows.Forms.Label();
            this.Ult_des_ERU = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Ult_inf = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre,
            this.Estatus});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(618, 403);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            // 
            // Estatus
            // 
            this.Estatus.HeaderText = "Estatus";
            this.Estatus.Name = "Estatus";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(403, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Iniciar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(290, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fecha ultima granularidad:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(290, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tipo ultima granularidad:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(290, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Uiltima descarga ERU: ";
            // 
            // Fec_ult_gr
            // 
            this.Fec_ult_gr.AutoSize = true;
            this.Fec_ult_gr.Location = new System.Drawing.Point(473, 105);
            this.Fec_ult_gr.Name = "Fec_ult_gr";
            this.Fec_ult_gr.Size = new System.Drawing.Size(0, 13);
            this.Fec_ult_gr.TabIndex = 6;
            // 
            // Tip_ult_gran
            // 
            this.Tip_ult_gran.AutoSize = true;
            this.Tip_ult_gran.Location = new System.Drawing.Point(473, 134);
            this.Tip_ult_gran.Name = "Tip_ult_gran";
            this.Tip_ult_gran.Size = new System.Drawing.Size(0, 13);
            this.Tip_ult_gran.TabIndex = 7;
            // 
            // Ult_des_ERU
            // 
            this.Ult_des_ERU.AutoSize = true;
            this.Ult_des_ERU.Location = new System.Drawing.Point(473, 169);
            this.Ult_des_ERU.Name = "Ult_des_ERU";
            this.Ult_des_ERU.Size = new System.Drawing.Size(0, 13);
            this.Ult_des_ERU.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(290, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Ultimo informe";
            this.label4.UseMnemonic = false;
            // 
            // Ult_inf
            // 
            this.Ult_inf.AutoSize = true;
            this.Ult_inf.Location = new System.Drawing.Point(473, 199);
            this.Ult_inf.Name = "Ult_inf";
            this.Ult_inf.Size = new System.Drawing.Size(0, 13);
            this.Ult_inf.TabIndex = 10;
            // 
            // ERU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 403);
            this.Controls.Add(this.Ult_inf);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Ult_des_ERU);
            this.Controls.Add(this.Tip_ult_gran);
            this.Controls.Add(this.Fec_ult_gr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ERU";
            this.Text = "ERU";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
		private System.Windows.Forms.DataGridViewTextBoxColumn Estatus;
		private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Fec_ult_gr;
        private System.Windows.Forms.Label Tip_ult_gran;
        private System.Windows.Forms.Label Ult_des_ERU;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Ult_inf;
    }
}