$(function () {
    let voucherTable = $('#VoucherTable').DataTable({
        "ajax": {
            "url": "/Voucher/GetVouchers",
            "type": "GET",
            "datatype": "json",
        },
        "columns": [
            { "data": "serialNumber" },
            { "data": "amount" },
            { "data": "sellDate" },
            { "data": "expirationDate" },
            { "data": "resort" }, 
            { "data": "status" },
            {
                "data": null,
                "render": function (data, type, row) {
                    if (row.status === "Aktywny") {
                        return `<a href="/Details/${row.serialNumber}" class="btn btn-secondary"><i class="bi bi-info-square"></i></a>
                        <button class="btn btn-warning voucher-realization-btn" data-serialNumber="${data.serialNumber}" data-bs-toggle="modal" data-bs-target="#realizationModal"><i class="bi bi-bookmark-x"></i></button>`
                    }
                    else if (row.status === "Nieaktywny") {
                        return `<a href="/Details/${row.serialNumber}" class="btn btn-secondary"><i class="bi bi-info-square"></i></i></a>
                        <button class="btn btn-success voucher-activation-btn" data-serialNumber="${data.serialNumber}" data-bs-toggle="modal" data-bs-target="#activationModal"><i class="bi bi-pencil-square text-white"></i></button>`;
                    }
                    else {
                        return `<a href="/Details/${row.serialNumber}" class="btn btn-secondary"><i class="bi bi-info-square"></i></a>`;
                        
                 
                    }
                            
                },
            }
        ],
        "responsive": true,
        "language": {
            "search": "Wyszukaj",
            "info": "Wyświetlanie _START_ do _END_ z _TOTAL_ wierszy",
            "lengthMenu": "Wyświetl _MENU_ wierszy na stronę",
            "infoFiltered": "(przefiltrowano z _MAX_ wszystkich rekordów)",
            "emptyTable": "Brak danych do wyświetlenia",
            "zeroRecords": "Brak wyników spełniających kryteria wyszukiwania",
            "infoEmpty": "Brak wyników do wyświetlenia",

        },
    });

    $(document).on('click', '.voucher-activation-btn', function () {
        clearModalFields();
        clearErrorMessages();
        let serialNumber = $(this).data('serialnumber');

        getVoucher(serialNumber).done(function (response) {
            $(".modal-title").html(`Aktywuj voucher o nr: ${response.data.serialNumber}`);
        }).fail(function () {
            alert("Wystąpił błąd podczas pobierania danych.")
        });

        submitActivationForm(serialNumber);

    });

    $(document).on('click', '.voucher-realization-btn', function () {
        $('#realizationDate').val('');
        let serialNumber = $(this).data('serialnumber');

        getVoucher(serialNumber).done(function (response) {
            $(".modal-title").html(`Aktywuj voucher o nr: ${response.data.serialNumber}`);
        }).fail(function () {
            alert("Wystąpił błąd podczas pobierania danych.")
        });

        submitRealizationForm(serialNumber);
    });

    function submitRealizationForm(serialNumber) {
        $('#realizationForm').off('submit').on('submit', function (e) {

            e.preventDefault();

            let date = $('#realizationDate').val();
           
            $.ajax({
                url: '/Voucher/EndStayByWorker',
                type: 'POST',
                data: {
                    "date": date,
                    "serialNumber": serialNumber
                },
                success: function (response) {
                    if (response.success) {
                        $('#realizationModal').modal('hide');
                        voucherTable.ajax.reload();
                        alert('Voucher został zrealizowany.');
                    } else {
                        console.log("ELSE")
                        alert(`Wystąpił błąd podczas realizacji vouchera. Błąd: ${response.error}`);
                    }
                },
                error: function () {
                    alert('Wystąpił błąd podczas realizacji vouchera.');
                }
            });
        });
    }


    function submitActivationForm(serialNumber) {

        $('#activationForm').off('submit').on('submit', function (e) {
            e.preventDefault();

            let formData = {
                serialNumber: serialNumber,
                invoiceNumber: $('#invoiceNumber').val(),
                email: $('#email').val(),
                phoneNumber: $('#phoneNumber').val()
            }
            

            $.ajax({
                url: '/Voucher/ActivateVoucherByWorker',
                type: 'POST',
                data: {
                    "activationVoucher": formData
                },
                success: function (response) {
                    if (response.success) {
                        $('#activationModal').modal('hide');
                        voucherTable.ajax.reload();
                        alert('Voucher został aktywowany.');
                    } else {
                        console.log(response.errors)
                        displayValidationErrors(response.errors)
                    }
                },
                error: function () {
                    console.log("ERROR")
                    alert('Wystąpił błąd podczas aktywacji vouchera.');
                }
            });
            
        });
    }

    function displayValidationErrors(errors) {
        clearErrorMessages();

        for (let key in errors) {
            if (errors.hasOwnProperty(key)) {
                let errorKey = key.replace('activationVoucher.', '');
                let errorId = errorKey + "Error";
                $("#" + errorId).html(errors[key]);
            }
        }
    }

    function clearModalFields() {
        $('#invoiceNumber').val('');
        $('#email').val('');
        $('#phoneNumber').val('');
    }

    function clearErrorMessages() {
        $('#InvoiceNumberError').html('');
        $('#EmailError').html('');
        $('#PhoneNumberError').html('');
    }

    function getVoucher(serialNumber) {
        return $.ajax({
            url: "/Voucher/GetVoucher",
            type: "GET",
            data: {
                "serialNumber": serialNumber
            }
        });
    }
});