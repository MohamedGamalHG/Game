using firstAppAsp.Services;
using firstAppAsp.ViewModel;
using firstAppAsp.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers;
public class GamesController : Controller
{
    private readonly ICategoriesService _categoriesService;
    private readonly IDevicesServices _devicesService;
              
    private readonly IGamesServices _gamesService;

    public GamesController(ICategoriesService categoriesService, 
        IDevicesServices devicesService, 
        IGamesServices gamesService)
    {
        _categoriesService = categoriesService;
        _devicesService = devicesService;
        _gamesService = gamesService;
    }

    public IActionResult Index()
    {
        var games = _gamesService.GetAll();
        return View(games);
    }

    public IActionResult Details(int id)
    {
        var game = _gamesService.GetById(id);

        if(game is null)
            return NotFound();

        return View(game);
    }

    [HttpGet]
    public IActionResult Create()
    {
        CreateGameFormViewModel viewModel = new()
        {
            Categories = _categoriesService.GetSelectList(),
            Devices = _devicesService.GetSelectList()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateGameFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categories = _categoriesService.GetSelectList();
            model.Devices = _devicesService.GetSelectList();
            return View(model);
        }

        await _gamesService.Create(model);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id) 
    {
        var game = _gamesService.Edit(id);
        if (game is null)
            return NotFound();
        EditGameFormViewModel editGame = new EditGameFormViewModel();
        editGame.Name = game.Name;
        editGame.Id = id;   
        editGame.Description = game.Description;
        editGame.Categories = _categoriesService.GetSelectList();
        editGame.SelectedDevices = game.Devices.Select( c=>c.DeviceId).ToList();
        editGame.Devices = _devicesService.GetSelectList();
        editGame.CurrentCover = game.Cover;
        return View(editGame);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(EditGameFormViewModel model,int id)
    {
        if (!ModelState.IsValid)
        {
            model.Categories = _categoriesService.GetSelectList();
            model.Devices = _devicesService.GetSelectList();
            return View(model);
        }

        var game = await _gamesService.Update(model,id);
        if (game is null)
            return BadRequest();

        return RedirectToAction(nameof(Index));
    }


    //[HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete( int id)
    {

        var isDeleted = _gamesService.Delete(id);

        return isDeleted ? Ok() : BadRequest();
    }



}