using HumanRegistrationSystem.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserAccountInfoResponce
    {
        public string UserName { get; set; }
        public int PersonalID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public AddressDto AddressDto { get; set; }
        public byte[] Picture { get; set; }
        

    }
}
