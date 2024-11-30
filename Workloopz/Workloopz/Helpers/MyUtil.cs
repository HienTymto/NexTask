using System.Text;
using Workloopz.Data;

namespace Workloopz.Helpers
{
	public class MyUtil
	{
		
		public static string GenerateRandomKey(int length = 5)
		{
			var pattern = @"abcdefgHIJKLMNOPQRSTU!";
			var sb = new StringBuilder();
			var random = new Random();
			for (int i = 0; i < length; i++)
			{
				sb.Append(pattern[random.Next(0, pattern.Length)]);
			}
			return sb.ToString();
		}
		
	}
}
