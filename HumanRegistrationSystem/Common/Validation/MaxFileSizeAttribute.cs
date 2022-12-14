using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Common.Validation;

public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;

    public MaxFileSizeAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IEnumerable<IFormFile> filesList)
            foreach (var file in filesList)
                if (file.Length > _maxFileSize)
                    return new ValidationResult($"Maximum allowed file size is {_maxFileSize} bytes.");

        return ValidationResult.Success;
    }
}