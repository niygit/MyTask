using MyTasks.Models;
using System.Collections.Generic;

namespace MyTasks.ViewModels
{
	public class EducationCenterVM
	{
		public List<School> Schools { get; set; }
		public List<Class> Classes { get; set; }
		public List<Student> Students { get; set; }
		public List<Teacher> Teachers { get; set; }
		public List<Director> Director { get; set; }

	}
}
