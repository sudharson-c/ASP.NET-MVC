using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskContext _context;

        public TaskController(TaskContext context)
        {
            _context = context;
        }

        // READ - List all tasks
        public async Task<IActionResult> Index()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return View(tasks);
        }

        // CREATE - Display form
        public IActionResult Create()
        {
            return View();
        }

        // CREATE - Add new task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // UPDATE - Display edit form
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        // UPDATE - Save edited task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.Task task)
        {
            if (id != task.ID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.ID))
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
            return View(task);
        }



        // DELETE - Confirm delete
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        // DELETE - Confirm deletion
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.ID == id);
        }
        

    }
}
