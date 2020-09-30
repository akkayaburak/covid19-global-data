using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19GlobalData.Interfaces
{
    public interface IData
    {
        string Country { get; }

        string CountryCode { get; }

        long CumulativeCases { get; }

        long CumulativeDeaths { get; }

        long NewCases { get; }

        long NewDeaths { get; }

        string WhoRegion { get; }

        DateTime DateReported { get; }

    }
}
