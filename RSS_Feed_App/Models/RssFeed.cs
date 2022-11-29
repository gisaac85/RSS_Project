using System.ServiceModel.Syndication;

namespace RSS_Feed_App.Models
{
	public class RssFeed 
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Link { get; set; }
		public string Guid { get; set; }
		public string PublicationDate { get; set; }
	}
}
