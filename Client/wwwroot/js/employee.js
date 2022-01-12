$(document).ready(function () {
    let api = $('#peserta').DataTable(
        {
            ajax: { url: "https://localhost:44378/API/Employees", dataSrc: '' },
            dataType: 'json',
            columns: [
                {
                    data: null,
                    render: (data, type, row, meta) => {
                        return (meta.row + 1);
                    }
                },
                { data: "nik" },
                {
                    data: "null",
                    render: function (data, type, row) {
                        return `${row['firstName']} ${row['lastName']}`;
                    }
                },
                { data: "phone" },
                {
                    data: "null",
                    render: function (data, type, row) {
                        if (row['gender'] == 0) {
                            return "Laki-laki";
                        }
                        else {
                            return "Perempuan";
                        }
                    }
                },
                {
                    data: "null",
                    render: (data, type, row) => {
                        var dataGet = new Date(row['birthDate']);
                        return dataGet.toLocaleDateString();
                    }
                },
                { data: "email" },
                { data: "salary" },
                {
                    data: "null",
                    render: function (data, type, row) {
                        return `<button class="btn btn-warning fa fa-edit"></button> <button class="btn btn-danger fa fa-trash"></button>`;
                    }
                }

            ],
            //    dom: 'Bfrtip',
            //    buttons: [
            //        'copy', 'csv', 'excel', 'pdf', 'print'
            //    ]

            //}
            dom: 'Bfrtip',
            scrollX:'true',
            buttons: [
                {
                    extend: 'copyHtml5',
                    title: "Data Karyawan",
                    className: "btn btn-secondary",
                    text: "<i class='fa fa-clone'> Copy</i>",
                    exportOptions: {
                        columns: [0, 1, 2, 3,4,5,6,7]
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
                    className: "btn btn-primary",
                    text: "<i class='fa fa-print'> Print</i>",
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7]
                    }
                }
            ]

        });
    console.log(api);
});

