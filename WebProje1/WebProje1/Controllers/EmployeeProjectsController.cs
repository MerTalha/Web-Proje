using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProje1.Data;
using WebProje1.Models;

namespace WebProje1.Controllers
{
    public class EmployeeProjectsController : Controller
    {
        private readonly WebDbContext _context;

        public EmployeeProjectsController(WebDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeProjects
        public async Task<IActionResult> Index()
        {
            var webDbContext = _context.EmployeesProjects.Include(e => e.Employee).Include(e => e.Project);
            return View(await webDbContext.ToListAsync());
        }

        // GET: EmployeeProjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmployeesProjects == null)
            {
                return NotFound();
            }

            var employeeProject = await _context.EmployeesProjects
                .Include(e => e.Employee)
                .Include(e => e.Project)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employeeProject == null)
            {
                return NotFound();
            }

            return View(employeeProject);
        }

        // GET: EmployeeProjects/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId");
            return View();
        }

        // POST: EmployeeProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,EmployeeId")] EmployeeProject employeeProject)
        {
            
                _context.Add(employeeProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", employeeProject.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", employeeProject.ProjectId);
        }

        // GET: EmployeeProjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmployeesProjects == null)
            {
                return NotFound();
            }

            var employeeProject = await _context.EmployeesProjects.FindAsync(id);
            if (employeeProject == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", employeeProject.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", employeeProject.ProjectId);
            return View(employeeProject);
        }

        // POST: EmployeeProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,EmployeeId")] EmployeeProject employeeProject)
        {
            if (id != employeeProject.EmployeeId)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(employeeProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeProjectExists(employeeProject.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", employeeProject.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", employeeProject.ProjectId);       
        }

        // GET: EmployeeProjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmployeesProjects == null)
            {
                return NotFound();
            }

            var employeeProject = await _context.EmployeesProjects
                .Include(e => e.Employee)
                .Include(e => e.Project)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employeeProject == null)
            {
                return NotFound();
            }

            return View(employeeProject);
        }

        // POST: EmployeeProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmployeesProjects == null)
            {
                return Problem("Entity set 'WebDbContext.EmployeesProjects'  is null.");
            }
            var employeeProject = await _context.EmployeesProjects.FindAsync(id);
            if (employeeProject != null)
            {
                _context.EmployeesProjects.Remove(employeeProject);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeProjectExists(int id)
        {
          return (_context.EmployeesProjects?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
