using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Covid19GlobalData.Models;
using Covid19GlobalData.Extensions;
using Nest;
using MoreLinq;

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
            var searchResponse = _elasticClient.Search<DailyCovid>(s => s.From(0).Take(100).MatchAll().Sort(ss => ss.Descending(d => d.DateReported) ));
            #region
            //var countrySearch = _elasticClient.Search<DailyCovid>(s => s
            //.Query(q => q
            //.Match(m => m
            //.Field(f => f
            //.CountryCode)
            //        )
            //     )
            //);
            //.Aggregations(a => a.Terms("countiries", t => t.Field(f => f.Country).Size(200))));
            //var countrySearch = _elasticClient.Search<DailyCovid>(s => s.Query(q => q.MultiMatch(mm => mm.Fields(f => f
            //      .Field(ff => ff.Country).Field(ff => ff.CountryCode)))).Size(200));           
            //var cslist = countrySearch.Documents.ToList();
            //var countiries = countrySearch.Aggregations.Terms("countiries").Buckets.ToList();
            //var countiries2 = _elasticClient.Search<Country>(s => s.StoredFields(sf => sf.Fields(f => f.CountryCode, f => f.CountryName)).Query(q => q.MatchAll()));
            //var countries = _elasticClient.Search<Country>(s => s.From(0).Take(10000).Source(sf => sf.Includes(i => i.Fields(f => f.CountryCode, f => f.CountryName))).Query(q => q.MatchAll()));
            #endregion
            var countries = searchResponse.Documents.ToList();
            var distinctByCountry = searchResponse.Documents.ToList().DistinctBy(x=>x.CountryCode);
            List<Country> countriesList = new List<Country>();
            foreach (var item in distinctByCountry)
            {
                countriesList.Add(new Country(item.Country, item.CountryCode));
            }
            #region searchByValue
            #endregion
            var viewModelList = new DailyCovidViewModel
            {
                DailyCovids = countries,
                Countries = countriesList
            };
            return View(viewModelList);
        }
        

        public IActionResult GetDailyCovidByCountry(string countryCode)
        {
            var searchResponse = _elasticClient.Search<DailyCovid>(s => s
            .Query(q => q
            .Term(c => c
            .Name("by_country")
            .Field(p => p.CountryCode)
            .Value(countryCode)))
            .Size(200)
            .Sort(ss => ss
            .Descending(d => d.DateReported)));
            var searchResponseList = searchResponse.Documents.ToList();
            var dailyCovidViewModel = new DailyCovidViewModel
            {
                Countries = null,
                DailyCovids = searchResponseList
            };
            return View("Index",dailyCovidViewModel);
            //return RedirectToAction("Index", "Home", dailyCovidViewModel);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
