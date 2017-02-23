using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppCore.Data;

namespace WebAppCore.Entities
{
    public class Speaker
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string CompanyName { get; set; }

        public string PhoneNumber { get; set; }

        public string WebsiteUrl { get; set; }

        public string TwitterName { get; set; }

        public string GitHubName { get; set; }

        public string Bio { get; set; }

        public string HeadShotUrl { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection <Talk> Talks { get; set; }

        public int CampId { get; set; }

        public virtual Camp Camp { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
