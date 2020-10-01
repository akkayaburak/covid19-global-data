using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Covid19GlobalData.Models;
using Covid19GlobalData.Extensions;
using Nest;

namespace Covid19GlobalData.Controllers
{
    public class HomeController : Controller
    {
        private IElasticClient _elasticClient;
        public HomeController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        public IActionResult Index()
        {
            var searchResponse = _elasticClient.Search<DailyCovid>(s => s.From(0).Take(10000).MatchAll().Sort(ss => ss.Descending(d => d.DateReported) ));
            
            //var searchResponse = _elasticClient.Search<Data>(s => s
            //.Aggregations(a => a.Terms("countiries_agg", t => t
            //.Field(p => p.Country).Aggregations(ab => ab.Cardinality("countiries_car", c => c.Field(p => p.Country))))));

            var countrySearch = _elasticClient.Search<DailyCovid>(s => s.Aggregations(a => a.Terms("countiries", t => t.Field(f => f.Country).Size(200))));
            //var agg = searchResponse.Aggregations.Terms("t").Buckets.ToList();
            //var countrySearch = _elasticClient.Search<DailyCovid>(s => s.From(0).Take(200).Aggregations(a => a.Terms("countiries", t => t.Field(f => f.Country))));
            var countiries = countrySearch.Aggregations.Terms("countiries").Buckets.ToList();
            List<Country> countiriesList = new List<Country>();
            foreach (var item in countiries)
            {
                countiriesList.Add(new Country(item.Key));
            }
            //var searchResponse = _elasticClient.Search<DailyCovid>(s => s
            //.Query(q => q
            //.Term(c => c
            //.Name("by_country")
            //.Field(p => p.Country)
            //.Value("Turkey")))
            //.Size(300)
            //.Sort(ss => ss
            //.Descending(d => d.DateReported)));
            //var searchResponseList = searchResponse.Documents.ToList();
            var viewModelList = new DailyCovidViewModel
            {
                DailyCovids = searchResponse.Documents.ToList(),
                Countries = countiriesList
            };
            return View(viewModelList);
        }
        public IActionResult Search(DailyCovidViewModel model) 
        {
            return View();
        
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
