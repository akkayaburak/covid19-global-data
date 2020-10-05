$('#select').on('change', function () {
    var countryCode = $("#select").val();
    $.ajax({
        url: "Home/GetDailyCovidByCountry?countryCode=" + countryCode,
        type: "GET",
        success: function (result) {
            $(".cv-data").remove();
            $.each(result.dailyCovids, function (key, value) {
                console.log(value);
                var string = '<tr class="cv-data"><th scope = "row" ></th><td>' + value.country + '</td><td>' + value.countryCode + '</td><td>' + value.cumulativeDeaths + '</td><td>' + value.newCases + '</td><td>' + value.cumulativeDeaths + '</td><td>' + value.whoRegion + '</td><td>' + value.newDeaths + '</td><td>' + value.dateReported + '</td></tr >'
                $('#cvDataTbl').append(string);
            });

        },
        error: function () {
            alert("Error!");
        },
    })
})