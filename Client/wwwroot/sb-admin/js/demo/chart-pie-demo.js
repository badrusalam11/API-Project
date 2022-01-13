$.ajax({
    url: "https://localhost:44378/API/Employees"
}).done((result) => {
    var male = result.filter((g) => {
        return g.gender == 0;
    });
    var female = result.filter((g) => {
        return g.gender == 1;
    });
    //console.log("male");
    //console.log(male.length);
    ChartPie(male.length, female.length);
}).fail((error) => {
    console.log(error);
});

function ChartPie(male, female) {
var options = {
    series: [male, female],
    chart: {
        width: 380,
        type: 'pie',

        toolbar: {
            show: true,
            offsetX: 0,
            offsetY: 0,
            tools: {
                download: true,
                selection: true,
                zoom: true,
                zoomin: true,
                zoomout: true,
                pan: true,
                reset: true | '<img src="/static/icons/reset.png" width="20">',
                customIcons: []
            },
            export: {
                csv: {
                    filename: undefined,
                    columnDelimiter: ',',
                    headerCategory: 'category',
                    headerValue: 'value',
                    dateFormatter(timestamp) {
                        return new Date(timestamp).toDateString()
                    }
                },
                svg: {
                    filename: undefined,
                },
                png: {
                    filename: undefined,
                }
            },
            autoSelected: 'zoom'
        },

    },
    labels: ['Laki-laki', 'Perempuan'],
    colors: ['#F44336', '#FFC0CB'],
    responsive: [{
        breakpoint: 480,
        options: {
            chart: {
                width: 200
            },
            legend: {
                position: 'bottom'
            }
        }
    }]
};

var chart = new ApexCharts(document.querySelector("#myPieChart"), options);
var render = chart.render();
//console.log(render);
    document.getElementById("myPieTitle").innerHTML = "Statistik Karyawan Berdasarkan Gender";
}