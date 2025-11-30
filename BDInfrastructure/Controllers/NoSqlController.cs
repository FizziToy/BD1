using BDInfrastructure.Models;
using BDInfrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;

namespace BDInfrastructure.Controllers
{
    public class NoSqlController : Controller
    {
        private readonly MongoServiceRequestRepository _mongoRepo;
        private readonly SqlServiceRequestRepository _sqlRepo;

        public NoSqlController(MongoServiceRequestRepository mongoRepo, SqlServiceRequestRepository sqlRepo)
        {
            _mongoRepo = mongoRepo;
            _sqlRepo = sqlRepo;
        }

        public IActionResult Index()
        {
            var data = _mongoRepo.GetAll();
            return View(data);
        }

        public IActionResult AddTest()
        {
            var doc = new ServiceRequestDocument
            {
                RequestId = 2001,
                UnitId = 3,
                TenantId = 1,
                Title = "Тестова заявка з .NET",
                Description = "Створено автоматично",
                Priority = "Medium",
                Status = "Open",
                CreatedAt = DateTime.UtcNow,
                History = new() { "Створено: " + DateTime.UtcNow.ToString() }
            };

            _mongoRepo.Insert(doc);

            return RedirectToAction("Index");
        }

        public IActionResult SpeedTest(int runs = 100)
        {
            var results = new SpeedTestResult();
            var sw = new Stopwatch();

            sw.Start();
            for (int i = 0; i < runs; i++)
            {
                _sqlRepo.GetRequestsWithHistory();
            }
            sw.Stop();
            results.SqlTimeMs = sw.Elapsed.TotalMilliseconds / runs;

            // Виконуємо запит ще раз, щоб отримати Count (без вимірювання часу)
            results.SqlCount = _sqlRepo.GetRequestsWithHistory().Count();

            sw.Restart();
            for (int i = 0; i < runs; i++)
            {
                _mongoRepo.GetAll();
            }
            sw.Stop();
            results.MongoTimeMs = sw.Elapsed.TotalMilliseconds / runs;
            results.MongoCount = _mongoRepo.GetAll().Count();

            results.Runs = runs;

            return View(results);
        }
    }
}