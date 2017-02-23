using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCore.Entities
{
    public class Location
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string CityTown { get; set; }

        public string StateProvince { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public virtual ICollection <Camp> Camps { get; set; }
    }
}
