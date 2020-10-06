$('#select').on('change', function () {
    var countryCode = $("#select").val();
    $.ajax({
        url: "Home/GetDailyCovidByCountry?countryCode=" + countryCode,
        type: "GET",
        success: function (result) {
            $(".cv-data").remove();
            $(".span").remove();
            $(".col-md-6").append('<span class="span btn btn-primary">Size : ' + result.dailyCovids.length + '</span>');
            //$(".container").append('<div class="col-4">' + result.dailyCovids.length + '</div>');
            $.each(result.dailyCovids, function (key, value) {
                var string = '<tr class="cv-data"><td>' + value.country + '</td><td>' + value.cumulativeDeaths + '</td><td>' + value.countryCode + '</td><td>' + value.newCases + '</td><td>' + value.cumulativeDeaths + '</td><td>' + value.whoRegion + '</td><td>' + value.newDeaths + '</td><td>' + value.dateReported.replace("-", "/").replace("-", "/").replace("T00:00:00","") + '</td></tr >'
                $('#cvDataTbl').append(string);
            });

        },
        error: function () {
            alert("Error!");
        },
    })
})

