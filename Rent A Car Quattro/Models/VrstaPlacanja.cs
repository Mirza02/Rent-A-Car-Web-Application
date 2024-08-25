using System.ComponentModel.DataAnnotations;

namespace Rent_A_Car_Quattro.Models
{
	public enum VrstaPlacanja
	{
        [Display(Name = "Kartica")]
        Kartica,
        [Display(Name = "Racun")]
        Racun
    }
}

