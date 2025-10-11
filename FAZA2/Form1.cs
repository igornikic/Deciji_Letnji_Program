using Deciji_Letnji_Program.Forme;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Deciji_Letnji_Program
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dete_Click(object sender, EventArgs e)
        {
            FormDeteDetalji form = new FormDeteDetalji();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataLayer.ProveriKonekciju();
        }
    }
}
