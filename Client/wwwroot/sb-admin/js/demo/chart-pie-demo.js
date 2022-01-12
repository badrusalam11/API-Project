////// Set new default font family and font color to mimic Bootstrap's default styling
////Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
////Chart.defaults.global.defaultFontColor = '#858796';

////// Pie Chart Example
////var ctx = document.getElementById("myPieChart");
////var myPieChart = new Chart(ctx, {
////  type: 'doughnut',
////  data: {
////    labels: ["Direct", "Referral", "Social"],
////    datasets: [{
////      data: [50, 50,50],
////      backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc'],
////      hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf'],
////      hoverBorderColor: "rgba(234, 236, 244, 1)",
////    }],
////  },
////  options: {
////    maintainAspectRatio: false,
////    tooltips: {
////      backgroundColor: "rgb(255,255,255)",
////      bodyFontColor: "#858796",
////      borderColor: '#dddfeb',
////      borderWidth: 1,
////      xPadding: 15,
////      yPadding: 15,
////      displayColors: false,
////      caretPadding: 10,
////    },
////    legend: {
////      display: false
////    },
////    cutoutPercentage: 80,
////  },
////});

//var options = {
//    series: [44, 55, 13, 43, 22],
//    chart: {
//        width: 380,
//        type: 'pie',
//    },
//    labels: ['Team A', 'Team B', 'Team C', 'Team D', 'Team E'],
//    responsive: [{
//        breakpoint: 480,
//        options: {
//            chart: {
//                width: 200
//            },
//            legend: {
//                position: 'bottom'
//            }
//        }
//    }]
//};

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
        type: 'pie'
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