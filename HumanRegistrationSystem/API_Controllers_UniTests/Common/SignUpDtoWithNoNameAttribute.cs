using AutoFixture;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Http;

namespace API_Controllers_UniTests.Common;

public class SignUpDtoWithNoNameAttribute : AutoDataAttribute
{
    public SignUpDtoWithNoNameAttribute() : base(() =>
    {
        var fixture = new Fixture();
        fixture.Customizations.Add(new SignUpDtoSpecimenBuilder());
        fixture.Customizations.Add(
            new TypeRelay(
                typeof(IFormFile),
                typeof(byte[])));
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        return fixture;
    })
    {
    }
}