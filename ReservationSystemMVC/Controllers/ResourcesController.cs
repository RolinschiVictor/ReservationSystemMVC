using Microsoft.AspNetCore.Mvc;
using ReservationSystemMVC.Core.Abstractions.Repositories;

namespace ReservationSystemMVC.Controllers
{
    public class ResourcesController : Controller
    {
        private readonly IResourceRepository _resourceRepository;

        public ResourcesController(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public IActionResult Index()
        {
            var resources = _resourceRepository.GetAll();
            return View(resources);
        }
    }
}
