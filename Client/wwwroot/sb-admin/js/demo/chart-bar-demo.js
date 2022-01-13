$.ajax({
    url: "https://localhost:44378/api/Universities/UniversityCount"
}).done((result) => {
    
    //console.log(result.result);
    
    var title = [];
    var value = [];

    $.each(result.result, function (key, val) {
        title.push(val.universityName);
        value.push(val.count);
        //console.log(val.universityName);
    });
    console.log(title);
    ChartBar(title, value);

}).fail((error) => {
    console.log(error);
});


function ChartBar(title, value) {
var options = {
    series: [{
        data: value
    }],
    chart: {
        height: 350,
        type: 'bar',
        events: {
            click: function (chart, w, e) {
                // console.log(chart, w, e)
            }
        }
    },
    colors: ['#ff0000', '#E91E63', '#9C27B0', '#0000FF', '#A9A9A9', '#FF1493', '#006400'],
    plotOptions: {
        bar: {
            columnWidth: '45%',
            distributed: true,
        }
    },
    dataLabels: {
        enabled: false
    },
    legend: {
        show: false
    },
    xaxis: {
        categories: title,
        labels: {
            style: {
                fontSize: '12px'
            }
        }
    }
};

var chart = new ApexCharts(document.querySelector("#myBarChart"), options);
chart.render();
    document.getElementById("myBarTitle").innerHTML = "Statistik Karyawan Berdasarkan Universitas";
}