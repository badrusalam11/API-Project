$('#loginForm').submit(function (e) {
    e.preventDefault();
        // do ajax now
        Login();
        //location.href = "https://localhost:44361/admin";

});

function Login() {
    var obj = new Object();

    obj.Email = $("#email").val();
    obj.Password = $("#password").val();

    console.log(obj);

    //let table = $('#peserta').DataTable();
    $.ajax({
        url: "https://localhost:44361/login/login",
        type: "POST",
        data: obj 
    }).done((result) => {
        console.log(result);
        var ikon;
        var pesan;
        if (result.status === 200) {
            ikon = 'success';
            pesan = 'Success';
            
        }
        else {
            ikon = 'error';
            pesan = 'Error';
        }
        Swal.fire({
            icon: ikon,
            title: pesan,
            text: result.message
        })

        if (result.status === 200) {
        setTimeout(function () {
            location.href = "https://localhost:44361/admin";
        }, 3000);
        }
    }).fail((error) => {
        console.log(error);
        //alert(result.message);
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: error.responseJSON.title
        })

    })
}