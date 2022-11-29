using Microsoft.AspNetCore.Mvc;
using RSS_Feed_App.Models;
using System.Diagnostics;
using System.ServiceModel.Syndication;
using System.Xml;
using System.IO;
using System.Runtime.Caching;
using Newtonsoft.Json;
using RSS_Feed_App.Helpers;

namespace RSS_Feed_App.Controllers
{
    public class HomeController : Controller
    {
        IEnumerable<SyndicationItem> post;

        public HomeController()
        {

        }
        public IActionResult RefreshList()
        {
            var cachedList = CacheHelper.FeedList().TakeLast(3).ToList();
             var model = new List<RssFeed>();

            foreach (var item in cachedList)
            {
                var rssFeed = new RssFeed();
                rssFeed.Title = item.Title.Text;
                rssFeed.Description = item.Summary.Text;
                rssFeed.PublicationDate = item.PublishDate.ToString();

                model.Add(rssFeed);       
            };
            return View(nameof(Index), model);
        }

        public IActionResult Index()
        {
            var url = "http://feeds.bbci.co.uk/news/world/rss.xml";
            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            post = feed.Items.TakeLast(3).OrderByDescending(x => x.PublishDate);

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