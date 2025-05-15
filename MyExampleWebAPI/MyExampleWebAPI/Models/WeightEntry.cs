using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyExampleWebAPI.Models
{
    /// <summary>
    /// Weight entry object used for relation with Member
    /// </summary>
    public class WeightEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Weight { get; set; }

        public Guid MemberId { get; set; }

        [ForeignKey(nameof(MemberId))]
        public Member Member { get; set; } = null!;
    }
}
