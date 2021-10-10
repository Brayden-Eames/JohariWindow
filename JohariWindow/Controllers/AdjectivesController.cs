using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JohariWindow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdjectivesController : Controller
    {
        private readonly IUnitOfWork _unitofWork;
        public AdjectivesController(IUnitOfWork unitofWork)
            => _unitofWork = unitofWork;

        [HttpPost]
        public IActionResult Update([FromBody] string request)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
