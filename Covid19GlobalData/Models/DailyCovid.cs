using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19GlobalData.Models
{
    public class DailyCovid
    {
        [Keyword(Name=" Country")]
        public string Country { get; set; }

        [Keyword(Name=" Country_code")]
        public string CountryCode { get; set; }
        
        [Number(Name = " Cumulative_cases")]
        public long CumulativeCases { get; set; }

        [Number(Name = " Cumulative_deaths")]
        public long CumulativeDeaths { get; set; }

        [Number(Name = " New_cases")]
        public long NewCases { get; set; }

        [Number(Name = " New_deaths")]
        public long NewDeaths { get; set; }

        [Keyword(Name= " WHO_region")]
        public string WhoRegion { get; set; }

        [Date(Name="Date_reported")]
        public DateTime DateReported { get; set; }
    }
}
