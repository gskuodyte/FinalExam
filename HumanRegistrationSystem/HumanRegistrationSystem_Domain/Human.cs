using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanRegistrationSystem_Domain
{
    public class Human
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PersonalID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int TelephoneNumber { get; set; }
        public string Email { get; set; }
        public Image Image { get; set; }
        public Address Address { get; set; }
    }
}
