using System.ComponentModel.DataAnnotations;

namespace Workloopz.ViewModels
{
	public class ProjectVM
	{
		[Display(Name = "Tên dự án")]
		public string Name { get; set; } = null!;

		public string? Owner { get; set; }
		[Display(Name = "Mô tả")]
		public string? Description { get; set; }

	}
}
