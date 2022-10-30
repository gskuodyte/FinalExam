using HumanRegistrationSystem_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanRegistrationSystem_BL
{
    public interface IImagesService
    {
        Task<Image> AddImageAsync(byte[] imageBytes, string contentType);
        Task<Image> GetImageAsync(int id);
    }
}
