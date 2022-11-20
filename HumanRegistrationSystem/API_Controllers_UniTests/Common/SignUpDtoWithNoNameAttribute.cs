using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;

namespace API_Controllers_UniTests.Common
{
    public class SignUpDtoWithNoNameAttribute: AutoDataAttribute
    {
        public SignUpDtoWithNoNameAttribute() : base(() =>
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new SignUpDtoSpecimenBuilder());
            fixture.Customizations.Add(
                new TypeRelay(
                    typeof(Microsoft.AspNetCore.Http.IFormFile),
                    typeof(byte[])));
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            return fixture;
        })
        {

        }
    }
}
