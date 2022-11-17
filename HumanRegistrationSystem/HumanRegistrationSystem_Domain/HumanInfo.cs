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
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public byte[] Picture { get; set; }
        [InverseProperty("HumanInfo")]
        public virtual Address Address { get; set; }
        [ForeignKey("Id")]
        [InverseProperty("HumanInfo")]
        public virtual UserAccount UserAccount { get; set; }
    }
}
