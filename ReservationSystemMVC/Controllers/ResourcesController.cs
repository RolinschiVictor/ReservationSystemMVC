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

        public IActionResult Details(Guid id)
        {
            var resource = _resourceRepository.GetById(id);
            if (resource == null)
            {
                return NotFound();
            }
            return View(resource);
        }
    }
}
