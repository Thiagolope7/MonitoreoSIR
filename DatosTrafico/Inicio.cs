

namespace DatosTrafico
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
		public static int dat = 0;
        int Cerrar = 0;
        public Form1()
        {
            InitializeComponent();
            this.btnOperacion.Enabled = false;
            this.btnERU.Enabled = false; 
        }
		DatosTrafico DT1 = new DatosTrafico();
		Operacion Op = new Operacion();
		ERU ERU1 = new ERU();


		public void Llamar()
		{
			
			
		}
        private void DatosTrafico(object frmDT)
			
        {
			dat++;

			if (this.panel2.Controls.Count > 0)
                this.panel2.Controls.RemoveAt(0);
			
				DT1.TopLevel = false;
				DT1.Dock = DockStyle.Fill;
				DT1.Show();
				this.panel2.Controls.Add(DT1);
				this.panel2.Tag = DT1;
				this.panel2.Show();
					
		
        }
        private void BtnDT_Click(object sender, EventArgs e)
        {
            DatosTrafico(new DatosTrafico());   
        }

        private void Operacion(object frmOp)
        {
            if (this.panel2.Controls.Count > 0)
                this.panel2.Controls.RemoveAt(0);
            Op.TopLevel = false;
            Op.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(Op);
            this.panel2.Tag = Op;
            Op.Show();
			this.panel2.Show();

		}

        private void BtnOperacion_Click(object sender, EventArgs e)
        {
            Operacion(new Operacion());
        }

        private void ERU(object frmERU)
        {
            if (this.panel2.Controls.Count > 0)
                this.panel2.Controls.RemoveAt(0);
           
            ERU1.TopLevel = false;
            ERU1.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(ERU1);
            this.panel2.Tag = ERU1;
            ERU1.Show();
			this.panel2.Show();
		}
        private void btnERU_Click(object sender, EventArgs e)
        {
            ERU(new ERU());
        }

		private void btnERU_Click_1(object sender, EventArgs e)
		{
            ERU(new ERU());

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
           
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (Cerrar == 1)
            {
                
            }
            else
            {
                e.Cancel = true;
                this.Hide();
            }
           
        }

        private void restaurarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Cerrar = Cerrar + 1; 
            this.Close();
        }
    }
}
