using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MyTasks.Data;
using MyTasks.Models;
using MyTasks.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTasks.Controllers
{
    public class SchoolController : Controller
    {
        private readonly AppDbContext _context;

        public SchoolController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<School> school = await _context.Schools.ToListAsync();
            return View(school);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSchoolVM model)
        {
            School school = new School()
            {
                Class=model.Class,
                Student=model.Student,
                Teacher=model.Teacher,
                Director=model.Director,
            };
           await _context.Schools.AddAsync(school);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            School school=await _context.Schools.FirstOrDefaultAsync(m=>m.Id==id);
            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            School school = await _context.Schools.FirstOrDefaultAsync(m => m.Id == id);
            UpdateSchoolVM model = new UpdateSchoolVM()
            {
                Id = school.Id,
                Class=school.Class,
                Student=school.Student,
                Teacher=school.Teacher,
                Director=school.Director
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateSchoolVM model)
        {
            School school = await _context.Schools.FirstOrDefaultAsync(m => m.Id == model.Id);
            school.Class=model.Class;
            school.Student=model.Student;
            school.Director=model.Director;
            school.Teacher=model.Teacher;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
