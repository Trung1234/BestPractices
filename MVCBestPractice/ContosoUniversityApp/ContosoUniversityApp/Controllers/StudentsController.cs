﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversityApp.Data;
using ContosoUniversityApp.Models;
using ContosoUniversityApp.Services;

namespace ContosoUniversityApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;
        private readonly IStudentService _service;

        public StudentsController(IStudentService service, SchoolContext context)
        {
            _context = context;
            _service = service;
        }

        // GET: Students
        //public async Task<IActionResult> Index(string searchString)
        //{
        //    ViewData["CurrentFilter"] = searchString;

        //    var students = from s in _context.Students
        //                   select s;
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        students = students.Where(s => s.LastName.Contains(searchString)
        //                               || s.FirstMidName.Contains(searchString));
        //    }

        //    return View(await students.AsNoTracking().ToListAsync());
        //}

        public async Task<IActionResult> Index(string searchString ,int? page = 0)
        {
            int limit = 4;
            int start;
            if (page == 0)
            {
                page = 1;
            }
            _service.FilterList(searchString);
            start = (int)(page - 1) * limit;

            ViewBag.pageCurrent = page;

            int totalProduct = _service.TotalStudent();

            ViewBag.totalProduct = totalProduct;

            ViewBag.numberPage = _service.NumberPage(totalProduct, limit);
            var students = await _service.PaginationStudent(start, limit);
            return View(students);
        }
     


    /// <summary>
    /// Detail of Student
    /// GET: Students/Details/5
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            //query Enrollments and  Course related to Student
            var student = await _context.Students.Include(s=>s.Enrollments)
                .ThenInclude(e=>e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            //Home Error
            //AsNoTracking method improves performance in scenarios 
            ////where the entities returned won't be updated in the current context's lifetime.
            //if (student == null)
            //{
            //    return RedirectToAction("Error" , "Home");
            //}
            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            if (id != student.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ID))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}
