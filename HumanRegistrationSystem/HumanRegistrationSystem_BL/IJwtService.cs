using HumanRegistrationSystem_Domain;

namespace HumanRegistrationSystem_BL
{
    public interface IJwtService
    {
        string GetJwtToken(UserAccount userAccount);
    }
}
