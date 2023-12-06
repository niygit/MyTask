using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTasks.Data;
using MyTasks.Models;
using MyTasks.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTasks.Controllers
{
	public class DirectorController : Controller
	{
        private readonly AppDbContext _context;

        public DirectorController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
		{
            List<Director> director = await _context.Directors.ToListAsync();
			return View(director);
		}
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDirectorVM model)
        {
            Director director = new Director()
            {
                Name = model.Name,
                Surname = model.Surname,
                Age = model.Age
            };
            await _context.Directors.AddAsync(director);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Director director = await _context.Directors.FirstOrDefaultAsync(m => m.Id == id);
            _context.Remove(director);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            Director director= await _context.Directors.FirstOrDefaultAsync(m => m.Id == id);
            UpdateDirectorVM updateDirectorVM = new UpdateDirectorVM()
            {
                Id = id,
                Name = director.Name,
                Surname= director.Surname,
                Age= director.Age

            };
            return View(updateDirectorVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateDirectorVM model)
        {
            Director director=await _context.Directors.FirstOrDefaultAsync(m => m.Id == model.Id);
            director.Name= model.Name;
            director.Surname= model.Surname;
            director.Age= model.Age;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
	}
}
