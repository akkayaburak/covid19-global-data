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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Globalization;

namespace Covid19GlobalData.Controllers
{
    public class HomeController : Controller
    {
        private readonly IElasticClient _elasticClient;
        public HomeController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var searchResponse = _elasticClient.Search<DailyCovid>(s => s.From(0).Take(1000).MatchAll().Sort(ss => ss.Descending(d => d.DateReported)));
            var dailyCovids = searchResponse.Documents.ToList();
            var viewModelList = new DailyCovidViewModel
            {
                DailyCovids = dailyCovids
            };
            return View(viewModelList);
        }

        [HttpPost]
        public IActionResult GetDailyCovidByFilters([FromBody]DailyCovidDate selectedFields)
        {

            if (ModelState.IsValid)
            {
                var searchResponse = _elasticClient.Search<DailyCovid>(s => s
               .Query(q => q
               .Term(c => c
               .Name("by_country")
               .Field(p => p.CountryCode)
               .Value(selectedFields.DailyCovid.CountryCode))
               &&
               q.DateRange(c => c
               .Name("by_date")
               .Field(p => p.DateReported)
               .GreaterThanOrEquals(selectedFields.StartDateTime.Value.ToString("MM/dd/yyyy",CultureInfo.InvariantCulture))
               .LessThanOrEquals(selectedFields.EndDateTime.Value.ToString("MM/dd/yyyy",CultureInfo.InvariantCulture))
               .Format("MM/dd/yyyy")))
               .Size(300));
                var searchResponseList = searchResponse.Documents.ToList();
                var dailyCovidViewModel = new DailyCovidViewModel
                {
                    DailyCovids = searchResponseList
                };
                return Ok(dailyCovidViewModel);
            }
            return Ok("");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
