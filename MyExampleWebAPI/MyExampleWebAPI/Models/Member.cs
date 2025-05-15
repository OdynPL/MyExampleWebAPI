using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyExampleWebAPI.Models
{
    /// <summary>
    /// Basic Member model used in application for patient
    /// </summary>
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }
        public decimal CurrentWeight { get; set; }
        public int Age { get; set; }
        public required string FirstName { get; set; } 
        public required string LastName { get; set; } 
        public DateTime JoinDate { get; set; } 
        public decimal Height { get; set; }  
        public required GenderType Gender { get; set; }  
        public bool IsActive { get; set; }   
        public decimal BMI { get; set; }
        public ICollection<WeightEntry> WeightEntries { get; set; } = new List<WeightEntry>();

    }
}
