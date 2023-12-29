using firstAppAsp.Models;
using firstAppAsp.Settings;
using firstAppAsp.ViewModel;

namespace firstAppAsp.Services
{
    public class GamesServices : IGamesServices
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _environment;
        private string _imagePath;
        public GamesServices(ApplicationDbContext context,IWebHostEnvironment webHostEnvironment) 
        { _context = context;
            _environment = webHostEnvironment;
            _imagePath = $"{_environment.WebRootPath}{FileSettings.ImagePath}";
        }
        public async Task Create(CreateGameFormViewModel viewModel)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(viewModel.Cover.FileName)}";
            // we use function combine to save the image and the path of image
            var path = Path.Combine(_imagePath, coverName);

            using var stream = File.Create(path);
            await viewModel.Cover.CopyToAsync(stream);
          //  stream.Dispose(); not important to use is


            Game game = new()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                CategoryId = viewModel.CategoryId,
                // here will take the game_id automatically because we are in game model so will take the game_id and assign
                // it to game_id in the entity of GameDevice like GameDevice{Device_id = d.device_id,Game_id = d.game_id}
                Devices = viewModel.SelectedDevices.Select(d => new GameDevice { DeviceId = d}).ToList(),
                Cover = coverName
            };

            _context.Add(game);
            _context.SaveChanges();

        }

        public Game Edit(int id)
        {
            return _context.Games.Include( c => c.Category).Include(d => d.Devices).ThenInclude(dd => dd.Device).SingleOrDefault(d => d.Id == id); 

        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .ToList();
        }

        public Game? GetById(int id)
        {
            return _context.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
        }

        public async Task<Game?> Update(EditGameFormViewModel model,int id)
        {
            Game game = _context.Games.Include(g => g.Devices).SingleOrDefault(i => i.Id == id);
            if (game is null)
                return null;
            var hasCover = model.Cover is not null;
            var oldCover = game.Cover;
            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(m => new GameDevice { DeviceId= m}).ToList();

            if(hasCover)
                game.Cover = await SaveCoverName(model.Cover!);

            // SaveChanges() => return number of row affect in DB
            var affected = _context.SaveChanges();
            if(affected > 0)
            {
                if(hasCover)
                {
                    var cover = Path.Combine(_imagePath, oldCover);
                    File.Delete(cover);
                }

                return game;
            }
            else
            {
                var cover = Path.Combine(_imagePath, game.Cover);
                File.Delete(cover);
                return null;
            }

        }

        public bool Delete(int id)
        {
            bool isDeleted = false;
            Game game = _context.Games.Include(d => d.Devices).SingleOrDefault(i => i.Id == id);
            if(game is null) return isDeleted;
           
            _context.Remove(game);
            var affected = _context.SaveChanges();
            if(affected > 0) 
            {
                isDeleted = true;
                var cover = Path.Combine(_imagePath, game.Cover);
                File.Delete(cover);
            }

            return isDeleted;
        }

        private async Task<string> SaveCoverName(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            // we use function combine to save the image and the path of image
            var path = Path.Combine(_imagePath, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);
            //  stream.Dispose(); not important to use is

            return coverName;
        }


    }
}
