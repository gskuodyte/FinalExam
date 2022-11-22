using AutoFixture.Kernel;
using DTO;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Runtime.Versioning;

namespace API_Controllers_UniTests.Common;
[SupportedOSPlatform("windows")]
public class SignUpDtoSpecimenBuilder : ISpecimenBuilder
{
    private readonly Mock<IFormFile> _iFormFileMock;
    public SignUpDtoSpecimenBuilder()
    {
        _iFormFileMock = new Mock<IFormFile>();
    }
    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && type == typeof(SignUpDto))
        {
            using var stream = new MemoryStream();
            _iFormFileMock
                .Setup(f => f.CopyTo(stream));


            return new SignUpDto
            {
                UserName = "LoginName",
                Password = "LoginNamePassword123",
                PersonalId = "37501050000",
                Name = "FirstLoginName",
                Surname = "LastLoginName",
                Email = "loginname@gmail.com",
                PhoneNumber = "37069553298",
                Address = new AddressDto{
                    City = "Klaipeda",
                    Street = "Smilteles",
                    ApartmentNumber = 11,
                    HouseNumber = 10,
                },
                Picture = _iFormFileMock.Object
            };
        }
        return new NoSpecimen();
    }
}

