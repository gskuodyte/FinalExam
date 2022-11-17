﻿using DTO;
using HumanRegistrationSystem_DAL;
using HumanRegistrationSystem_Domain;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Common.Validation;
using Image = System.Drawing.Image;
using Microsoft.AspNetCore.Http;

namespace HumanRegistrationSystem_BL
{
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
            if (existingUser != null)
            {
                return false;
            }

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
                    PersonalId = signUpDto.PersonalID,
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
            await _dbRepository.SaveChangesAsync();

            return true;
        }

        public async Task<(bool authenticationSuccessful, UserAccount? userAccount)> LoginAsync(string username, string password)
        {
            var account = await _dbRepository.GetAccountByUserNameAsync(username);
            if (account == null)
            {
                return (false, null);
            }

            if (VerifyPasswordHash(password, account.PasswordHash, account.PasswordSalt))
            {
                return (true, account);
            }
            else
            {
                return (false, null);
            }
        }
        private (byte[] hash, byte[] salt) CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512();
            var salt = hmac.Key;
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return (hash, salt);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }

        public async Task<bool> UpdateUserPersonalId(int id, int personalId)
        {
            var user = await _dbRepository.GetUserByIdAsync(id);

            user!.HumanInfo.PersonalId = personalId;
            await _dbRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUserName(int id, string name)
        {
            var user = await _dbRepository.GetUserByIdAsync(id);
            
            user!.HumanInfo.Name = name;
            await _dbRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUserSurname(int id, string surname)
        {
            var user = await _dbRepository.GetUserByIdAsync(id);
            
            user!.HumanInfo.Surname = surname;
            await _dbRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUserPhoneNumber(int id, string phoneNumber)
        {
            var user = await _dbRepository.GetUserByIdAsync(id);

            user!.HumanInfo.PhoneNumber = phoneNumber;
            await _dbRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUserEmail(int id, string email)
        {
            var user = await _dbRepository.GetUserByIdAsync(id);

            user!.HumanInfo.Email = email;
            await _dbRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateImageAsync(int id, byte[] picture)
        {
            var user = await _dbRepository.GetUserByIdAsync(id);
            
            user!.HumanInfo.Picture = picture;
            await _dbRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUserCityAddress(int id, string city)
        {
            var user = await _dbRepository.GetUserByIdAsync(id);
            
            user!.HumanInfo.Address.City = city;
            await _dbRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUserStreetAddress(int id, string street)
        {
            var user = await _dbRepository.GetUserByIdAsync(id);
            
            user!.HumanInfo.Address.Street = street;
            await _dbRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUserHouseNumberAddress(int id, int houseNumber)
        {
            var user = await _dbRepository.GetUserByIdAsync(id);

            user!.HumanInfo.Address.HouseNumber = houseNumber;
            await _dbRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUserApartmentNumberAddress(int id, int apartmentNumber)
        {
            var user = await _dbRepository.GetUserByIdAsync(id);
            
            user!.HumanInfo.Address.ApartmentNumber = apartmentNumber;
            await _dbRepository.SaveChangesAsync();

            return true;
        }

        

        public async Task<UserAccountInfoResponce> GetMapedUserAccount(int id)
        {
            var responce = await _dbRepository.GetUserByIdAsync(id);

            var userDto = new UserAccountInfoResponce
            {
                UserName = responce!.UserName,
                Name = responce.HumanInfo.Name,
                Surname = responce.HumanInfo.Surname,
                PersonalID = responce.HumanInfo.PersonalId,
                PhoneNumber = responce.HumanInfo.PhoneNumber,
                Email = responce.HumanInfo.Email,
                AddressDto = new AddressDto
                {
                    City = responce.HumanInfo.Address.City,
                    Street = responce.HumanInfo.Address.Street,
                    HouseNumber = responce.HumanInfo.Address.HouseNumber,
                    ApartmentNumber = responce.HumanInfo.Address.ApartmentNumber
                },
                Picture = responce.HumanInfo.Picture,
            };
            return userDto;
        }

       

        public async Task<bool> DeleteUser(int id)
        {
            var existingUser = await _dbRepository.GetUserByIdAsync(id);

            if (Validation.CheckIfNull(existingUser))
            {
                return false;
            }
            await _dbRepository.DeleteUser(existingUser);
            return true;
        }




        public Task<byte[]> ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            using var ms = new MemoryStream();
            destImage.Save(ms, ImageFormat.Png);

            return Task.FromResult(ms.ToArray());
        }

        public async Task<byte[]> FileUpload(IFormFile file, int width, int height)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            var memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);
            var img = Image.FromStream(memoryStream);
            var imageResized = await ResizeImage(img, width, height);

            return imageResized;

        }
    }
}
