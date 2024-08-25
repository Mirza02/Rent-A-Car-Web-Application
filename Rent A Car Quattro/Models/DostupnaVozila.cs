using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rent_A_Car_Quattro.Models
{
    public class DostupnaVozila
    {
        [Key]
        public int ID { get; set; }
        public int VoziloId { get; set; }
        public int PoslovnicaId { get; set; }
    }
}
