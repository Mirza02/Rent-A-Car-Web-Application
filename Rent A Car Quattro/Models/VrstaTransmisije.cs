using System.ComponentModel.DataAnnotations;

namespace Rent_A_Car_Quattro.Models
{
    public enum VrstaTransmisije
    {
        [Display(Name = "Automatik")]
        Automatik,
        [Display(Name = "Manuelni")]
        Manuelna
    }
}
