using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet_Binome.Data;
using Projet_Binome.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Projet_Binome.Controllers
{
    [Authorize]
    public class CertificatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CertificatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Display(int id)
        {
            var certificate = await _context.Certificates
                .Include(c => c.Course)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (certificate == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null || certificate.UserId != userId)
            {
                return Forbid();
            }

            return View(certificate);
        }
    }
}
