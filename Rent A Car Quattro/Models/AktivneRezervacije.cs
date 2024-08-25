using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rent_A_Car_Quattro.Models
{
    public class AktivneRezervacije
    {
        [Key]
        public int ID { get; set; }
        public int korisnikId { get; set; }
        public int rezervacijaId { get; set; }
    }
}
