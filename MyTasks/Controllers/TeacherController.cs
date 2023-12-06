using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTasks.Data;
using MyTasks.Models;
using MyTasks.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTasks.Controllers
{
	public class TeacherController : Controller
	{
        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
		{
            List<Teacher> teachers=await _context.Teachers.ToListAsync();
			return View(teachers);
		}

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTeacherVM model)
        {
            Teacher teacher = new Teacher()
            {
                Name = model.Name,
                Surname = model.Surname,
                ClassName = model.ClassName,
            };
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Teacher teacher=await _context.Teachers.FirstOrDefaultAsync(m=>m.Id==id);
            _context.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            Teacher teacher=await _context.Teachers.FirstOrDefaultAsync(m=>m.Id==id);
            UpdateTeacherVM updateTeacherVM = new UpdateTeacherVM()
            {
                Id = id,
                Name = teacher.Name,
                Surname = teacher.Surname,
                ClassName = teacher.ClassName,
            };
            return View(updateTeacherVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateTeacherVM model)
        {
            Teacher teacher = await _context.Teachers.FirstOrDefaultAsync(m => m.Id == model.Id);
            teacher.Name=model.Name;
            teacher.Surname=model.Surname;
            teacher.ClassName=model.ClassName;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
	}
}
