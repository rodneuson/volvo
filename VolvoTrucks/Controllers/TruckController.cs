using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using VolvoTrucks.Models;

namespace VolvoTrucks.Controllers
{
    public class TruckController : Controller
    {
        private VolvoTrucksContext _context = new VolvoTrucksContext();

        public IActionResult Index()
        {
            var list = _context.Trucks.Include(t => t.Model).ToList();
            return View(list);
        }

        public IActionResult New()
        {
            return View(new Trucks());
        }

        [HttpPost]
        public IActionResult New(Trucks truck)
        {
            Validation(truck);

            if (ModelState.IsValid)
            {
                _context.Trucks.Add(truck);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(truck);
        }

        public IActionResult Edit(int id)
        {
            var truck = _context.Trucks.Find(id);
            return View(truck);
        }

        [HttpPost]
        public IActionResult Edit(Trucks truck)
        {
            Validation(truck);

            if (ModelState.IsValid)
            {
                _context.Entry(truck).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(truck);
        }

        public IActionResult Delete(int id)
        {
            var truck = _context.Trucks.Find(id);
            _context.Entry(truck).State = EntityState.Deleted;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private void Validation(Trucks truck)
        {
            if (string.IsNullOrEmpty(truck.Chassis))
            {
                ModelState.AddModelError("Chassis", "Please provide a valid chassis");
            }

            if (truck.MaximumLoad <= 0)
            {
                ModelState.AddModelError("MaximumLoad", "Please provide a valid weight");
            }
        }
    }
}