

namespace DatosTrafico
{
    using System;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void DatosTrafico(object frmDT)
        {
            if (this.panel2.Controls.Count > 0)
                this.panel2.Controls.RemoveAt(0);
            DatosTrafico DT = frmDT as DatosTrafico;
            DT.TopLevel = false;
            DT.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(DT);
            this.panel2.Tag = DT;
            DT.Show();

        }
        private void BtnDT_Click(object sender, EventArgs e)
        {
            DatosTrafico(new DatosTrafico());   
        }

        private void Operacion(object frmOp)
        {
            if (this.panel2.Controls.Count > 0)
                this.panel2.Controls.RemoveAt(0);
            Operacion Op = frmOp as Operacion;
            Op.TopLevel = false;
            Op.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(Op);
            this.panel2.Tag = Op;
            Op.Show();

        }

        private void BtnOperacion_Click(object sender, EventArgs e)
        {
            Operacion(new Operacion());
        }

        private void ERU(object frmERU)
        {
            if (this.panel2.Controls.Count > 0)
                this.panel2.Controls.RemoveAt(0);
            ERU ERU = frmERU as ERU;
            ERU.TopLevel = false;
            ERU.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(ERU);
            this.panel2.Tag = ERU;
            ERU.Show();

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
