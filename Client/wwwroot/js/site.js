//// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
//// for details on configuring this project to bundle and minify static web assets.

//// Write your JavaScript code.

////console.log("berhasil");

////var data5 = $(".list").html("halo ini diubah melalui jquery");

////var data = document.getElementById("judul");
////data.addEventListener("click", function () {
////    data.style.backgroundColor = "blue";
////});

////$('.judul').css('color', 'white');


////mode berhasil :
////var hiddenBox = $("#modals");

////$("#download").on("click", function (event) {
////hiddenbox.show();
////var delayinmilliseconds = 3000; //5 second
////settimeout(function () {
////    //your code to be executed after 5 second
////        var i = 100;
////        while (i > 0) {
////            console.log(i);
////        }
////}, delayinmilliseconds);

////});

////let download = document.getElementById("download");
////let hiddenBox = document.getElementById("modals");
////download.addEventListener("click", function () {

////    //var delayinmilliseconds = 10000; //5 second
////    //let time = setTimeout(function () {
////    //    //your code to be executed after 5 second
////    //        //var i = 100;
////    //        //while (i > 0) {
////    //        //    console.log(i);
////    //        //}
////    //}, delayinmilliseconds);
////    var timeleft = 10;
////    var downloadTimer = setInterval(function () {
////        if (timeleft <= 0) {
////            clearInterval(downloadTimer);

////        }
////        document.getElementById("count").innerHTML = timeleft;
////        timeleft -= 1;
////        if (timeleft === 0) {
////            hiddenBox.style.display = "block";
////            setInterval(crash, 2000);
////        }
////    }, 1000);

////    function crash() {
////        let i = 100;
////        while (i > 0) {
////            console.log(i);
////        }
////    };
////});

//let promo = document.getElementById("promo");
//let event = document.getElementById("event");
//let certified = document.getElementById("certified");
//let maaf = document.getElementById("maaf");

//promo.addEventListener("click", function () {
//    certified.style.display = "block";
//    maaf.style.display = "none";
//});
//event.addEventListener("click", function () {
//    maaf.style.display = "block";
//    certified.style.display = "none";
//});

//$(document).ready(function () {
//    $('#navbarDarkDropdownMenuLink').dropdown()
//});

$(document).ready(function () {
    $('li').click(function () {
        $("li.active").removeClass("active");
        $(this).addClass('active');

    })
});

//$(.bg-login-image).css('background-image', 'url(' + https://www.linovhr.com/wp-content/uploads/2020/09/employee-database.jpg + ')');