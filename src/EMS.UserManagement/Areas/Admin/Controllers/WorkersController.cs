using System;
using System.Linq;
using System.Threading.Tasks;
using EMS.ConfigurationDbContext;
using EMS.Models.Configuration;
using EMS.UserManagement.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMS.UserManagement.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class WorkersController : Controller
    {
        private readonly ConfigurationContext _context;

        public WorkersController(ConfigurationContext context)
        {
            _context = context;
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddWorkerModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _context.Workers.AddAsync(new WorkerConfiguration
            {
                WorkerName = model.Name,
                ApiKey = Guid.NewGuid().ToString()
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        
//        public async Task<IActionResult> Edit(Guid id) => View(await _context.Workers.SingleAsync(x => x.Id == id));
        
        public IActionResult Index() => View(_context.Workers.ToList());

        public async Task<IActionResult> Remove(Guid id)
        {
            _context.Workers.Remove(await _context.Workers.SingleAsync(x => x.Id == id));
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}