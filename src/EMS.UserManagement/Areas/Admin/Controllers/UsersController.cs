using System;
using System.Linq;
using System.Threading.Tasks;
using EMS.CachingDbContext;
using EMS.CachingDbContext.Models;
using EMS.Contracts.DataAccess;
using EMS.UserManagement.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EMS.UserManagement.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class UsersController : Controller
    {
        private readonly IDataSource _dataSource;
        private readonly IDataTarget _dataTarget;

        public UsersController(IDataSource dataSource, IDataTarget dataTarget)
        {
            _dataSource = dataSource;
            _dataTarget = dataTarget;
        }

        public async Task<IActionResult> Index() => 
            View(
                await _dataSource.Entities<User>()
                    .Include(x => x.Manager)
                    .ToListAsync());

        public IActionResult Create()
        {
            ViewBag.Users = _dataSource.Entities<User>()
                .Select(x => new SelectListItem(x.FullName, x.Id.ToString()));
            return View();
        }

        public async Task<IActionResult> Add([FromForm] AddUserModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var managerId = Guid.Parse(model.ManagerId);

            await _dataTarget.ProvisionAsync(new User
            {
                EmailAddress = model.EmailAddress,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName,
                Manager = await _dataSource.Entities<User>()
                    .SingleOrDefaultAsync(x => x.Id == managerId)
            });

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(Guid id)
        {
            await _dataTarget.DeprovisionAsync(await _dataSource.Entities<User>().SingleAsync(x => x.Id == id));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid id) =>
            View(
                await _dataSource.Entities<User>()
                    .Include(x => x.Manager)
                    .Include(x => x.Manages)
                    .SingleAsync(x => x.Id == id));

//            View(await _context.Users.Include(x => x.Manager).Include(x => x.Manages).SingleAsync(x => x.Id == id));
    }
}