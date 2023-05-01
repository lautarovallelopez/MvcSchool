using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcSchool.Data;
using MvcSchool.Models;

namespace MvcSchool.Controllers
{
    public class PrincipalController : Controller
    {
        private readonly SchoolContext _context;

        public PrincipalController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Principal
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.Principal.Include(p => p.School);
            return View(await schoolContext.ToListAsync());
        }

        // GET: Principal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Principal == null)
            {
                return NotFound();
            }

            var principal = await _context.Principal
                .Include(p => p.School)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (principal == null)
            {
                return NotFound();
            }

            return View(principal);
        }

        // GET: Principal/Create
        public IActionResult Create()
        {
            ViewData["SchoolId"] = new SelectList(_context.School, "Id", "Id");
            return View();
        }

        // POST: Principal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,PhoneNumber,SchoolId")] Principal principal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(principal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SchoolId"] = new SelectList(_context.School, "Id", "Id", principal.SchoolId);
            return View(principal);
        }

        // GET: Principal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Principal == null)
            {
                return NotFound();
            }

            var principal = await _context.Principal.FindAsync(id);
            if (principal == null)
            {
                return NotFound();
            }
            ViewData["SchoolId"] = new SelectList(_context.School, "Id", "Id", principal.SchoolId);
            return View(principal);
        }

        // POST: Principal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,PhoneNumber,SchoolId")] Principal principal)
        {
            if (id != principal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(principal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrincipalExists(principal.Id))
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
            ViewData["SchoolId"] = new SelectList(_context.School, "Id", "Id", principal.SchoolId);
            return View(principal);
        }

        // GET: Principal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Principal == null)
            {
                return NotFound();
            }

            var principal = await _context.Principal
                .Include(p => p.School)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (principal == null)
            {
                return NotFound();
            }

            return View(principal);
        }

        // POST: Principal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Principal == null)
            {
                return Problem("Entity set 'SchoolContext.Principal'  is null.");
            }
            var principal = await _context.Principal.FindAsync(id);
            if (principal != null)
            {
                _context.Principal.Remove(principal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrincipalExists(int id)
        {
          return (_context.Principal?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
