using NUnit.Framework;
using System.Linq;
using VolvoTrucks.Controllers;
using VolvoTrucks.Models;

namespace Tests
{
    public class Tests
    {
        private TruckController _controller;
        private VolvoTrucksContext _context;

        [SetUp]
        public void Setup()
        {
            _context = new VolvoTrucksContext();
            _controller = new TruckController();
        }

        [Test, Order(1)]
        public void InsertTest()
        {
            var truck = new Trucks
            {
                Chassis = "ABC123",
                MaximumLoad = 3000,
                ModelId = 1
            };

            _controller.New(truck);
            Assert.Pass();
        }

        [Test, Order(2)]
        public void EditTest()
        {
            var oldTruck = _context.Trucks.Where(x => x.Chassis == "ABC123").OrderByDescending(x => x.Id).FirstOrDefault();
            if (oldTruck == null)
            {
                Assert.Fail();
                return;
            }

            var oldMaximunLoad = oldTruck.MaximumLoad;

            oldTruck.MaximumLoad = oldMaximunLoad + 500;

            _controller.Edit(oldTruck);

            var editedTruck = _context.Trucks.Find(oldTruck.Id);

            if(editedTruck.MaximumLoad != (oldMaximunLoad + 500))
            {
                Assert.Fail();
                return;
            }

            Assert.Pass();
        }

        [Test, Order(3)]
        public void Delete()
        {
            var oldTrucks = _context.Trucks.Where(x => x.Chassis == "ABC123").OrderByDescending(x => x.Id).ToList();
            if (oldTrucks == null)
            {
                Assert.Fail();
            }

            oldTrucks.ForEach(x =>
            {
                _context.Entry(x).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
            });

            Assert.Pass();
        }
    }
}