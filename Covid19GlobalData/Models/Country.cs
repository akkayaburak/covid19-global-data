using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19GlobalData.Models
{
    public class Country
    {
        [Keyword(Name = " Country")]
        public string CountryName { get; set; }

        [Keyword(Name = " Country_code")]
        public string CountryCode { get; set; }
        public Country(string countryName, string countryCode)
        {
            CountryName = countryName;
            CountryCode = countryCode;

        }
        
    }
}
