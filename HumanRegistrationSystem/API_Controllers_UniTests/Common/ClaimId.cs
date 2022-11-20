using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API_Controllers_UniTests.Common
{
    public class ClaimId
    {
        private readonly ControllerBase _sut;

        public ClaimId(ControllerBase sut)
        {
            _sut = sut;
        }
        public void UserMockRole()
        {
            var userMock = new ClaimsPrincipal(new ClaimsIdentity(
                new[]
                {
                    new Claim("UserId", "1"),
                    new Claim(ClaimTypes.Role, "User")
                }));

            _sut.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = userMock }
            };
        }

        public void AdminMockRole()
        {
            var userMock = new ClaimsPrincipal(new ClaimsIdentity(
                new[]
                {
                    new Claim("UserId", "2"),
                    new Claim(ClaimTypes.Role, "Admin")
                }));

            _sut.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = userMock }
            };
        }
    }
}
