using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Workloopz.ViewModels
{
	public class TaskVM
	{
		    
		public int Id { get; set; }
        [Display(Name = "Tên công việc")]
      
        public string Tittle { get; set; } = null!;
		[Display(Name = "Mô tả")]
		public string? Description { get; set; }
      
        [Display(Name = "Thời gian dự kiến")]
		public DateTime? ScheduledTime { get; set; }
		[Display(Name = "Thời gian bắt đầu")]
		public DateTime? ActualStart { get; set; }
		[Display(Name = "Thời gian kết thúc")]
		public DateTime? ActualEnd { get; set; }
		[Display (Name = "Thời hạn")]
		public DateTime? ScheduledEnd { get; set; }
		[Display(Name = "Trạng thái")]
		public int StatusId { get; set; }
		[Display(Name = "Độ ưu tiên")]
		public int PriorityId { get; set; }

		public int Owner { get; set; }

		public int? ProjectId { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? Updated { get; set; }
        public int? Duration { get; set; }
        public decimal? Progress { get; set; }
        public int? ParentId { get; set; }
    }
}
