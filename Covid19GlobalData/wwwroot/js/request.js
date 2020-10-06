$('#select').on('change', function () {
    var countryCode = $("#select").val();
    var model = {
        Country:"",
        CountryCode: countryCode,
        CumulativeCases: 0,
        CumulativeDeaths: 0,
        NewCases: 0,
        NewDeaths: 0,
        WhoRegion: "",
        DateReported: ""
    };
    var data = JSON.stringify(model);
    console.log(data);
    $.ajax({
        url: "/Home/GetDailyCovidByFilters",
        data: data,
        type: "POST",
        contentType:"application/json",
        success: function (result) {
            $(".cv-data").remove();
            $(".span").remove();
            $(".col-md-6").append('<span class="span btn btn-primary">Size : ' + result.dailyCovids.length + '</span>');
            //$(".container").append('<div class="col-4">' + result.dailyCovids.length + '</div>');
            $.each(result.dailyCovids, function (key, value) {
                var string = '<tr class="cv-data"><td>' + value.country + '</td><td>' + value.cumulativeDeaths + '</td><td>' + value.countryCode + '</td><td>' + value.newCases + '</td><td>' + value.cumulativeCases + '</td><td>' + value.whoRegion + '</td><td>' + value.newDeaths + '</td><td>' + value.dateReported.replace("-", "/").replace("-", "/").replace("T00:00:00","") + '</td></tr >'
                $('#cvDataTbl').append(string);
            });

        },
        error: function () {
            alert("Error!");
        },
    })
})

