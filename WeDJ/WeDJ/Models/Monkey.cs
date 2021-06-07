using WeDJ.Interfaces;

namespace WeDJ.Models
{
	public class Monkey : IMapModel
	{
		public string Name { get; set; }
		public string Details { get; set; }
		public ILocation Location { get; set; }
		public string ImageUrl { get; set;}
	}
}

