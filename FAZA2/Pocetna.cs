using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Deciji_Letnji_Program.Forme;

namespace Deciji_Letnji_Program
{
    public partial class Pocetna : Form
    {
        public Pocetna()
        {
            InitializeComponent();
        }
        private void dete_Click(object sender, EventArgs e)
        {
            DetePregled form = new DetePregled();
            form.ShowDialog();
        }
        private void roditelj_Click(object sender, EventArgs e)
        {
            RoditeljPregled form = new RoditeljPregled();
            form.ShowDialog();
        }
        private void prijava_Click(object sender, EventArgs e)
        {
            PrijavaPregled form = new PrijavaPregled();
            form.ShowDialog();
        }
        private void aktivnost_Click(object sender, EventArgs e)
        {
            AktivnostPregled form = new AktivnostPregled();
            form.ShowDialog();
        }
        private void angazovanoLice_Click(object sender, EventArgs e)
        {
            AngazovanoLiceDodajIzmeni form = new AngazovanoLiceDodajIzmeni();
            form.ShowDialog();
        }
    }
}
