namespace klijentmanager
{
    public class Klijenti
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Kontakt { get; set; }
        public string Adresa { get; set; }

        public Klijenti() { }

        public Klijenti(int id, string ime, string prezime, string kontakt, string adresa)
        {
            ID = id;
            Ime = ime;
            Prezime = prezime;
            Kontakt = kontakt;
            Adresa = adresa;
        }
    }
}
