using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZombieParty.Models;
using ZombieParty.ViewModels;

namespace ZombieParty.Controllers
{
    public class ZombieController : Controller
    {
        private BaseDonnees _baseDonnees { get; set; }

        public ZombieController(BaseDonnees baseDonnees)
        {
            _baseDonnees = baseDonnees;
        }

        public IActionResult Index()
        {

            List<Zombie> zombiesList = _baseDonnees.Zombies.ToList();
            return View(zombiesList);
        }

        public IActionResult Create()
        {
            ZombieVM zombieVM = new ZombieVM();

            zombieVM.ZombieTypeSelectList = new SelectList(
                _baseDonnees.ZombieTypes.ToList(),
                "Id",
                "TypeName"
            );

            return View(zombieVM);
        }

        [HttpPost]
        public IActionResult Create(ZombieVM zombieVM)
        {
            if (ModelState.IsValid)
            {
                _baseDonnees.Zombies.Add(zombieVM.Zombie);
                _baseDonnees.SaveChanges();

                TempData["Success"] = $"Zombie {zombieVM.Zombie.Name} added";

                return RedirectToAction("Index");
            }

            ZombieType selectedZombieType = _baseDonnees.ZombieTypes
                .Where(zt => zt.Id == zombieVM.Zombie.ZombieTypeId)
                .SingleOrDefault();

            zombieVM.Zombie.ZombieType = selectedZombieType;

            zombieVM.ZombieTypeSelectList = new SelectList(
                _baseDonnees.ZombieTypes.ToList(),
                "Id",
                "TypeName"
            );

            return View(zombieVM);
        }


    }
}
