using System.Runtime.Caching;
using System.ServiceModel.Syndication;
using System.Xml;

namespace RSS_Feed_App.Helpers
{
    public static class CacheHelper
    {

        private static MemoryCache _cache = MemoryCache.Default;

        public static IEnumerable<SyndicationItem> FeedList()
        {          
            if (!_cache.Contains("FeedList"))
            {
                RefreshFeedList();
            }
            return _cache.Get("FeedList") as IEnumerable<SyndicationItem>;              
        }

        public static void RefreshFeedList()
        {
            var url = "http://feeds.bbci.co.uk/news/world/rss.xml";
            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            var feedList = feed.Items;

            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddDays(1);
            _cache.Set("FeedList", feedList, cacheItemPolicy);
            _cache.Add("FeedList", feedList, cacheItemPolicy);
        }
    }

}
