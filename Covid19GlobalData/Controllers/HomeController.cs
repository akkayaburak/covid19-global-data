using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Covid19GlobalData.Models;
using Nest;

namespace Covid19GlobalData.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("covid19-global-data");
            var client = new ElasticClient(settings);
            //var searchResponse = client.Search<Data>(s => s.Index("covid19-global-data").Query(q => q.MatchAll()));
            var searchResponse = client.Search<Data>(s => s.From(0).Take(10000).MatchAll());
            //var searchResponse = client.Search<Data>(s => s.Query(q => q.Term(f => f.Country, "Turkey")).Size(20).Explain());
            var docs = searchResponse.Documents.Select(f => f).ToList();
            ViewBag.All = docs;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
