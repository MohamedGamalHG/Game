using firstAppAsp.Models;
using firstAppAsp.ViewModel;

namespace firstAppAsp.Services
{
    public interface IGamesServices
    {
        IEnumerable<Game> GetAll();
        Game? GetById(int id);
        Task<Game?> Update(EditGameFormViewModel model,int id);
        Game Edit(int id);
       bool Delete(int id);
        Task Create(CreateGameFormViewModel model);
    }
}
