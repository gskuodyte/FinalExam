using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanRegistrationSystem_Domain
{
    public class HumanInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int Id { get; set; }
        public int PersonalId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public byte[] Picture { get; set; } = null!;

        [InverseProperty("HumanInfo")]
        public virtual Address Address { get; set; } = null!;

        [ForeignKey("Id")]
        [InverseProperty("HumanInfo")]
        public virtual UserAccount UserAccount { get; set; } = null!;
    }
}
