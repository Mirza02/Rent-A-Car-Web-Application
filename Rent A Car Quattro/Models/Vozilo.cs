using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Rent_A_Car_Quattro.Models
{
    public class Vozilo
    {
        [Key]
        public int ID { get; set; }

        public String marka { get; set; }

        public String model { get; set; }

        public int kilometraza { get; set; }

        public DateTime datum_zadnjeg_servisa {  get; set; }

        public double cijena_po_satu { get; set; }
        
        public double cijena_po_kilometru {  get; set; }

        [EnumDataType(typeof(VrstaTransmisije))]
        public VrstaTransmisije Transmisija { get; set; }

        [EnumDataType(typeof(VrstaGoriva))]
        public VrstaGoriva Gorivo { get; set; }

        public Boolean vozilo_rezervisano { get; set; }

        public String broj_sasije { get; set; }

        public DateTime datum_sljedeceg_servisa { get; set; }
        public int lokacija_fk { get; set; }

        public int engine_size { get; set; }
        public int broj_vrata { get; set; }
        public string grad { get; set; }

    }


    

    
}
