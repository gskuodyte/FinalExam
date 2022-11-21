using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Text;
using Common.Validation;
using DTO;
using HumanRegistrationSystem_DAL;
using HumanRegistrationSystem_Domain;
using Microsoft.AspNetCore.Http;

namespace HumanRegistrationSystem_BL;

public class UserAccountService : IUserAccountService
{
    private readonly IDbRepository _dbRepository;

    public UserAccountService(IDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public async Task<bool> CreateUserAccountAsync(SignUpDto signUpDto, byte[] picture)
    {
        var existingUser = await _dbRepository.GetAccountByUserNameAsync(signUpDto.UserName);
        if (existingUser != null) return false;

        var (hash, salt) = CreatePasswordHash(signUpDto.Password);


        var newUser = new UserAccount
        {
            UserName = signUpDto.UserName,
            PasswordHash = hash,
            PasswordSalt = salt,
            Role = "User",
            HumanInfo = new HumanInfo
            {
                Name = signUpDto.Name,
                Surname = signUpDto.Surname,
                PersonalId = signUpDto.PersonalId,
                PhoneNumber = signUpDto.PhoneNumber,
                Email = signUpDto.Email,
                Picture = picture,

                Address = new Address
                {
                    City = signUpDto.Address.City,
                    Street = signUpDto.Address.Street,
                    HouseNumber = signUpDto.Address.HouseNumber,
                    ApartmentNumber = signUpDto.Address.ApartmentNumber
                }
            }
        };

        await _dbRepository.InsertAccountAsync(newUser);

        return true;
    }

    public async Task<(bool authenticationSuccessful, UserAccount? userAccount)> LoginAsync(string username,
        string password)
    {
        var account = await _dbRepository.GetAccountByUserNameAsync(username);
        if (account == null) return (false, null);

        if (VerifyPasswordHash(password, account.PasswordHash, account.PasswordSalt)) return (true, account);
        return (false, null);
    }

    public async Task<bool> UpdateUserPersonalIdAsync(int id, string personalId)
    {
        return await _dbRepository.UpdatePersonalIdAsync(id, personalId);
    }

    public async Task<bool> UpdateUserNameAsync(int id, string name)
    {
        return await _dbRepository.UpdateNameAsync(id, name);
    }

    public async Task<bool> UpdateUserSurnameAsync(int id, string surname)
    {
        return await _dbRepository.UpdateSurnameAsync(id, surname);
    }

    public async Task<bool> UpdateUserPhoneNumberAsync(int id, string phoneNumber)
    {
        return await _dbRepository.UpdatePhoneNumberAsync(id, phoneNumber);
    }

    public async Task<bool> UpdateUserEmailAsync(int id, string email)
    {
        return await _dbRepository.UpdateEmailAsync(id, email);
    }

    public async Task<bool> UpdateImageAsync(int id, byte[] picture)
    {
        return await _dbRepository.UpdateImageAsync(id, picture);
    }

    public async Task<bool> UpdateUserCityAddressAsync(int id, string city)
    {
        return await _dbRepository.UpdateCityAsync(id, city);
    }

    public async Task<bool> UpdateUserStreetAddressAsync(int id, string street)
    {
        return await _dbRepository.UpdateStreetAsync(id, street);
    }

    public async Task<bool> UpdateUserHouseNumberAddressAsync(int id, int houseNumber)
    {
        return await _dbRepository.UpdateHouseNumberAsync(id, houseNumber);
    }

    public async Task<bool> UpdateUserApartmentNumberAddressAsync(int id, int apartmentNumber)
    {
        return await _dbRepository.UpdateApartmentNumberAsync(id, apartmentNumber);
    }


    public async Task<UserAccountInfoResponce> GetMappedUserAccountAsync(int id)
    {
        try
        {
            var responce = await _dbRepository.GetUserByIdAsync(id);

            var userDto = new UserAccountInfoResponce
            {
                UserName = responce!.UserName,
                Name = responce.HumanInfo.Name,
                Surname = responce.HumanInfo.Surname,
                PersonalId = responce.HumanInfo.PersonalId,
                PhoneNumber = responce.HumanInfo.PhoneNumber,
                Email = responce.HumanInfo.Email,
                AddressDto = new AddressDto
                {
                    City = responce.HumanInfo.Address.City,
                    Street = responce.HumanInfo.Address.Street,
                    HouseNumber = responce.HumanInfo.Address.HouseNumber,
                    ApartmentNumber = responce.HumanInfo.Address.ApartmentNumber
                },
                Picture = responce.HumanInfo.Picture
            };
            return userDto;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public async Task<bool> DeleteUserAsync(int id)
    {
        var existingUser = await _dbRepository.GetUserByIdAsync(id);

        if (Validation.CheckIfNull(existingUser)) return false;
        await _dbRepository.DeleteUserAsync(existingUser);
        return true;
    }

    public async Task<byte[]> FileUploadAsync(IFormFile file, int width, int height)
    {
        if (file == null || file.Length == 0) return null;

        var memoryStream = new MemoryStream();

        await file.CopyToAsync(memoryStream);
        var img = Image.FromStream(memoryStream);
        var imageResized = await ResizeImageAsync(img, width, height);

        return imageResized;
    }

    private static (byte[] hash, byte[] salt) CreatePasswordHash(string password)
    {
        using var hmac = new HMACSHA512();
        var salt = hmac.Key;
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return (hash, salt);
    }

    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return computedHash.SequenceEqual(passwordHash);
    }


    public static Task<byte[]> ResizeImageAsync(Image image, int width, int height)
    {
        var destRect = new Rectangle(0, 0, width, height);
        var destImage = new Bitmap(width, height);

        destImage.SetResolution(200, 200);

        using (var graphics = Graphics.FromImage(destImage))
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            //graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using var wrapMode = new ImageAttributes();
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
        }

        using var ms = new MemoryStream();
        destImage.Save(ms, ImageFormat.Png);

        return Task.FromResult(ms.ToArray());
    }
}