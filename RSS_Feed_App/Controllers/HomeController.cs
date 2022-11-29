using Microsoft.AspNetCore.Mvc;
using RSS_Feed_App.Models;
using System.Diagnostics;
using System.ServiceModel.Syndication;
using System.Xml;
using System.IO;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace RSS_Feed_App.Controllers
{
    public class HomeController : Controller
    {
        IEnumerable<SyndicationItem> post;
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            var url = "http://feeds.bbci.co.uk/news/world/rss.xml";
            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            post = feed.Items.TakeLast(3).OrderByDescending(x => x.PublishDate);

            IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
            object result = cache.Set("Key", post);
            bool found = cache.TryGetValue("Key", out result);

            var model = new List<RssFeed>();

            foreach (var item in post)
            {
                var rssFeed = new RssFeed();
                rssFeed.Title = item.Title.Text;
                rssFeed.Description = item.Summary.Text;
                rssFeed.PublicationDate = item.PublishDate.ToString();

                model.Add(rssFeed);
            };

            return View(model);
        }

        public IActionResult GetMoreFeeds()
        {
            var url = "http://feeds.bbci.co.uk/news/world/rss.xml";
            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);       

            post = feed.Items.SkipLast(3);
            var newPost = post.TakeLast(3).OrderByDescending(x => x.PublishDate);

            var model = new List<RssFeed>();

            foreach (var item in newPost)
            {
                var rssFeed = new RssFeed();
                rssFeed.Title = item.Title.Text;
                rssFeed.Description = item.Summary.Text;
                rssFeed.PublicationDate = item.PublishDate.ToString();

                model.Add(rssFeed);
            };
        
            return Json(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}