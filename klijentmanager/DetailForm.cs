using System.Windows.Forms;

namespace klijentmanager
{
    public partial class DetailForm : Form
    {
        public DetailForm(Klijenti k)
        {
            InitializeComponent();
            dID.Text = k.ID.ToString();
            dIme.Text = k.Ime;
            dPrezime.Text = k.Prezime;
            dKontakt.Text = k.Kontakt;
            dAdresa.Text = k.Adresa;
        }
    }
}
