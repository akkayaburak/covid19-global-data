using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19GlobalData.Models
{
    public class Country
    {
        public string CountryName { get; set; }
        public Country(string countryName)
        {
            this.CountryName = countryName;
        }
        
    }
}
