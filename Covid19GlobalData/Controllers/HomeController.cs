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

namespace Covid19GlobalData.Controllers
{
    public class HomeController : Controller
    {
        private readonly IElasticClient _elasticClient;
        public HomeController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
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
        public IActionResult GetDailyCovidByFilters([FromBody]DailyCovid selectedFields)
        {

            if (ModelState.IsValid)
            {
                //SearchDescriptor<DailyCovid> parent = new SearchDescriptor<DailyCovid>();
                //QueryContainerDescriptor<DailyCovid> query = new QueryContainerDescriptor<DailyCovid>();
                //TermQueryDescriptor<DailyCovid> term = new TermQueryDescriptor<DailyCovid>();

                //if (!string.IsNullOrWhiteSpace(selectedFields.Country))
                //{
                //    term = term.Name("by_country"); 
                //}

                //if (!string.IsNullOrWhiteSpace(selectedFields.CountryCode))
                //{
                //    term = term.Name("by_countryCode");
                //}

                //query.Term();

                var searchResponse = _elasticClient.Search<DailyCovid>(s => s
                   .Query(q => q
                   .Term(c => c
                   .Name("by_country")
                   .Field(p => p.CountryCode)
                   .Value(selectedFields.CountryCode.ToString())))
                   .Size(200)
                   .Sort(ss => ss
                   .Descending(d => d.DateReported)));
                var searchResponseList = searchResponse.Documents.ToList();
                var dailyCovidViewModel = new DailyCovidViewModel
                {
                    DailyCovids = searchResponseList
                };
                return Json(dailyCovidViewModel);
            }
            //var json = selectedFields.ToString();
            //var result = JsonConvert.DeserializeObject<DailyCovid>(selectedFields);
            return Json("");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
