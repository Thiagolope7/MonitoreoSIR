using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatosTrafico
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnDT_Click(object sender, EventArgs e)
        {
            DatosTrafico DT = new DatosTrafico();
            DT.Show();
            this.btnDT.Enabled = false; 
            
        }
    }
}
