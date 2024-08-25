using System.ComponentModel.DataAnnotations;

namespace Rent_A_Car_Quattro.Models
{
	public class Placanje
	{
		[Key]
		public int placanje_id { get; set; }
		[EnumDataType(typeof(VrstaPlacanja))]
		public VrstaPlacanja vrsta_placanja { get; set; }
		public Double cijena { get; set; }
		public DateTime vrijeme_placanja { get; set; }
		public Boolean uspjesno_placanje { get; set; }
	}
}

