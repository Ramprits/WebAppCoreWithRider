using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppCore.Data;

namespace WebAppCore.Entities
{
    public class Camp
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Moniker { get; set; }

        public string Name { get; set; }

        public DateTime? EventDate { get; set; }

        public int Length { get; set; }

        public string Description { get; set; }

        public int LocationId { get; set; }

        public Location Location { get; set; }

        public int ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection <Speaker> Speakers { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
