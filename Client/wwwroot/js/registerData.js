$(document).ready(function () {
    $('#peserta').DataTable(
        {
            ajax: { url: "https://localhost:44378/API/Employees/getregisterdata", dataSrc: 'result' },
            dataType: 'json',
            columns: [
                //{
                //    data: null,
                //    //bSortable: false,
                //    render: (data, type, row, meta) => {
                //        //return (meta.row + 1);
                //        return meta.row + meta.settings._iDisplayStart + 1;
                //    }
                //},
                { data: "nik" },
                { data: "fullName" },
                { data: "phone" },
                { data: "email" },
                { data: "gender" },

                {
                    data: "null",
                    render: (data, type, row) => {
                        var dataGet = new Date(row['birthDate']);
                        return dataGet.toLocaleDateString();
                    }
                },
                { data: "education.degree" },
                { data: "education.gpa" },
                { data: "education.university.name" },
                { data: "roleName" },
                {
                    data: "salary",
                    render: DataTable.render.number('.', ',', 2, 'Rp ')
                    //render: (data, type, row) =>{
                    //    return formatRupiah(row['salary'], "Rp. ");
                    //}
                },
                {
                    data: "null",
                    render: function (data, type, row) {
                        return `<button class="btn btn-warning fa fa-edit" data-toggle="modal" data-target="#employeeModal" onclick="ShowEdit(${row['nik']})"></button> <button onclick="Delete(${row['nik']})" class="btn btn-danger fa fa-trash"></button>`;
                    }
                }

            ],
            //    dom: 'Bfrtip',
            //    buttons: [
            //        'copy', 'csv', 'excel', 'pdf', 'print'
            //    ]

            //}
            dom: 'Bfrtip',
            scrollX: 'true',
            buttons: [
                {
                    extend: 'copyHtml5',
                    title: "Data Karyawan",
                    className: "btn btn-secondary",
                    text: "<i class='fa fa-clone'> Copy</i>",
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7]
                    }
                },
                {
                    extend: 'csvHtml5',
                    title: "Data Karyawan",
                    className: "btn btn-warning",
                    text: "<i class='fa fa-table'> CSV</i>",
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7]
                    }
                },
                {
                    extend: 'excelHtml5',
                    title: "Data Karyawan",
                    className: "btn btn-success",
                    text: "<i class='fa fa-file-excel-o'> Excel</i>",
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7]
                    }
                },
                {
                    extend: 'pdfHtml5',
                    title: "Data Karyawan",
                    className: "btn btn-danger",
                    text: "<i class='fa fa-file-pdf-o'> PDF</i>",
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7]
                    }
                },
                {
                    extend: 'print',
                    title: "Data Karyawan",
                    className: "btn btn-dark",
                    text: "<i class='fa fa-print'> Print</i>",
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7]
                    }
                }
            ]

        });

});

function formatRupiah(angka, prefix) {
    var number_string = angka.toString().replace(/[^,\d]/g, ''),
        split = number_string.split(','),
        sisa = split[0].length % 3,
        rupiah = split[0].substr(0, sisa),
        ribuan = split[0].substr(sisa).match(/\d{3}/gi);

    // tambahkan titik jika yang di input sudah menjadi angka ribuan
    if (ribuan) {
        separator = sisa ? '.' : '';
        rupiah += separator + ribuan.join('.');
    }

    rupiah = split[1] != undefined ? rupiah + ',' + split[1] : rupiah;
    return prefix == undefined ? rupiah : (rupiah ? 'Rp. ' + rupiah : '');
}

//document.getElementById("#registerForm").addEventListener("submit", function (e) {
//    if (!isValid) {
//        e.preventDefault();    //stop form from submitting
//    }
//    //do whatever an submit the form
//    Insert();
//});

$('#registerForm').submit(function (e) {
    e.preventDefault();
    if ($("#submit").html() == "Save Change") {
        // do ajax now
        Insert();
        //$('#registerForm').trigger('reset');
        $("#registerForm")[0].reset();
        $('#employeeModal').modal('hide');

    }
    else {
        //console.log(Edit());
        Edit();
    }

});

function Edit() {
    var obj = new Object(); 
    obj.NIK = $("#nik").val();
    obj.FirstName = $("#firstName").val();
    obj.LastName = $("#lastName").val();
    obj.Email = $("#email").val();
    obj.Phone = $("#phone").val();
    obj.BirthDate = $("#birthDate").val();
    obj.Gender = parseInt($("#gender").val());
    obj.Degree = $("#degree").val();
    obj.GPA = parseFloat($("#gpa").val());
    obj.UniversityId = parseInt($("#university").val());
    obj.Salary = parseInt($("#salary").val());

    const myJSON = JSON.stringify(obj);
    console.log(myJSON);


    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    let table = $('#peserta').DataTable();
    $.ajax({
        url: "https://localhost:44378/api/employees/UpdateRegisterData",
        contentType: "application/json;charset=utf-8",
        type: "PUT",
        data: myJSON 
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
        table.ajax.reload();
    }).fail((error) => {
        console.log(result);

    })
}

