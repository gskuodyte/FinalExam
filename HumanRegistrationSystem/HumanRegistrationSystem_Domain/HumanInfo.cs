using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HumanRegistrationSystem_Domain
{
    public class HumanInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int Id { get; set; }
        public int PersonalID { get; set; }
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
