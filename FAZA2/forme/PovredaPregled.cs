using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Deciji_Letnji_Program.Forme
{
    public partial class PovredaPregled : Form
    {
        private readonly int deteId;

        public PovredaPregled(int deteId)
        {
            InitializeComponent();
            this.deteId = deteId;
            this.Load += PovredaPregled_Load;
        }

        private async void PovredaPregled_Load(object sender, EventArgs e)
        {
            await UcitajPovredeAsync();
        }

        private async Task UcitajPovredeAsync()
        {
            try
            {
                var povrede = await DTOManager.GetPovredeDetetaAsync(deteId);

                if (povrede == null || povrede.Count == 0)
                {
                    MessageBox.Show("Ovo dete nema evidentiranih povreda.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }

                dataGridViewPovrede.DataSource = povrede;
                dataGridViewPovrede.Columns["Id"].HeaderText = "ID";
                dataGridViewPovrede.Columns["Datum"].HeaderText = "Datum";
                dataGridViewPovrede.Columns["Opis"].HeaderText = "Opis";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju povreda: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
