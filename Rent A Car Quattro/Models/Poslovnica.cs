using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Rent_A_Car_Quattro.Models
{
    public class Poslovnica
    {
        [Key]
        public int ID { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string grad { get; set; }
        public string opis { get; set; }
    }

}
   
