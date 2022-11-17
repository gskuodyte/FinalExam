using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanRegistrationSystem_Domain
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int ApartmentNumber { get; set; }
        [ForeignKey("Id")]
        [InverseProperty("Address")]
        public virtual HumanInfo HumanInfo { get; set; }
    }
}
