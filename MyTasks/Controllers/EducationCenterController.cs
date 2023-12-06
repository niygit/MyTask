using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTasks.Data;
using MyTasks.Models;
using MyTasks.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTasks.Controllers
{
	public class EducationCenterController : Controller
	{
        private readonly AppDbContext _context;

        public EducationCenterController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
		{
            List<School> schools = await _context.Schools.ToListAsync();
            List<Class> classes = await _context.Classes.ToListAsync();
            List<Student> students = await _context.Students.ToListAsync();
            List<Teacher> teachers = await _context.Teachers.ToListAsync();
            List<Director> director = await _context.Directors.ToListAsync();

            EducationCenterVM educationCenterVM=new EducationCenterVM()
            {
                Schools=schools,
                Classes=classes,
                Students=students,
                Teachers=teachers,
                Director=director
            };

			return View(educationCenterVM);
		}
	}
}
