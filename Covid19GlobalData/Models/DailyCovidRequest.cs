using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19GlobalData.Models
{
    public class DailyCovidRequest
    {
        public string Country { get; set; }

        public string CountryCode { get; set; }

        public int? CumulativeCases { get; set; }

        public int? CumulativeDeaths { get; set; }

        public int? NewCases { get; set; }

        public int? NewDeaths { get; set; }

        public string WhoRegion { get; set; }

        public DateTime? DateReported
        {
            get; set;
        }
    }
}
