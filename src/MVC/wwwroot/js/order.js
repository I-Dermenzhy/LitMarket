$(document).ready(function () {
    var url = window.location.search;

    if (url.includes("approved")) {
        loadDataTable("approved");
        return;
    }
    if (url.includes("paymentPending")) {
        loadDataTable("paymentPending");
        return;
    }
    if (url.includes("inProcess")) {
        loadDataTable("inProcess");
        return;
    }
    if (url.includes("cancelled")) {
        loadDataTable("cancelled");
        return;
    }
    if (url.includes("completed")) {
        loadDataTable("completed");
        return;
    }

    loadDataTable("all");
});

function loadDataTable(status) {
    dataTable = $('#tableData').DataTable({
        "ajax": { url: '/admin/order/getAll?status=' + status },
        "columns": [
            { data: 'id', "width": "10%" },
            { data: 'customer.name', "width": "27%" },
            { data: 'status', "width": "20%" },
            {
                data: 'totalPrice',
                "render": function (data) {
                    const formattedTotalPrice = '$' + parseFloat(data).toFixed(2);
                    return formattedTotalPrice;
                },
                "width": "18%"
            },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                        <a href="/admin/order/details?orderId=${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square"></i> Details
                        </a>
                    </div>`
                },
                "width": "25%"
            },
        ],
        "rowCallback": function (row, data) {
            const statusCell = $(row).find('td:eq(2)');
            if (data.status.toLowerCase() === 'cancelled') {
                const cells = $(row).children('td');
                cells.not(':last-child').css('background-color', 'lightcoral');
            };
            if (data.status.toLowerCase() === 'shipped' || data.status.toLowerCase() === 'delivered') {
                const cells = $(row).children('td');
                cells.not(':last-child').css('background-color', 'lightgreen');
            };
        }
    });
}