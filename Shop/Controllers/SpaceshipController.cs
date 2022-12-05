using Microsoft.AspNetCore.Mvc;
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
                    Name = x.Name,
                    ModelType = x.ModelType,
                    Passengers = x.Passengers,
                    BuildOfDate = x.BuildOfDate,
                    LaunchDate = x.LaunchDate
                });

        
            return View(result);
        }

        [HttpGet]

        public IActionResult Add()
        {
            SpaceshipViewModel spaceship = new SpaceshipViewModel();    

            return View("Edit");
        }

        [HttpPost]

        public async Task<IActionResult> Add(SpaceshipViewModel vm)
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

            };

            var result = await _spaceshipServices.Add(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }
    }
}
