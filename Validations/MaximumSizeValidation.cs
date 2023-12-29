namespace firstAppAsp.Validations
{
    public class MaximumSizeValidation : ValidationAttribute
    {
        private readonly int _maximumSize;
        public MaximumSizeValidation(int maxSize)
        {
            _maximumSize = maxSize;
        }


        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > _maximumSize)
                    return new ValidationResult($"Maximum size is {_maximumSize}");
            }
            return ValidationResult.Success;
        }

    }
}
