namespace Workloopz.ViewModels
{
	public class KanbanVM
	{
		public int id { get; set; }
		public string title { get; set; }
		public DateTime? end { get; set; }
		public string? description { get; set; }
		public int? status { get; set; }
	}
}
