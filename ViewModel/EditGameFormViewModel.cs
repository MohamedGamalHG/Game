using firstAppAsp.Validations;
using firstAppAsp.ViewModels;

namespace firstAppAsp.ViewModel
{
    public class EditGameFormViewModel : GameFormViewModel
    {
        public int Id { get; set; }

        public string? CurrentCover { get; set; }

        [ExtensionValidation(FileSettings.AllowExentions),
            MaximumSizeValidation(FileSettings.MaxFileSizeInByte)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
