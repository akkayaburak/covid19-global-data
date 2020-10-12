
$('#searchButton').on('click', function () {
    var dateFrom = $('#from').val();
    var dateTo = $('#to').val();
    var countryCode = $("#select").val();
    var model =
    {
        "StartDateTime": dateFrom,
        "EndDateTime": dateTo,
        "DailyCovid": {
            "Country": "",
            "CountryCode": countryCode,
            "CumulativeCases": "0",
            "CumulativeDeaths": "0",
            "NewCases": "0",
            "NewDeaths": "0",
            "WhoRegion": "Test"
        }
    };
    $.ajax({
        url: '/Home/GetDailyCovidByFilters',
        data: JSON.stringify(model),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $(".cv-data").remove();
            $(".span").remove();
            $(".countCase").append('<span class="span btn btn-primary">Size : ' + result.dailyCovids.length + '</span>');
            //$(".container").append('<div class="col-4">' + result.dailyCovids.length + '</div>');
            $.each(result.dailyCovids, function (key, value) {
                var string = '<tr class="cv-data"><td>' + value.country + '</td><td>' + value.cumulativeDeaths + '</td><td>' + value.countryCode + '</td><td>' + value.newCases + '</td><td>' + value.cumulativeCases + '</td><td>' + value.whoRegion + '</td><td>' + value.newDeaths + '</td><td>' + value.dateReported.replace("-", "/").replace("-", "/").replace("T00:00:00", "") + '</td></tr >'
                $('#cvDataTbl').append(string);
            });

        },
        error: function () {
            alert("Error!");
        },
    })
})

