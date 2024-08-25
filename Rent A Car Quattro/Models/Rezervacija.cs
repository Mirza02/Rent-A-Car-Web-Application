using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rent_A_Car_Quattro.Models;

public class Rezervacija
{
    [Key]
    public int ID { get; set; }

    public DateTime pocetak_rezervacije { get; set; }

    public DateTime kraj_rezervacije { get; set; }

    public int vrijeme_rezervacije { get; set; }

    public int kilometraza_rezervacije { get; set; }

    public Boolean kilometraza_ili_vrijeme { get; set; }

    public Double cijena_rezervacija { get; set; }

    public int lokacija_fk { get; set; }

    public string korisnik_fk { get; set; }

    public int placanje_fk { get; set; }

    public int vozilo_fk { get; set; }

    [NotMapped]
    public double cijena_po_satu { get; set; } // Add this property to hold the value temporarily
}
