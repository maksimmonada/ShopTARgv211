using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Models.Spaceship;
using ShopTARgv21.Core.Dto;
using ShopTARgv21.Core.ServiceInterface;
using ShopTARgv21.Data;
using System.Runtime.Intrinsics.X86;

namespace Shop.Controllers
{
    public class SpaceshipController : Controller
    {
        private readonly ShopDbContext _context;
        private readonly ISpaceshipServices _spaceshipServices;


        public SpaceshipController
            (
            ShopDbContext context,
            ISpaceshipServices spaceshipServices


            )
        {
            _context = context;
            _spaceshipServices = spaceshipServices;
        }
        public IActionResult Index()
        {
            var result = _context.Spaceship
                .OrderByDescending(y => y.CreatedAt)
                .Select(x => new SpaceshipListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ModelType = x.ModelType,
                    Passengers = x.Passengers,
                    BuildOfDate = x.BuildOfDate,
                    LaunchDate = x.LaunchDate
                });

        
            return View(result);
        }

        [HttpGet]

        public IActionResult Create()
        {
            SpaceshipEditViewModel spaceship = new SpaceshipEditViewModel();    

            return View("CreateUpdate", spaceship);
        }

        [HttpPost]

        public async Task<IActionResult> Create(SpaceshipViewModel vm)
        {
            var dto = new SpaceshipDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                ModelType = vm.ModelType,
                SpaceshipBuider = vm.SpaceshipBuider,
                PlaceOfBuild = vm.PlaceOfBuild,
                EnginePower = vm.EnginePower,
                LiftUpToSpaceByTonn = vm.LiftUpToSpaceByTonn,
                Crew = vm.Crew,
                Passengers = vm.Passengers,
                LaunchDate = vm.LaunchDate,
                BuildOfDate = vm.BuildOfDate,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    SpaceshipId = x.SpaceshipId,

                }).ToArray()

            };

            var result = await _spaceshipServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var spaceship = await _spaceshipServices.GetAsync(id);
            if (spaceship == null)
            {
                return NotFound();
            }

            var photos = await _context.FileToDatabase
                .Where(x => x.SpaceshipId == id)
                .Select(y => new ImageViewModel
                {
                    ImageData = y.ImageData,
                    ImageId = y.Id,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData)),
                    ImageTitle = y.ImageTitle,
                    SpaceshipId = y.Id
                }).ToArrayAsync();

            var wm = new SpaceshipEditViewModel();
                wm.Id = spaceship.Id;
                wm.Name = spaceship.Name;
                wm.ModelType = spaceship.ModelType;
                wm.SpaceshipBuider = spaceship.SpaceshipBuider;
                wm.PlaceOfBuild = spaceship.PlaceOfBuild;
                wm.EnginePower = spaceship.EnginePower;
                wm.LiftUpToSpaceByTonn = spaceship.LiftUpToSpaceByTonn;
                wm.Crew = spaceship.Crew;
                wm.Passengers = spaceship.Passengers;
                wm.LaunchDate = spaceship.LaunchDate;
                wm.BuildOfDate = spaceship.BuildOfDate;
                wm.CreatedAt = spaceship.CreatedAt;
                wm.ModifiedAt = spaceship.ModifiedAt;
                wm.Image.AddRange(photos);

            return View("CreateUpdate", wm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SpaceshipEditViewModel vm)
        {
            var dto = new SpaceshipDto()
            {
                Id=vm.Id,
                Name=vm.Name,
                ModelType=vm.ModelType,
                SpaceshipBuider=vm.SpaceshipBuider,
                PlaceOfBuild=vm.PlaceOfBuild,
                EnginePower=vm.EnginePower, 
                LiftUpToSpaceByTonn=vm.LiftUpToSpaceByTonn,
                Crew=vm.Crew,
                Passengers=vm.Passengers,
                LaunchDate=vm.LaunchDate,
                BuildOfDate=vm.BuildOfDate,
                CreatedAt=vm.CreatedAt,
                ModifiedAt=vm.ModifiedAt
            };

            var result = await _spaceshipServices.Update(dto);

            if(result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
           
                var spaceship = await _spaceshipServices.GetAsync(id);
                if (spaceship == null)
                {
                    return NotFound();
                }

                var wm = new SpaceshipViewModel()
                {
                    Id = spaceship.Id,
                    Name = spaceship.Name,
                    ModelType = spaceship.ModelType,
                    SpaceshipBuider = spaceship.SpaceshipBuider,
                    PlaceOfBuild = spaceship.PlaceOfBuild,
                    EnginePower = spaceship.EnginePower,
                    LiftUpToSpaceByTonn = spaceship.LiftUpToSpaceByTonn,
                    Crew = spaceship.Crew,
                    Passengers = spaceship.Passengers,
                    LaunchDate = spaceship.LaunchDate,
                    BuildOfDate = spaceship.BuildOfDate,
                    CreatedAt = spaceship.CreatedAt,
                    ModifiedAt = spaceship.ModifiedAt,

                };

                return View(wm);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var product = await _spaceshipServices.Delete(id);
            if(product == null)
            {
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index));
        }
    }
}
