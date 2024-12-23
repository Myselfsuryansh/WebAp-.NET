using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.DataBaseConfiguration;
using WebApp.Models;

namespace WebApp.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class MiniaturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MiniaturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Miniatures
        public async Task<IActionResult> Index()
        {
            return View(await _context.Miniature.ToListAsync());
        }

        // GET: Miniatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miniature = await _context.Miniature
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (miniature == null)
            {
                return NotFound();
            }

            return View(miniature);
        }

        // GET: Miniatures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Miniatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Guid,Name,TableReady,Description")] Miniature miniature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(miniature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(miniature);
        }

        // GET: Miniatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miniature = await _context.Miniature.FindAsync(id);
            if (miniature == null)
            {
                return NotFound();
            }
            return View(miniature);
        }

        // POST: Miniatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Guid,Name,TableReady,Description")] Miniature miniature)
        {
            if (id != miniature.Guid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(miniature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MiniatureExists(miniature.Guid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(miniature);
        }

        // GET: Miniatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miniature = await _context.Miniature
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (miniature == null)
            {
                return NotFound();
            }

            return View(miniature);
        }

        // POST: Miniatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var miniature = await _context.Miniature.FindAsync(id);
            if (miniature != null)
            {
                _context.Miniature.Remove(miniature);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MiniatureExists(int id)
        {
            return _context.Miniature.Any(e => e.Guid == id);
        }
    }
}
