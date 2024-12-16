namespace Workloopz.ViewModels
{
	public class commentVM
	{
		public string Contents { get; set; } = null!;

		public int UserId { get; set; }

		public int TaskId { get; set; }

		public int Id { get; set; }
		public DateTime? CreateAt { get; set; }
	}
}