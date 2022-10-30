﻿using Common.Validation;
using Microsoft.AspNetCore.Http;

namespace HumanRegistrationSystem.Dto
{
    public class ImageUploadRequest
    {
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new[] { ".png", ".jpg" })]
        public List<IFormFile> Images { get; set; }

        public string NoteContent { get; set; }
        public int Number { get; set; }
    }
}
