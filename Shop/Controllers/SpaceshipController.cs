using Microsoft.AspNetCore.Mvc;
using Shop.Models.Spaceship;
using ShopTARgv21.Data;

namespace Shop.Controllers
{
    public class SpaceshipController : Controller
    {
        private readonly ShopDbContext _context;

        public SpaceshipController
            (
            ShopDbContext context
            )
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var result = _contextSpaceship
                .OrderByDescending(y => y.CreatedAt)
                .Select(XmlConfigurationExtensions => new SpaceshipListViewModel
                {
                    Name = x.Name;
            ModelType = x.ModelType;
            Passenger = x.Passenger;
            BuidOfDate = x.BuidOfDate;
            LaunchDate = x.LaunchDate
                })

        {
            return View();
        }
    }
}
