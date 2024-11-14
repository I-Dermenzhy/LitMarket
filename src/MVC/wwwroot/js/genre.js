$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tableData').DataTable({
        "ajax": { url: '/admin/genre/getall' },
        "columns": [
            { data: 'name', "width": "40%" },
            { data: 'displayOrder', "width": "20%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                        <a href="/admin/genre/upsert?id=${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>
                        <a onClick=Delete("/admin/genre/delete/${data}") class="btn btn-danger mx-2">
                            <i class="bi bi-trash-fill"></i> Delete
                        </a>
                    </div>`
                },
                "width": "40%"
            },
        ]
    });
}
