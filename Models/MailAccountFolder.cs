using System.ComponentModel.DataAnnotations;

namespace MailArchiver.Models
{
    public class MailAccountFolder
    {
        public int Id { get; set; }
        [Required]
        public int MailAccountId { get; set; }
        public uint? UidValidity { get; set; }
        public string Name { get; set;}
        public DateTime LastSync { get; set; }
        public ulong? LastSyncModSeq { get; set; }

        public virtual MailAccount MailAccount { get; set; }
    }
}