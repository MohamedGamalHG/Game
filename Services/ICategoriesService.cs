namespace firstAppAsp.Services;

public interface ICategoriesService
{
    IEnumerable<SelectListItem> GetSelectList();
}