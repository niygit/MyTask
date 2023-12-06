using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTasks.Data;
using MyTasks.Migrations;
using MyTasks.Models;
using MyTasks.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTasks.Controllers
{
    public class StudentController : Controller
    {

        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Student> students = await _context.Students.ToListAsync();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentVM model)
        {
            Student student = new Student()
            {
                Name=model.Name,
                Surname=model.Surname,
                ClassName=model.ClassName,
                TeacherName=model.TeacherName,
            };
            await _context.AddAsync(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Student stu = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            _context.Remove(stu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            Student student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            UpdateStudentVM updateStudentVM = new UpdateStudentVM()
            {
                Id = id,
                Name = student.Name,
                Surname = student.Surname,
                ClassName = student.ClassName,
                TeacherName = student.TeacherName,
            };
            return View(updateStudentVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateStudentVM model)
        {
            Student student = await _context.Students.FirstOrDefaultAsync(m => m.Id == model.Id);
            student.Name=model.Name;
            student.Surname=model.Surname;
            student.ClassName=model.ClassName;
            student.TeacherName=model.TeacherName;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
