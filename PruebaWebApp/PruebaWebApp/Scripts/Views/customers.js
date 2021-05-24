var consultas = {};

(function (self) {
    "use strict";
    var form = function (selector) { return document.getElementById(selector) }

    function loadData() {
        $.ajax({
            url: "/ReportCustomers/ObterneListaCustomer",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.lista.length > 0) {

                    $('#tblGeneral').dataTable.responsive = true;
                    $('#tblGeneral').DataTable(
                        {
                            dom: 'Bfrtip',
                            data: data.lista,
                            buttons: [
                                'copyHtml5',
                                'excelHtml5',
                                'csvHtml5',
                                'pdfHtml5'
                            ],
                            columns: [
                                { title: "#Cliente", "data": "IdCliente" }
                                , {
                                    title: "Fecha de Registro Empresa", "data": "FechaRegistroEmpresa",
                                    render: function (data, type, row) {
                                        return dateFromStringWithTime(data);
                                    }
                                }
                                , { title: "RazonSocial", "data": "RazonSocial" }
                                , { title: "RFC", "data": "RFC" }
                                , { title: "Sucursal", "data": "Sucursal" }
                                , { title: "#Empleado", "data": "IdEmpleado" }
                                , { title: "Nombre", "data": "Nombre" }
                                , { title: "Apellido Paterno", "data": "Paterno" }
                                , { title: "Apellido Materno", "data": "Materno" }
                                , { title: "#Viaje", "data": "IdViaje" }
                            ]
                        }
                    );


                }

                console.log("Consultando");
                $('#divExportar').show();

            },
            error: function (data) {
                console.log(data);
            }
        });
    }

    function dateFromStringWithTime(str) {
        if (str == "" || str == null) {
            return "";
        }
        else {
            var match;
            if (!(match = str.match(/\d+/))) {
                return false;
            }
            var date = new Date();
            date.setTime(match[0] - 0);
            var x = new Date(date);
            var dd = x.getDate();
            var mm = x.getMonth() + 1;
            var yy = x.getFullYear();
            date = dd + "/" + mm + "/" + yy;
            return date;
        }

    }

    $(document).ready(function () {
        loadData();
    });


})(consultas);






