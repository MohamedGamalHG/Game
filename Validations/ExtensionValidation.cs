namespace firstAppAsp.Validations
{
    public class ExtensionValidation : ValidationAttribute
    {
        private readonly string _allowedExentions;
        public ExtensionValidation(string allowedExentions)
        {
            _allowedExentions = allowedExentions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if(file is not null)
            {
                var extension = Path.GetExtension(file.FileName);
                var isAllowed = _allowedExentions.Split(',').Contains(extension,StringComparer.OrdinalIgnoreCase);
                if(!isAllowed )
                {
                    return new ValidationResult($"{_allowedExentions} are allowed");
                }
            }

            return ValidationResult.Success;
        }
    }
}
