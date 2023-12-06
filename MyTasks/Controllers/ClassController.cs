using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTasks.Data;
using MyTasks.Models;
using MyTasks.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTasks.Controllers
{
    public class ClassController : Controller
    {

        private readonly AppDbContext _context;

        public ClassController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Class> classes = await _context.Classes.ToListAsync();
            return View(classes);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateClassVM model)
        {
            Class cas = new Class()
            {
                ClassName=model.ClassName,
                ClassTeacher=model.ClassTeacher,
            };
            await _context.AddAsync(cas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Class cas = await _context.Classes.FirstOrDefaultAsync(m => m.Id == id);
             _context.Remove(cas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            Class cas = await _context.Classes.FirstOrDefaultAsync(m => m.Id == id);
            UpdateClassVM updateClassVM=new UpdateClassVM()
            {
                Id=cas.Id,
                ClassName=cas.ClassName,
                ClassTeacher=cas.ClassTeacher,
            };
            return View(updateClassVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateClassVM model)
        {
            Class cas = await _context.Classes.FirstOrDefaultAsync(m => m.Id == model.Id);
            cas.ClassName = model.ClassName;
            cas.ClassTeacher = model.ClassTeacher;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

	}
}