//function SubmitInsert() {
//    if () {

//    }
//    Insert();
//    //$('#registerForm').trigger('reset');
//    $("#registerForm")[0].reset();
//    $('#employeeModal').modal('hide');

//}

function Insert() {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.FirstName = $("#firstName").val();
    obj.LastName = $("#lastName").val();
    obj.Email = $("#email").val();
    obj.Password = $("#password").val();
    obj.Phone = $("#phone").val();
    obj.BirthDate = $("#birthDate").val();
    obj.Gender = parseInt($("#gender").val());
    obj.Degree = $("#degree").val();
    obj.GPA = parseFloat($("#gpa").val());
    obj.UniversityId = parseInt($("#university").val());
    obj.Salary = parseInt($("#salary").val());

    const myJSON = JSON.stringify(obj);
    console.log(myJSON);


    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    let table = $('#peserta').DataTable();
    $.ajax({
        url: "https://localhost:44378/api/employees/register",
        contentType: "application/json;charset=utf-8",
        type: "POST",
        data: myJSON //jika 415 unsupported bisa cari cara JSON stringify dll
    }).done((result) => {
        //buat alert pemberitahuan jika success
        console.log(result);
        //alert(result.message);
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
        table.ajax.reload();
    }).fail((error) => {
        console.log(result);
        //alert(result.message);
        //Swal.fire({
        //    icon: 'error',
        //    title: 'Oops...',
        //    text: result.message
        //})

    })
}

function ResetForm() {
    var h1 = "Add Employee Data";
    $("#exampleModalLabel").html(h1);
    $("#password").attr("readonly", false);
    $("#form-nik").attr("hidden", true);
    $("#registerForm")[0].reset();
    $("#submit").html("Save Change");
    UniversitiesOption();
}

function UniversitiesOption() {
    $.ajax({
        url: "https://localhost:44378/api/universities",
    }).done((result) => {
        //buat alert pemberitahuan jika success
        var option = "<option selected>Choose...</option>";
        $.each(result, function (key, val) {
            option += `
            <option value="${val.universityId}">${val.name}</option>
            `;
        });
        $("#university").html(option);

    }).fail((error) => {
        //alert pemberitahuan jika gagal
        console.log("data tidak masuk");
    })
}

function Delete(nik) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            let table = $('#peserta').DataTable();
            $.ajax({
                type: 'DELETE',
                url: "https://localhost:44378/api/Employees/" + nik,
                success: function (result) {
                    console.log(result);
                    table.ajax.reload();
                    $("#registerForm")[0].reset();
                    if (result.status === 200) {
                        Swal.fire(
                            'Deleted!',
                            result.message,
                            'success'
                        )
                    }
                    else {
                        Swal.fire(
                            'Error',
                            result.message,
                            'error'
                        )
                    }
                }
            });
        }
    })
}

function ShowEdit(nik) {
    UniversitiesOption();
    $("#form-nik").attr("readonly", true);
    $.ajax({
        url: "https://localhost:44378/API/Employees/getregisterdata/" + nik,
        contentType: "application/json;charset=utf-8"
    }).done((result) => {
        let data = result.result[0];
        //console.log(data);
        const myArray = data.birthDate.split("T");
        var h1 = "Edit data";
        $("#exampleModalLabel").html(h1);
        $("#nik").val(data.nik);
        $("#firstName").val(data.firstName);
        $("#lastName").val(data.lastName);
        $("#email").val(data.email);
        $("#password").attr("readonly", true);
        $("#phone").val(data.phone);
        $("#birthDate").val(myArray[0]);
        $("#gender").val(data.gender);
        $("#degree").val(data.education.degree);
        $("#gpa").val(data.education.gpa);
        $("#university").val(data.education.university.id);
        $("#salary").val(data.salary);
        //$("button.btn-primary").attr("id", "insert");
        $("#submit").html("Edit");
        //const myJson = JSON.stringify(obj);
        //console.log(myJson);

    }).fail((error) => {
        console.log(error);
    })

    console.log(myJson);
}


//function validate() {
//    $('#registerForm').validate({
//        rules: {
//            errorClass: "error",
//            'password': {
//                required: true,
//                minlength: 5
//            }
//        },
//        messages: {
//            'password': {
//                required: "This field is required",
//                minlength: "Your password must be at least 5 character long"
//            }
//        }
//    });
//}