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
            { "data": "status" }
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
});