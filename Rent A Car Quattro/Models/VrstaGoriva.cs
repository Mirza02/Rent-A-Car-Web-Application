using System.ComponentModel.DataAnnotations;

namespace Rent_A_Car_Quattro.Models
{
    public enum VrstaGoriva
    {
        [Display(Name = "Dizel")]
        Dizel,
        [Display(Name = "Benzin")]
        Benzin,
        [Display(Name = "Hibrid")]
        Hibrid,
        [Display(Name = "Elektricni")]
        Elektricni
    }
}
