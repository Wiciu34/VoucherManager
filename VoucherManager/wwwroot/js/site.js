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
                        <button class="btn btn-success" data-serialNumber="${row.serialNumber}" data-bs-toggle="modal" data-bs-target="#"><i class="bi bi-info-square"></i></button>`
                    }
                    else if (row.status === "Nieaktywny") {
                        return `<a href="/Details/${row.serialNumber}" class="btn btn-secondary"><i class="bi bi-info-square"></i></i></a>
                        <button class="btn btn-success" data-serialNumber="${row.serialNumber}" data-bs-toggle="modal" data-bs-target="#activationModal"><i class="bi bi-pencil-square text-white"></i></button>`;
                    }
                    else {
                        return
                        `<a href="/Details/${row.serialNumber}" class="btn btn-secondary"><i class="bi bi-info-square"></i></a>`;
                 
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
        let serialNumber = $(this).data('serialNumber');

    });

    function submitActivationForm(serialNumber) {

        let formData = {
            serialNumber: serialNumber,
            invoiceNumber = $('#invoiceNumber').val(),
            email = $('#email').val(),
            phoneNumber = $('#phoneNumber').val()
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
                    alert('Wystąpił błąd podczas aktywacji vouchera.');
                }
            },
            error: function () {
                alert('Wystąpił błąd podczas aktywacji vouchera.');
            }
        });
    }

    function clearModalFields() {
        $('#invoiceNumber').val('');
        $('#email').val('');
        $('#phoneNumber').val('');
    }
});