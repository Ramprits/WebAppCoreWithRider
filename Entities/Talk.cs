using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppCore.Data;

namespace WebAppCore.Entities
{
    public class Talk
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Abstract { get; set; }

        public string Category { get; set; }

        public string Level { get; set; }

        public string Prerequisites { get; set; }

        public DateTime StartingTime { get; set; }

        public string Room { get; set; }

        public int ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        public int SpeakerId { get; set; }

        public virtual Speaker Speaker { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
