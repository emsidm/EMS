using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.ConfigurationDbContext;
using EMS.Contracts.Workers;
using EMS.Models;
using EMS.Models.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMS.UserManagement.Areas.Api.Controllers
{
    [Area(nameof(Api))]
    public class ConfigController : Controller
    {
        private readonly ConfigurationContext _context;

        public ConfigController(ConfigurationContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index(Guid id)
        {
            return new JsonResult(await _context.Workers.SingleAsync(x => x.ApiKey == id.ToString()));
        }
    }
}