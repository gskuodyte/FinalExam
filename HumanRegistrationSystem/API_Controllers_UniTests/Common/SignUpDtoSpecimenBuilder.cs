﻿using AutoFixture.Kernel;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API_Controllers_UniTests.Common
{
    public class SignUpDtoSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(SignUpDto))
            {
                var stream = new MemoryStream();
                return new SignUpDto
                {
                    UserName = "GSK",
                    Password = "123456",
                    PersonalID = 84651,
                    Name = "",
                    Surname = "eswdfd",
                    PhoneNumber = "54120561651",
                    Email = "greta@gmail.com",
                    Address = new AddressDto()
                    {
                        City = "ergfsa",
                        Street = "sadfsfd",
                        HouseNumber = 1,
                        ApartmentNumber = 10
                    },
                    //Picture = new FormFile(stream, 200, 200, "Counter", ".jpg")
                };
            }
            return new NoSpecimen();
        }

    }
}