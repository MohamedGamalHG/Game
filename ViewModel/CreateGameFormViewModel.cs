using firstAppAsp.Settings;
using firstAppAsp.Validations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace firstAppAsp.ViewModel
{
    public class CreateGameFormViewModel
    {
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(2500)]
        public string Description { get; set; }
        [Display(Name="Category")]
        public int CategoryId { get; set; } 
        // we make this Enumerable to loop on it in the select list item in the view 
        // this is the approach use it to draw the select dropdown list in view 
        // we have more than one type to draw this like viewBag but this is the best one to draw list dropdown in view
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        [Display(Name = "Device")]

        public List<int> SelectedDevices { get; set; }  = new List<int>();

        public IEnumerable<SelectListItem> Devices { get; set;} =Enumerable.Empty<SelectListItem>();
        [ExtensionValidation(FileSettings.AllowExentions),MaximumSizeValidation(FileSettings.MaxFileSizeInByte)]
        public IFormFile Cover { get; set; }
        //public object Devices { get; internal set; }
    }
}
