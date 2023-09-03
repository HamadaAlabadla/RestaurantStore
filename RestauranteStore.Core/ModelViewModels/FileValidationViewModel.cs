using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RestaurantStore.Core.ModelViewModels
{
    public class FileValidationViewModel
    {
        [Required(ErrorMessage = "Please select an Excel file.")]
        [AllowedExtensions(new string[] { ".xlsx" }, ErrorMessage = "Only Excel files (.xlsx) are allowed.")]
        public IFormFile ExcelFile { get; set; }
    }

    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = (value as IFormFile)!;

            if (file != null)
            {
                var extension = System.IO.Path.GetExtension(file.Name);

                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
