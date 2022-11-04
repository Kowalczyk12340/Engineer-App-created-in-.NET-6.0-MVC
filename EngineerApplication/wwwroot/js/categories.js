var dataTable;

$(document).ready(function () {
    loadDataTable();

    $(function () {
        $("#btnSubmit").click(function () {
            $("input[name='GridHtml']").val($("#tblData").html());
        });
    });
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/category/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "40%" },
            { "data": "displayOrder", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/category/AddOrUpdate/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                                    <i class='far fa-edit'></i> Edytuj
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/category/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                                    <i class='far fa-trash-alt'></i> Usuń
                                </a>
                            </div>
                            `;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable":"No records found."
        },
        "width":"100%"
    });
}

function Delete(url) {
    swal({
        title: "Czy na pewno chcesz usunąć tą kategorię?",
        text: "Po usunięciu nie możesz przywrócić tej zawartości!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DE3M39",
        confirmButtonText: "Tak, usuń to!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}

