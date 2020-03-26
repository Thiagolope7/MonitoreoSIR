

namespace DatosTrafico
{
    using System;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
		public static int dat = 0;
		
        public Form1()
        {
            InitializeComponent();
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
	}
}
