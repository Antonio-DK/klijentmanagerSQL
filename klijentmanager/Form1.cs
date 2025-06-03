using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace klijentmanager
{
    public partial class Form1 : Form
    {
        private List<Klijenti> klijentiList = new List<Klijenti>();
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KlijentManagerDB;Integrated Security=True;";

        public Form1()
        {
            InitializeComponent();
            btnDetalji.Enabled = false;
            dataGridView.SelectionChanged += dataGridView_SelectionChanged;
            dataGridView.CellDoubleClick += dataGridView_CellDoubleClick;
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIme.Text) ||
                string.IsNullOrWhiteSpace(txtPrezime.Text) ||
                string.IsNullOrWhiteSpace(txtKontakt.Text) ||
                string.IsNullOrWhiteSpace(txtAdresa.Text))
            {
                return;
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string sql = "INSERT INTO Klijenti (Ime, Prezime, Kontakt, Adresa) VALUES (@ime, @prezime, @kontakt, @adresa)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ime", txtIme.Text.Trim());
                    cmd.Parameters.AddWithValue("@prezime", txtPrezime.Text.Trim());
                    cmd.Parameters.AddWithValue("@kontakt", txtKontakt.Text.Trim());
                    cmd.Parameters.AddWithValue("@adresa", txtAdresa.Text.Trim());
                    cmd.ExecuteNonQuery();
                }
            }

            txtIme.Clear();
            txtPrezime.Clear();
            txtKontakt.Clear();
            txtAdresa.Clear();
            MessageBox.Show("Klijent spremljen.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUcitaj_Click(object sender, EventArgs e)
        {
            klijentiList.Clear();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string sql = "SELECT ID, Ime, Prezime, Kontakt, Adresa FROM Klijenti";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string ime = reader.GetString(1);
                        string prezime = reader.GetString(2);
                        string kontakt = reader.GetString(3);
                        string adresa = reader.GetString(4);

                        Klijenti k = new Klijenti(id, ime, prezime, kontakt, adresa);
                        klijentiList.Add(k);
                    }
                }
            }

            dataGridView.DataSource = null;
            dataGridView.DataSource = klijentiList;

            if (dataGridView.Columns["Kontakt"] != null)
                dataGridView.Columns["Kontakt"].Visible = false;
            if (dataGridView.Columns["Adresa"] != null)
                dataGridView.Columns["Adresa"].Visible = false;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            btnDetalji.Enabled = (dataGridView.CurrentRow != null && dataGridView.CurrentRow.DataBoundItem != null);
        }

        private void btnDetalji_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null) return;
            Klijenti odabrani = dataGridView.CurrentRow.DataBoundItem as Klijenti;
            if (odabrani != null)
            {
                DetailForm df = new DetailForm(odabrani);
                df.ShowDialog();
            }
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Klijenti odabrani = dataGridView.Rows[e.RowIndex].DataBoundItem as Klijenti;
            if (odabrani != null)
            {
                DetailForm df = new DetailForm(odabrani);
                df.ShowDialog();
            }
        }
    }
}
