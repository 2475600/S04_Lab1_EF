using Microsoft.AspNetCore.Mvc;
using ZombieParty.Models;
using ZombieParty.ViewModels;

namespace ZombieParty.Controllers
{
    public class WeaponController : Controller
    {
        private BaseDonnees _baseDonnees { get; set; }

        public WeaponController(BaseDonnees baseDonnees)
        {
            _baseDonnees = baseDonnees;
        }

        public IActionResult Index()
        {
            List<Weapon> weapons = _baseDonnees.Weapons.ToList();
            return View(weapons);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Weapon weapon)
        {
            if (ModelState.IsValid)
            {
                // Ajouter à la BD
                _baseDonnees.Weapons.Add(weapon);
                TempData["Success"] = $"{weapon.Name} weapon added";
                return this.RedirectToAction("Index");
            }

            return this.View(weapon);
        }
    }
}
