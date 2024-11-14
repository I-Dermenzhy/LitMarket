$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tableData').DataTable({
        "ajax": { url: '/admin/book/getall'},
        "columns": [
            { data: 'title', "width": "30%" },
            { data: 'author', "width": "20%" },
            { data: 'genre.name', "width": "20%" },
            {
                data: 'priceList.price',
                "render": function (data) {
                    const formattedPrice = '$' + parseFloat(data).toFixed(2);
                    return formattedPrice;
                },
                "width": "8%"
            },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                        <a href="/admin/book/upsert?id=${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>
                        <a onClick=Delete("/admin/book/delete/${data}") class="btn btn-danger mx-2">
                            <i class="bi bi-trash-fill"></i> Delete
                        </a>
                    </div>`
                },
                "width": "22%"
            },
        ]
    });
}